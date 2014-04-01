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
using eStoreWeb.Controls;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.navigation;

namespace eStoreWeb {
    partial class ViewCart : BasePage {
        protected ShoppingCartTable cartTable; 
        
        protected void Page_Init(object sender, EventArgs e) {
            //Wire up the event (CurrencyChanged) to the event handler (CurrencyChangedFromMasterPage) 
            Master.CurrencyChanged += CurrencyChangedFromMasterPage;
        }

        private void CurrencyChangedFromMasterPage(object sender, CurrencyChangedEventArgs e) {
            string currencyValue = e.CurrencyValue;
            cartTable = ShoppingCartTable;
            cartTable.TotalTitle1 = "Total Price(" + currencyValue + ")";

            GoTo.Instance.ViewCartPage();
        }

        protected void Page_Load(object sender, EventArgs e) {
            if(SessionHandler.Instance.CurrentPage != "ViewCart.aspx") {
                //Test ensures that previous page is always the one before viewCart.asp
                SessionHandler.Instance.PreviousPage = SessionHandler.Instance.CurrentPage;
            }
            SessionHandler.Instance.CurrentPage = "ViewCart.aspx";
        }

        protected void ContinueShopping_Click(object sender, EventArgs e) {
            GoTo.Instance.ContinueShoppingPage();
        }

        protected void CheckOut_Click(object sender, EventArgs e) {
            GoTo.Instance.Checkout1Page();
        }
    }
}