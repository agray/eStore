#region Licence
/*
 * The MIT License
 *
 * Copyright (c) 2008-2013, Andrew Gray
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;

namespace phoenixconsulting.businessentities.list {
    public class DTItemList: Page {
        //Declare module level array to store list of DTItems
        protected ArrayList itemList;
        
        public bool IsEmptyList() {
            return itemList == null || itemList.Count == 0;
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


        protected int ItemIndex(int productId, int colorId, int sizeId) {
            int index = -1;

            for(int count = 0; count <= itemList.Count - 1; count++) {
                DTItem item = (DTItem)itemList[count];
                if(item.ProductId == productId && item.ColorId == colorId && item.SizeId == sizeId) {
                    index = count;
                    break;
                }
            }

            return index;
        }

        public bool hasWrapping() {
            for(int i = 0; i <= itemList.Count - 1; i++) {
                DTItem item = (DTItem)itemList[i];
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
            DataSet tempDS = CreateDataSet();

            foreach(DTItem item in itemList) {
                DataRow myRow = tempDS.Tables[0].NewRow();
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