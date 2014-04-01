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
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.navigation;
using eStoreWeb.Controls;

namespace eStoreWeb.WishList {
    public partial class WishList : BasePage {
        protected WishListTable wlTable; 

        protected void Page_Init(object sender, EventArgs e) {
            //Wire up the event (CurrencyChanged) to the event handler (CurrencyChangedFromMasterPage) 
            Master.CurrencyChanged += CurrencyChangedFromMasterPage;
        }
        
        private void CurrencyChangedFromMasterPage(object sender, CurrencyChangedEventArgs e) {
            string currencyValue = e.CurrencyValue;
            wlTable = WishListTable;
            wlTable.PriceLabel1 = "Price(" + currencyValue + ")";

            GoTo.Instance.WishListPage();
        }
    }
}