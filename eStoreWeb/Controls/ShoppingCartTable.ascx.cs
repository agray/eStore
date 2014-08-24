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
using System.Web.UI.WebControls;
using phoenixconsulting.common.cart;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.navigation;

namespace eStoreWeb.Controls {
    public partial class ShoppingCartTable : BaseUserControl {

        public string TotalTitle1 {
            get { return TotalTitle.Text; }
            set { TotalTitle.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e) {
            var sc = new ShoppingCart();
            if(!sc.IsEmptyList()) {
                var dataSet = sc.ConvertToDataSet();
                SessionHandler.Instance.CartDataSet = dataSet;
                cartItemRepeater.DataSource = dataSet.Tables[0];
                cartItemRepeater.DataBind();
            }
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        protected void cartItemRepeater_ItemCommand(object source, CommandEventArgs e) {
            //Remove item from cart
            var sc = new ShoppingCart();
            sc.RemoveItem(Int32.Parse(e.CommandArgument.ToString()));
            GoTo.Instance.ViewCartPage();
        }
    }
}