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
using phoenixconsulting.businessentities.helpers;
using phoenixconsulting.businessentities.list;
using phoenixconsulting.common.cart;
using phoenixconsulting.common.navigation;

namespace eStoreWeb.Controls {
    public partial class WishListTable : BaseUserControl {
        public string PriceLabel1{
            get {
                Label priceLabel = (Label)wishListItemLV.FindControl("PriceLabel");
                return priceLabel.Text;
            }
            set {
                Label priceLabel = (Label)wishListItemLV.FindControl("PriceLabel");
                priceLabel.Text = value;
            }
        }

        protected void WishListODS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e) {
            e.InputParameters["guid"] = getUserID(this.Page);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        protected void wishListItemLV_ItemCommand(object source, CommandEventArgs e) {
            if(!string.IsNullOrEmpty(e.CommandArgument.ToString())) {
                int ID = Int32.Parse(e.CommandArgument.ToString());
                if(e.CommandName.Equals("delete")) {
                    //Remove Item from WishList
                    WishListHelper.deleteItem(ID);
                    GoTo.Instance.WishListPage();
                } else {
                    //Add Item to Cart
                    DTItem item = WishListHelper.CreateDTItem(ID);
                    AddToCart(item);
                    GoTo.Instance.ViewCartPage();
                }
            }
        }
        
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected void AddAllToCart(object sender, EventArgs e) {
            int ID;
            foreach(ListViewDataItem listItem in wishListItemLV.Items) {
                ID = int.Parse((((Label)listItem.FindControl("HiddenIDLabel")).Text));
                DTItem item = WishListHelper.CreateDTItem(ID);
                AddToCart(item);
            }

            GoTo.Instance.ViewCartPage();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected void UpdateQuantities(object sender, EventArgs e) {
            foreach(ListViewDataItem item in wishListItemLV.Items) {
                int quantity = int.Parse((((TextBox)item.FindControl("QuantityTextBox")).Text));
                int ID = int.Parse((((Label)item.FindControl("HiddenIDLabel")).Text));
                WishListHelper.updateQuantity(ID, quantity);
            }
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private void AddToCart(DTItem listItem) {
            ShoppingCart cart = new ShoppingCart();
            
            cart.AddItem(listItem);
        }
    }
}