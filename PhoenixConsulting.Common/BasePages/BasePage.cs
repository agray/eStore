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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using eStoreBLL;
using eStoreDAL;
using phoenixconsulting.common.handlers;
using PhoenixConsulting.Common.Properties;

namespace phoenixconsulting.common.basepages {
    public class BasePage : Page {
        public const string ROOT = "~/";
        
        public static bool IsAustralia(string countryId) {
            return (countryId == Settings.Default.AustraliaCountryID);
        }

        public static bool IsSameAddress() {
            return String.IsNullOrEmpty(SessionHandler.Instance.SameAddress) || SessionHandler.Instance.SameAddress.Equals("1");
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public void SetCurrencyByCountry(int countryId) {
            CurrenciesBLL currTableAdapter = new CurrenciesBLL();
            DAL.CurrencyRow row = currTableAdapter.getCurrencyByBillingCountry(countryId)[0];
            SessionHandler.Instance.SetCurrency(row.Value, row.ExchangeRate, row.ID.ToString());
        }

        #region ListUtils
        public static bool isEmpty(ListView list) {
            return list.Items.Count == 0;
        }

        public static bool isEmpty(FormView form) {
            return form.DataItemCount == 0;
        }
        #endregion

        #region PageUtils
        protected void RememberPages() {
            SessionHandler.Instance.PreviousPage = SessionHandler.Instance.CurrentPage;
            SessionHandler.Instance.CurrentPage = GetCurrentPageName();
        }
        #endregion

        #region metaTagsUtils
        protected void setDepartmentMetaTags(int ID) {
            DepartmentsBLL departmentBLL = new DepartmentsBLL();
            ArrayList al = departmentBLL.getSEODetails(ID);

            addMetaTagSet(al);
        }


        protected void setCategoryMetaTags(int ID) {
            CategoriesBLL categoryBLL = new CategoriesBLL();
            ArrayList al = categoryBLL.getSEODetails(ID);

            addMetaTagSet(al);
        }

        protected void setProductMetaTags(int ID) {
            ProductsBLL productBLL = new ProductsBLL();
            ArrayList al = productBLL.getSEODetails(ID);

            addMetaTagSet(al);
        }

        private void addMetaTagSet(ArrayList al){
            try {
                addMetaTag("description", al, 0);
                addMetaTag("keywords", al, 1);
            } catch(ArgumentOutOfRangeException atmex) {
                Console.WriteLine(atmex.Message);
            }
        }

        private void addMetaTag(string name, ArrayList al, int itemIndex){
            HtmlMeta h = new HtmlMeta();
            h.Name = name;
            h.Content = ((string[])al[0])[itemIndex];
            Page.Header.Controls.Add(h);
        }

        #endregion

        #region AuthenticationUtils
        protected bool isLoggedIn() {
            return Page.User.Identity.Name != "";
        }
        #endregion

        #region ValidatorUtils
        //*****************************************
        //  Custom Validator Methods
        //*****************************************
        public static bool validateInt(string s) {
            int throwaway;
            return int.TryParse(s, out throwaway);
        }

        public static bool validateString(string s) {
            return true;
        }

        public static bool validateDouble(string s) {
            double throwaway;
            return double.TryParse(s, out throwaway);
        }

        public static bool validateURL(string s) {
            Regex objPattern = new Regex("((https?|ftp|gopher|http|telnet|file|notes|ms-help):((//)|(\\\\))+[\\w\\d:#@%/;$()~_?\\+-=\\\\.&]*)");
            return objPattern.IsMatch(s);
        }
        public static bool validateEmail(string s) {
            Regex objPattern = new Regex("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
            return objPattern.IsMatch(s);
        }

        public static bool validateLength(string s, int length) {
            return s.Length <= length;
        }
        #endregion

        #region Parse Methods
        //*****************************************
        //  Custom Parse Methods
        //*****************************************
        public static int IntParseOrZero(string s) {
            int i;
            return Int32.TryParse(s, out i) ? i : 0;
        }
        #endregion

        #region FormFieldUtils

        public static void EmptyTextBoxValues(Control parent) {
            foreach(Control c in parent.Controls) {
                if((c.Controls.Count > 0)) {
                    EmptyTextBoxValues(c);
                } else if((c.GetType() == typeof(TextBox))) {
                    ((TextBox)(c)).Text = "";
                }
            }
        }

        public static void SetValidatorStatus(Control parent, bool status) {
            foreach(Control c in parent.Controls) {
                if(c.Controls.Count > 0) {
                    SetValidatorStatus(c, status);
                } else if(c.GetType() == typeof(TextBox)) {
                    ((TextBox)c).Enabled = status;
                } else if(c.GetType() == typeof(DropDownList)) {
                    ((DropDownList)c).Enabled = status;
                } else if(c.GetType() == typeof(RequiredFieldValidator)) {
                    ((RequiredFieldValidator)c).Enabled = status;
                } else if(c.GetType() == typeof(RegularExpressionValidator)) {
                    ((RegularExpressionValidator)c).Enabled = status;
                } else if(c.GetType() == typeof(CheckBox)) {
                    ((CheckBox)c).Enabled = status;
                } else if(c.GetType() == typeof(RadioButton)) {
                    ((RadioButton)c).Enabled = status;
                }
            }
        }
        #endregion

        #region Core HTTP Methods
        //*****************************************
        //  Core HTTP Methods
        //*****************************************
        public static string mapPath(string path) {
            return getCurrentContext().Server.MapPath(path);
        }

        protected static HttpContext getCurrentContext() {
            return HttpContext.Current;
        }

        protected static HttpRequest getRequest() {
            return getCurrentContext().Request;
        }

        public static string GetCurrentPath() {
            return getRequest().Url.AbsolutePath;
        }

        public static string GetURL() {
            return getRequest().Url.GetLeftPart(UriPartial.Authority);
        }

        public static string GetCurrentPageName() {
            FileInfo oInfo = new FileInfo(GetCurrentPath());
            return oInfo.Name;
        }

        public static string CurrentUserName() {
            return getCurrentContext().User.Identity.Name;
        }
        #endregion
    }
}