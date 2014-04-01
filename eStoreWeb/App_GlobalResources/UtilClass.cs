using System;
using System.Collections;
using System.Web.UI;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Web.UI.WebControls;
using System.Net.Mail;
using eStoreBLL;
using eStoreDAL;
using eStoreWeb.Properties;
using System.Data;
using com.domaintransformations.culture;

namespace eStoreWeb {
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    public class UtilClass : Page {

        public static bool IsEmptyCart() {
            ArrayList shoppingCart = SessionHandler.ShoppingCart;

            if(shoppingCart == null) {
                return true;
            } else {
                if(shoppingCart.Count == 0) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        public static DataSet ConvertArrayListToDataSet(ArrayList al) {
            DataRow myRow = default(DataRow);

            DataSet tempDS = default(DataSet);
            tempDS = CreateDataSet();

            foreach(CartItem cartItem in al) {
                myRow = tempDS.Tables[0].NewRow();
                myRow[0] = cartItem.ProductId;
                myRow[1] = cartItem.ProductDetails;
                myRow[2] = cartItem.ImagePath;
                myRow[3] = cartItem.ProductPrice;
                myRow[4] = cartItem.ProductWeight;
                myRow[5] = cartItem.ProductQuantity;
                myRow[6] = cartItem.ProductOnSale;
                myRow[7] = cartItem.ProductDiscountPrice;
                myRow[8] = cartItem.ColorId;
                myRow[9] = cartItem.ColorName;
                myRow[10] = cartItem.SizeId;
                myRow[11] = cartItem.SizeName;
                myRow[12] = cartItem.CategoryId;
                myRow[13] = cartItem.DepartmentId;
                myRow[14] = cartItem.Subtotal;
                tempDS.Tables[0].Rows.Add(myRow);
            }

            SessionHandler.CartDataSet = tempDS;
            return tempDS;
        }

        public static DataSet CreateDataSet() {
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

        public static string EmitSizeInformation(string sizeName) {
            if(!String.IsNullOrEmpty(sizeName)) {
                return "<BR>Size: " + sizeName;
            } else {
                return "";
            }
        }

        public static string EmitColorInformation(string colorName) {
            if(!String.IsNullOrEmpty(colorName)) {
                return "<BR>Color: " + colorName;
            } else {
                return "";
            }
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public static string EmitSubtotal(double subtotal) {
            return ((subtotal * SessionHandler.CurrencyXRate).ToString("C"));
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public static string EmitShippingModeName(int mode) {
            if(mode.ToString() == Settings.Default.ModeExpressAirMail) {
                return "EXPRESS";
            } else if(mode.ToString() == Settings.Default.ModeAirMail) {
                return "AIRMAIL";
            } else {
                return "UPS";
            }
        }

        public static string EmitTotalLabel() {
            if(SessionHandler.Currency == Settings.Default.AustralianCurrencyID) {
                return "Total (Inc GST)";
            } else {
                return "Total (Ex GST)";
            }
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public static void SetCurrencyByCountry(int countryId) {
            CurrenciesBLL currTableAdapter = new CurrenciesBLL();
            DAL.CurrencyRow currRow = null;

            currRow = currTableAdapter.getCurrencyByBillingCountry(countryId)[0];
            SessionHandler.Currency = currRow.ID.ToString();
            SessionHandler.CurrencyValue = currRow.Value;
            SessionHandler.CurrencyXRate = currRow.ExchangeRate;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public static void SetCurrencyById(int currencyId) {
            CurrenciesBLL currTableAdapter = new CurrenciesBLL();
            DAL.CurrencyRow currRow = null;

            currRow = currTableAdapter.getCurrencyByID(currencyId)[0];
            SessionHandler.Currency = currRow.ID.ToString();
            SessionHandler.CurrencyValue = currRow.Value;
            SessionHandler.CurrencyXRate = currRow.ExchangeRate;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        public static string GetContinueShoppingUrl() {
            string URL;

            if(String.IsNullOrEmpty(SessionHandler.PreviousPage)) {
                URL = "/";
            } else {
                URL = SessionHandler.PreviousPage + AddQueryString(SessionHandler.PreviousPage);
                if(URL.Contains("Sentinel")) {
                    URL = "/";
                }
            }
            return URL;
        }

        // 
        // Returns the current querystring in a form for appending to the current page.
        //
        private static string AddQueryString(string previousPage) {

            switch(previousPage) {
                case "BrowseItem.aspx":
                    return GetItemQueryString();
                case "BrowseCategory.aspx":
                    return GetCategoryQueryString();
                case "BrowseDepartment.aspx":
                    return GetDepartmentQueryString();
                default:
                    return "";
            }
        }

        private static string GetDepartmentQueryString() {
            if(String.IsNullOrEmpty(SessionHandler.DepartmentId)) {
                return "Sentinel";
            } else {
                return "?DepID=" + SessionHandler.DepartmentId;
            }
        }

        private static string GetCategoryQueryString() {
            if(String.IsNullOrEmpty(SessionHandler.CategoryId) || String.IsNullOrEmpty(SessionHandler.DepartmentId)) {
                return "Sentinel";
            } else {
                return "?DepID=" + SessionHandler.DepartmentId + "&CatID=" + SessionHandler.CategoryId;
            }
        }

        private static string GetItemQueryString() {
            if(String.IsNullOrEmpty(SessionHandler.ProductId) || String.IsNullOrEmpty(SessionHandler.CategoryId) || String.IsNullOrEmpty(SessionHandler.DepartmentId)) {
                return "Sentinel";
            } else {
                return "?ProdID=" + SessionHandler.ProductId + "&CatID=" + SessionHandler.CategoryId + "&DepID=" + SessionHandler.DepartmentId;
            }
        }

        public static bool IsAustralia(string countryId) {
            return (countryId == Settings.Default.AustraliaCountryID);
        }

        public static bool IsSameAddress() {
            if(String.IsNullOrEmpty(SessionHandler.SameAddress)) {
                return true;
            } else {
                return SessionHandler.SameAddress == "1";
            }
        }
    }
}