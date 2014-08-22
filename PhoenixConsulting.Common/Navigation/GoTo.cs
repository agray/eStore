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
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.cart;
using phoenixconsulting.common.handlers;
using System;

namespace phoenixconsulting.common.navigation {
    public sealed class GoTo : SingletonBase<GoTo> {
        private GoTo() {
        }

    #region Page Methods
        public void HomePage() {
            RedirectBrowser(ROOT + "/Splash.aspx");
        }

        public void Custom404Page() {
            RedirectBrowser(Pages.ERROR);
        }

        public void AccountInfoPage() {
            RedirectBrowser(Pages.ACCOUNT_INFO);
        }

        public void RegisterPage() {
            RedirectBrowser(Pages.REGISTER);
        }

        public void WishListPage() {
            RedirectBrowser(Pages.WISHLIST);
        }

        public void ContactUsSentPage() {
            RedirectBrowser(Pages.CONTACT_US_SENT);
        }

        public void SearchPage() {
            RedirectBrowser(Pages.SEARCH);
        }

        public void BrowseDepartmentPage(string qString) {
            RedirectBrowser(Pages.BROWSE_DEPARTMENT + qString);
        }

        public void BrowseCategoryPage(string qString) {
            RedirectBrowser(Pages.BROWSE_CATEGORY + qString);
        }

        public void BrowseItemPage(string qString) {
            RedirectBrowser(Pages.BROWSE_ITEM + qString);
        }

        public void ViewCartPage() {
            RedirectBrowser(Pages.VIEW_CART);
        }

        public void Checkout1Page() {
            var sc = new ShoppingCart();
            if(SessionHandler.Instance.CartDataSet == null || sc.IsEmptyList()) {
                HomePage();
            } else {
                RedirectBrowser(Pages.CHECKOUT1);
            }
        }

        public void Checkout2Page() {
            RedirectBrowser(Pages.CHECKOUT2);
        }

        public void Checkout3Page() {
            RedirectBrowser(Pages.CHECKOUT3);
        }

        public void Checkout4Page() {
            RedirectBrowser(Pages.CHECKOUT4);
        }

        public void Checkout5Page() {
            RedirectBrowser(Pages.CHECKOUT5);
        }

        public void OrderProcessingErrorPage() {
            RedirectBrowser(Pages.ORDER_PROCESSING_ERROR);
        }

        public void OrderHistoryDetailPage(int id) {
            RedirectBrowser(Pages.ORDER_HISTORY_DETAIL + "?orderID=" + id);
        }

        public void SavedSearchPage() {
            RedirectBrowser(Pages.SAVED_SEARCH);
        }

        public void AddSavedSearchPage() {
            RedirectBrowser(Pages.ADD_SAVED_SEARCH);
        }

        public void CustomPage(string url) {
            RedirectBrowser(url);
        }

        public void ContinueShoppingPage() {
            RedirectBrowser(GetContinueShoppingUrl());   
        }
    #endregion

        private static string GetContinueShoppingUrl() {
            string url;

            if (String.IsNullOrEmpty(SessionHandler.Instance.PreviousPage)) {
                url = "/";
            } else {
                url = SessionHandler.Instance.PreviousPage + AddQueryString(SessionHandler.Instance.PreviousPage);
                if (url.Contains("Sentinel")) {
                    url = "/";
                }
            }
            return url;
        }

        #region QueryStringUtils
        //********************************************************************
        //  Returns the current querystring for appending to the current page.
        //********************************************************************
        private static string AddQueryString(string previousPage) {

            switch (previousPage) {
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
            return SessionHandler.Instance.DepartmentId == 0
                   ? "Sentinel"
                   : "?DepID=" + SessionHandler.Instance.DepartmentId;
        }

        private static string GetCategoryQueryString() {
            return SessionHandler.Instance.CategoryId == 0
                   ? "Sentinel"
                   : "?CatID=" + SessionHandler.Instance.CategoryId;
        }

        private static string GetItemQueryString() {
            return SessionHandler.Instance.ProductId == 0
                   ? "Sentinel"
                   : "?ProdID=" + SessionHandler.Instance.ProductId;
        }

        #endregion

        private void RedirectBrowser(string url) {
            ResponseObject.Redirect(url);
        }
    }
}