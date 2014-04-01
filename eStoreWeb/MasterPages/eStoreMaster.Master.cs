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
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.navigation;
using eStoreBLL;
using eStoreDAL;
using eStoreWeb.Controls;
using NLog;

namespace eStoreWeb {
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    public partial class eStoreMaster : MasterPage {
        private static Logger logger = LogManager.GetLogger("TraceFileAndEventLogger");
        protected DropDownList NavCart_CurrencyDropDownList;
        protected UpdatePanel NavCurrencyPanel;
        protected Repeater NavCatalogue_DepartmentRepeater;

        public delegate void CurrencyChangedEventHandler(object sender, CurrencyChangedEventArgs e);
        public event CurrencyChangedEventHandler CurrencyChanged;

        protected virtual void OnCurrencyChanged(CurrencyChangedEventArgs e) {
            DropDownList ddl = (DropDownList)currencyDDL.FindControl("CurrencyDropDownList");
            DAL.CurrencyRow row = getCurrencyById(int.Parse(ddl.SelectedValue));
            SessionHandler.Instance.SetCurrency(row.Value, row.ExchangeRate, row.ID.ToString());

            if(CurrencyChanged != null) {
                CurrencyChanged(this, e);
            }
        }

        public DAL.CurrencyRow getCurrencyById(int id) {
            return (new CurrenciesBLL()).getCurrencyByID(id)[0];
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected void currencyDDL_IndexChanged(object sender, EventArgs e) {
            OnCurrencyChanged((CurrencyChangedEventArgs)e);
            NavCurrencyPanel.Update();
        }

        protected void Page_Init(object sender, EventArgs e) {
            if(Page.IsPostBack) {
                currencyDDL.IndexChanged +=
                    new IndexChangedEventHandler(currencyDDL_IndexChanged);
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                currencyDDL.Text = SessionHandler.Instance.Currency;
            }
            //Build NavCatalogue Menu
            //BuildCatalogueMenu("FK_tCategory_tDepartment", "ID", "DepID");
            NavCurrencyPanel.Update();
        }

        public static string LoginText(HttpContext context) {
            if(context.Request.UrlReferrer == null) {
                return context.Request.IsAuthenticated
                           ? "Welcome, " + getAuthenticatedFullName()
                           : String.Empty;
            } 
            string authenticatedFullName = getAuthenticatedFullName();
            return !authenticatedFullName.Equals(String.Empty)
                       ? "Welcome, " + getAuthenticatedFullName()
                       : String.Empty;
        }

        private static string getAuthenticatedFullName() {
            UserBLL user = new UserBLL();
            string fullname = user.getFullName(HttpContext.Current.User.Identity.Name, "eStore");
            setLoginSessionVariables(fullname);
            return fullname;
        }

        private static void setLoginSessionVariables(string fullname) {
            string[] names = fullname.Split(' ');
            if(names.Length.Equals(2)) {
                SessionHandler.Instance.LoginFirstName = names[0];
                SessionHandler.Instance.LoginLastName = names[1];
            }
        }

        protected void GoButton_Click(object sender, EventArgs e) {
            //This is for the search box
            SessionHandler.Instance.SearchString = SearchQueryTextBox.Text;
            GoTo.Instance.SearchPage();
        }
    }
}