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
using phoenixconsulting.businessentities.list;
using phoenixconsulting.common.cart;
using phoenixconsulting.common.navigation;

namespace eStoreWeb.Controls {
    public partial class OrderHistoryDetailTable : BaseUserControl {
        public string TotalTitle1 {
            get {
                Label totalTitle = (Label)orderHistoryDetailItemLV.FindControl("TotalTitle");
                return totalTitle.Text; 
            }
            set {
                Label totalTitle = (Label)orderHistoryDetailItemLV.FindControl("TotalTitle");
                totalTitle.Text = value; 
            }
        }

        protected void ProductDetailsLabel_DataBinding(object sender, EventArgs e) {
            Label label = (Label)(sender);
            label.Text = string.Format("{0} From {1}", Eval("Product"), Eval("Supplier"));
        }

        protected void AddOrderToCart(object sender, EventArgs e) {
            foreach(ListViewDataItem lvdi in orderHistoryDetailItemLV.Items) {
                int depID = int.Parse(((Label)lvdi.FindControl("HiddenDepIDLabel")).Text);
                int catID = int.Parse(((Label)lvdi.FindControl("HiddenCatIDLabel")).Text);
                int prodID = int.Parse(((Label)lvdi.FindControl("HiddenProdIDLabel")).Text);
                string prodDetails = ((Label)lvdi.FindControl("ProductDetailsLabel")).Text;
                string imgPath = ((Label)lvdi.FindControl("HiddenImgPathLabel")).Text;
                double unitPrice = double.Parse(((Label)lvdi.FindControl("HiddenUnitPriceLabel")).Text);
                double prodWeight = double.Parse(((Label)lvdi.FindControl("HiddenProdWeightLabel")).Text);
                int prodQuantity = int.Parse(((Label)lvdi.FindControl("HiddenProdQuantityLabel")).Text);
                int isOnSale = int.Parse(((Label)lvdi.FindControl("HiddenIsOnSaleLabel")).Text);
                double discPrice = double.Parse(((Label)lvdi.FindControl("HiddenDiscPriceLabel")).Text);
                int colorID = int.Parse(((Label)lvdi.FindControl("HiddenColorIDLabel")).Text);
                string colorName = ((Label)lvdi.FindControl("HiddenColorNameLabel")).Text;
                int sizeID = int.Parse(((Label)lvdi.FindControl("HiddenSizeIDLabel")).Text);
                string sizeName = ((Label)lvdi.FindControl("HiddenSizeNameLabel")).Text;
                
                DTItem item = new DTItem(depID, catID, prodID, prodDetails, imgPath, unitPrice, 
                                         prodWeight, prodQuantity, isOnSale, discPrice, colorID, 
                                         colorName, sizeID, sizeName);
                AddToCart(item);
            }

            GoTo.Instance.ViewCartPage();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private void AddToCart(DTItem listItem) {
            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(listItem);
        }
    }
}