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
using eStoreBLL;
using phoenixconsulting.common.handlers;
using PhoenixConsulting.Common.Properties;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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
            var currTableAdapter = new CurrenciesBLL();
            var row = currTableAdapter.GetCurrencyByBillingCountry(countryId)[0];
            SessionHandler.Instance.SetCurrency(row.Value, row.ExchangeRate, row.ID.ToString(CultureInfo.InvariantCulture));
        }

        #region ListUtils
        public static bool IsEmpty(ListView list) {
            return list.Items.Count == 0;
        }

        public static bool IsEmpty(FormView form) {
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
        protected void SetDepartmentMetaTags(int id) {
            var departmentBll = new DepartmentsBLL();
            var al = departmentBll.GetSeoDetails(id);

            AddMetaTagSet(al);
        }


        protected void SetCategoryMetaTags(int id) {
            var categoryBll = new CategoriesBLL();
            var al = categoryBll.GetSeoDetails(id);

            AddMetaTagSet(al);
        }

        protected void SetProductMetaTags(int id) {
            var productBll = new ProductsBLL();
            var al = productBll.GetSeoDetails(id);

            AddMetaTagSet(al);
        }

        private void AddMetaTagSet(ArrayList al){
            try {
                AddMetaTag("description", al, 0);
                AddMetaTag("keywords", al, 1);
            } catch(ArgumentOutOfRangeException atmex) {
                Console.WriteLine(atmex.Message);
            }
        }

        private void AddMetaTag(string name, ArrayList al, int itemIndex){
            var h = new HtmlMeta {Name = name};
            if(al != null) {
                h.Content = ((string[])al[0])[itemIndex];
            }
            Page.Header.Controls.Add(h);
        }

        #endregion

        #region AuthenticationUtils
        protected bool IsLoggedIn() {
            return Page.User.Identity.Name != string.Empty;
        }
        #endregion

        #region ValidatorUtils
        //*****************************************
        //  Custom Validator Methods
        //*****************************************
        public static bool ValidateInt(string s) {
            int throwaway;
            return int.TryParse(s, out throwaway);
        }

        public static bool ValidateString(string s) {
            return true;
        }

        public static bool ValidateDouble(string s) {
            double throwaway;
            return double.TryParse(s, out throwaway);
        }

        public static bool ValidateUrl(string s) {
            var objPattern = new Regex("((https?|ftp|gopher|http|telnet|file|notes|ms-help):((//)|(\\\\))+[\\w\\d:#@%/;$()~_?\\+-=\\\\.&]*)");
            return objPattern.IsMatch(s);
        }
        public static bool ValidateEmail(string s) {
            var objPattern = new Regex("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
            return objPattern.IsMatch(s);
        }

        public static bool ValidateLength(string s, int length) {
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
        public static string MapThePath(string path) {
            return GetCurrentContext().Server.MapPath(path);
        }

        protected static HttpContext GetCurrentContext() {
            return HttpContext.Current;
        }

        protected static HttpRequest GetRequest() {
            return GetCurrentContext().Request;
        }

        public static string GetCurrentPath() {
            return GetRequest().Url.AbsolutePath;
        }

        public static string GetUrl() {
            return GetRequest().Url.GetLeftPart(UriPartial.Authority);
        }

        public static string GetCurrentPageName() {
            var oInfo = new FileInfo(GetCurrentPath());
            return oInfo.Name;
        }

        public static string CurrentUserName() {
            return GetCurrentContext().User.Identity.Name;
        }
        #endregion
    }
}