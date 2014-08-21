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
using eStoreWeb.Controls;
using phoenixconsulting.common;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.navigation;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace eStoreWeb {
    partial class BrowseCategory : BasePage {
        protected void Page_Init(object sender, EventArgs e) {
            if(RequestHandler.Instance.CategoryID == 0) {
                CategoryList.DataSourceID = "";
            }
            
            //Wire up the event (CurrencyChanged) to the event handler (CurrencyChangedFromMasterPage) 
            Master.CurrencyChanged += CurrencyChangedFromMasterPage;

            SetCategoryMetaTags(RequestHandler.Instance.CategoryID);
            PopulateHeaderDetails();
        }

        private void CurrencyChangedFromMasterPage(object sender, CurrencyChangedEventArgs e) {
            CategoryUpdatePanel.Update();
        }

        protected void Page_Load(object sender, EventArgs e) {
            RememberPages();

            if(CategoryList.DataSourceID == "") {
                Page.Title = "Category not found.";
            } else {
                SessionHandler.Instance.DepartmentId = RequestHandler.Instance.DepartmentID;
                SessionHandler.Instance.CategoryId = RequestHandler.Instance.CategoryID;
            }
        }

        protected void CategoryList_DataBound(object sender, EventArgs e) {
            if(CategoryList.Items.Count > 0) {
                PagerUtils.SetPageDetails(CategoryList);
            }
        }

        protected void CategoryList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e) {
            PagerUtils.SetPageDetails((ListView)sender);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void PopulateHeaderDetails() {
            var catTableAdapter = new CategoriesBLL();

            try {
                var catRow = catTableAdapter.getCategoryByID(RequestHandler.Instance.CategoryID)[0];
                DepartmentName_Breadcrumb.Text = catRow.DepName;
                DepartmentName_Breadcrumb.NavigateUrl = "BrowseDepartment.aspx?DepID=" + catRow.DepID;
                HeaderNameLabel.Text = catRow.Name;
                HeaderDescriptionLabel.Text = catRow.Description;
                Page.Title = catRow.DepName + " - " + catRow.Name;
            } catch(Exception) {
                Page.Title = "Category not found.";
            }
        }

        protected void ViewDetails_Click(object sender, CommandEventArgs e) {
            GoTo.Instance.BrowseItemPage("?ProdID=" + e.CommandArgument);
        }
    }
}