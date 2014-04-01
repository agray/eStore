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
using phoenixconsulting.common.basepages;

namespace phoenixconsulting.common.navigation {
    public class Pages : BasePage {
        public const string ERROR = ROOT + "Error.aspx";
        public const string REGISTER = ROOT + "Home/Register.aspx";
        public const string ACCOUNT_INFO = ROOT + "Profile/AccountInfo.aspx";
        public const string WISHLIST = ROOT + "WishList/WishList.aspx";
        public const string CONTACT_US_SENT = ROOT + "ContactUsSent.aspx";
        public const string SEARCH = ROOT + "Search.aspx";
        public const string BROWSE_DEPARTMENT = ROOT + "BrowseDepartment.aspx";
        public const string BROWSE_CATEGORY = "BrowseCategory.aspx";
        public const string BROWSE_ITEM = "BrowseItem.aspx";
        public const string VIEW_CART = ROOT + "ViewCart.aspx";
        public const string CHECKOUT1 = ROOT + "Checkout1.aspx";
        public const string CHECKOUT2 = ROOT + "Checkout2.aspx";
        public const string CHECKOUT3 = ROOT + "Checkout3.aspx";
        public const string CHECKOUT4 = ROOT + "Checkout4.aspx";
        public const string CHECKOUT5 = ROOT + "Checkout5.aspx";
        public const string ORDER_PROCESSING_ERROR = ROOT + "OrderProcessingError.aspx";
        public const string ORDER_HISTORY_DETAIL = ROOT + "Profile/OrderHistoryDetail.aspx";
        public const string SAVED_SEARCH = ROOT + "Profile/SavedSearches.aspx";
        public const string ADD_SAVED_SEARCH = ROOT + "Profile/AddSavedSearch.aspx";
    }
}