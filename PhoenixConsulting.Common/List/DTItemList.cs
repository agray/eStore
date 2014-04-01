using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using com.domaintransformations.common.handlers;

namespace domaintransformations.common.list {
    public class DTItemList: Page {
        //Declare module level array to store list of DTItems
        protected ArrayList itemList;

        #region Constructor
        //contructor
        public DTItemList() {
        }
        #endregion

        public bool IsEmptyList() {
            if(itemList == null) {
                return true;
            } else {
                if(itemList.Count == 0) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        public int ListLength() {
            return itemList.Count;
        }

        public void AddItem(DTItem item) {
            int index = ItemIndex(item.ProductId, item.ColorId, item.SizeId);
            if(index == -1) {
                //Doesn't already exist
                itemList.Add(item);
                itemList.TrimToSize();
            } else {
                //Does already exist - increase quantity only
                IncreaseQuantity(index, item.ProductQuantity);
            }
        }

        public void RemoveItem(int index) {
            itemList.RemoveAt(index);
            itemList.TrimToSize();
        }

        protected void IncreaseQuantity(int index, int quantity) {
            DTItem item = (DTItem)itemList[index];

            item.ProductQuantity = item.ProductQuantity + quantity;
            item.Subtotal = item.ProductQuantity * item.ProductPrice;
        }

        public DTItem GetItem(int index) {
            return (DTItem)itemList[index];
        }


        protected int ItemIndex(int productID, int colorID, int sizeID) {
            DTItem item = default(DTItem);
            int index = -1;
            int count = 0;

            for(count = 0; count <= itemList.Count - 1; count++) {
                item = (DTItem)itemList[count];
                if(item.ProductId == productID & item.ColorId == colorID & item.SizeId == sizeID) {
                    index = count;
                    break;
                }
            }

            return index;
        }

        public bool hasWrapping() {
            DTItem item = default(DTItem);
            int i = 0;

            for(i = 0; i <= itemList.Count - 1; i++) {
                item = (DTItem)itemList[i];
                if(isWrapping(item)) {
                    return true;
                }
            }
            return false;
        }

        protected bool isWrapping(DTItem item) {
            return item.ProductDetails.Contains("Gift Wrapping");
        }

        public DataSet ConvertToDataSet() {
            DataRow myRow = default(DataRow);

            DataSet tempDS = default(DataSet);
            tempDS = CreateDataSet();

            foreach(DTItem item in itemList) {
                myRow = tempDS.Tables[0].NewRow();
                myRow[0] = item.ProductId;
                myRow[1] = item.ProductDetails;
                myRow[2] = item.ImagePath;
                myRow[3] = item.ProductPrice;
                myRow[4] = item.ProductWeight;
                myRow[5] = item.ProductQuantity;
                myRow[6] = item.ProductOnSale;
                myRow[7] = item.ProductDiscountPrice;
                myRow[8] = item.ColorId;
                myRow[9] = item.ColorName;
                myRow[10] = item.SizeId;
                myRow[11] = item.SizeName;
                myRow[12] = item.CategoryId;
                myRow[13] = item.DepartmentId;
                myRow[14] = item.Subtotal;
                tempDS.Tables[0].Rows.Add(myRow);
            }

            SessionHandler.CartDataSet = tempDS;
            return tempDS;
        }

        private DataSet CreateDataSet() {
            DataSet tempDS = new DataSet();
            DataTable dataTable = new DataTable();
            //Satisfies rule: SetLocaleForDataTypes.
            dataTable.Locale = CultureInfo.InvariantCulture;
            tempDS.Locale = CultureInfo.InvariantCulture;
            tempDS.Tables.Add(dataTable);

            tempDS.Tables[0].Columns.Add("ProductID", Type.GetType("System.Int32"), "");
            tempDS.Tables[0].Columns.Add("ProductDetails", Type.GetType("System.String"), "");
            tempDS.Tables[0].Columns.Add("ImagePath", Type.GetType("System.String"), "");
            tempDS.Tables[0].Columns.Add("ProductPrice", Type.GetType("System.Double"), "");
            tempDS.Tables[0].Columns.Add("ProductWeight", Type.GetType("System.Double"), "");
            tempDS.Tables[0].Columns.Add("ProductQuantity", Type.GetType("System.Int32"), "");
            tempDS.Tables[0].Columns.Add("ProductOnSale", Type.GetType("System.Int32"), "");
            tempDS.Tables[0].Columns.Add("ProductDiscountPrice", Type.GetType("System.Double"), "");
            tempDS.Tables[0].Columns.Add("ColorID", Type.GetType("System.Int32"), "");
            tempDS.Tables[0].Columns.Add("ColorName", Type.GetType("System.String"), "");
            tempDS.Tables[0].Columns.Add("SizeID", Type.GetType("System.Int32"), "");
            tempDS.Tables[0].Columns.Add("SizeName", Type.GetType("System.String"), "");
            tempDS.Tables[0].Columns.Add("CategoryID", Type.GetType("System.Int32"), "");
            tempDS.Tables[0].Columns.Add("DepartmentID", Type.GetType("System.Int32"), "");
            tempDS.Tables[0].Columns.Add("SubTotal", Type.GetType("System.Double"), "");

            return tempDS;
        }
    }
}