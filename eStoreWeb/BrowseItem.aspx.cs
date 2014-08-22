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

using System.Linq;
using eStoreBLL;
using eStoreWeb.Controls;
using eStoreWeb.Properties;
using phoenixconsulting.businessentities.list;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.cart;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.navigation;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eStoreWeb {
    partial class BrowseItem : BasePage {
        protected void Page_Init(object sender, EventArgs e) {
            if(RequestHandler.Instance.ProductID == 0) {
                ProductFormView.DataSourceID = String.Empty;
            }
            //Wire up the event (CurrencyChanged) to the event handler (CurrencyChangedFromMasterPage) 
            Master.CurrencyChanged += CurrencyChangedFromMasterPage;
        }

        private void CurrencyChangedFromMasterPage(object sender, CurrencyChangedEventArgs e) {
            BrowseItemUpdatePanel.Update();
        }

        protected void Page_Load(object sender, EventArgs e) {
            RememberPages();

            if(ProductFormView.DataSourceID != String.Empty) {
                SessionHandler.Instance.DepartmentId = RequestHandler.Instance.DepartmentID;
                SessionHandler.Instance.CategoryId = RequestHandler.Instance.CategoryID;
                SessionHandler.Instance.ProductId = RequestHandler.Instance.ProductID;

                var products = new ProductsBLL();
                products.IncrementNumViews(RequestHandler.Instance.ProductID);
            }
            var prodId = RequestHandler.Instance.ProductID;
            SetProductMetaTags(prodId);
            Page.Title = GetPageTitle(prodId);
        }

        protected void ProductFormView_DataBound(object sender, EventArgs e) {
            if(ProductFormView.DataSourceID != "") {
                var productNameLabel = (Label)ProductFormView.FindControl("HeaderNameLabel");
                if(productNameLabel != null) {
                    SetDiggUrl(productNameLabel.Text);
                    SetDeliciousUrl(productNameLabel.Text);
                    SetRedditUrl(productNameLabel.Text);
                    SetGoogleUrl(productNameLabel.Text);
                    SetEmailUrl(productNameLabel.Text);
                } else {
                    Page.Title = "Product not found.";
                }
            } else {
                Page.Title = "Product not found.";
            }
        }

        protected void MiniImageListView_ItemDataBound(object sender, ListViewItemEventArgs e) {
            if(e.Item.ItemType == ListViewItemType.DataItem) {
                var miniImage = (Image)e.Item.FindControl("MiniImage");
                var imageUrl = GetImageUrl(miniImage.ImageUrl);
                miniImage.Attributes.Add("onMouseOver", "ctl00_ContentPlaceHolder1_ProductFormView_MainImage.src='" + imageUrl + "';ctl00_ContentPlaceHolder1_ProductFormView_MainImage.alt='" + miniImage.AlternateText + "'");
            }
        }

        private static string GetImageUrl(string url) {
            return url.Remove(0, 2);
        }

        private void SetDiggUrl(string productName) {
            var hyperLink = (HyperLink)ProductFormView.FindControl("DiggHyperLink");
            hyperLink.NavigateUrl = Settings.Default.diggURLPrefix + Request.Url;
            hyperLink.NavigateUrl = hyperLink.NavigateUrl + "&title=" + productName;
        }

        private void SetDeliciousUrl(string productName) {
            var hyperLink = (HyperLink)ProductFormView.FindControl("DeliciousHyperLink");
            hyperLink.NavigateUrl = Settings.Default.deliciousURLPrefix + Request.Url;
            hyperLink.NavigateUrl = hyperLink.NavigateUrl + "&title=" + productName;
        }

        private void SetRedditUrl(string productName) {
            var hyperLink = (HyperLink)ProductFormView.FindControl("RedditHyperLink");
            hyperLink.NavigateUrl = Settings.Default.redditURLPrefix + Request.Url;
            hyperLink.NavigateUrl = hyperLink.NavigateUrl + "&title=" + productName;
        }

        private void SetGoogleUrl(string productName) {
            var hyperLink = (HyperLink)ProductFormView.FindControl("GoogleHyperLink");
            hyperLink.NavigateUrl = Settings.Default.googleURLPrefix + Request.Url;
            hyperLink.NavigateUrl = hyperLink.NavigateUrl + "&title=" + productName;
        }

        private void SetEmailUrl(string productName) {
            var mySu = Server;

            var hyperLink = (HyperLink)ProductFormView.FindControl("EmailHyperLink");
            hyperLink.NavigateUrl = Settings.Default.emailURLPrefix + productName + "&body=" + mySu.UrlEncode(Request.Url.ToString());
        }

        protected string ConcatKeys(string depId, string catId, string prodId) {
            return depId + " " + catId + " " + prodId;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected void AddToCart(object sender, ImageClickEventArgs e) {
            var cart = new ShoppingCart();
            var keys = ((ImageButton)sender).CommandArgument.Split(' ');
            var cartItem = new DTItem(Int32.Parse(keys[0]), Int32.Parse(keys[1]), Int32.Parse(keys[2]), GetProductDetailsFromPage(), GetImagePathFromPage(), GetUnitPriceFromPage(), GetProductWeightFromPage(), GetProductQuantityFromPage(), GetProductOnSaleFromPage(), GetProductDiscountPriceFromPage(), GetColorIdFromPage(), GetColorNameFromPage(), GetSizeIdFromPage(), GetSizeNameFromPage());

            cart.AddItem(cartItem);

            //Go to ViewCart Page
            GoTo.Instance.ViewCartPage();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected void AddToWishList(object sender, ImageClickEventArgs e) {
            var userId = BaseUserControl.GetUserId(this);
            var wishListBll = new WishListsBLL();
            var currentList = wishListBll.GetWishListByUserId(userId);
            var prodId = RequestHandler.Instance.ProductID;
            var colorId = GetColorIdFromPage();
            var sizeId = GetSizeIdFromPage();
            var quantity = GetProductQuantityFromPage();

            if(currentList.Rows.Count == 0 || !ExistsInWishList(currentList, prodId, colorId, sizeId)) {
                //current list is empty OR list not empty but not in list
                wishListBll.AddItem(userId, prodId, quantity, colorId, sizeId);
            } else {
                //item exists in list, increase quantity
                wishListBll.IncreaseQuantity(userId, prodId, sizeId, colorId, quantity);
            }

            //Go to WishList Page
            GoTo.Instance.WishListPage();
        }

        private static bool ExistsInWishList(DataTable currentList, int prodId, int colorId, int sizeId) {
            return (from DataRow dr in currentList.Rows 
                    let itemProdId = int.Parse(dr["ProdID"].ToString()) 
                    let itemColorId = int.Parse(dr["ColorID"].ToString()) 
                    let itemSizeId = int.Parse(dr["SizeID"].ToString()) where (itemProdId == prodId) && (itemColorId == colorId) && (itemSizeId == sizeId) 
                    select itemProdId).Any();
        }

        private string GetProductDetailsFromPage() {
            var prodName = (Label)ProductFormView.FindControl("ProductNameLabel");
            var companyName = (Label)ProductFormView.FindControl("CompanyNameLabel");
            return prodName.Text + " From " + companyName.Text;
        }

        private string GetImagePathFromPage() {
            var label = (Label)ProductFormView.FindControl("ImagePathLabel");
            return label.Text;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private double GetUnitPriceFromPage() {
            var onSaleLabel = (Label)ProductFormView.FindControl("OnSaleLabel");
            var label = Int32.Parse(onSaleLabel.Text) == 1
                              ? (Label)ProductFormView.FindControl("DiscountUnitPriceLabel")
                              : (Label)ProductFormView.FindControl("HiddenUnitPriceLabel");
            return Convert.ToDouble(label.Text);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private double GetProductWeightFromPage() {
            var label = (Label)ProductFormView.FindControl("ProductWeightLabel");
            return Convert.ToDouble(label.Text);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private int GetProductQuantityFromPage() {
            var ddl = (DropDownList)ProductFormView.FindControl("productQuantityDDL").Controls[0];
            return Int32.Parse(ddl.SelectedValue);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private int GetProductOnSaleFromPage() {
            var label = (Label)ProductFormView.FindControl("OnSaleLabel");
            return Int32.Parse(label.Text);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private double GetProductDiscountPriceFromPage() {
            var label = (Label)ProductFormView.FindControl("DiscountUnitPriceLabel");
            return String.IsNullOrEmpty(label.Text) ? 0 : Convert.ToDouble(label.Text);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected bool HasProductSizes() {
            var label = (Label)ProductFormView.FindControl("NumProductSizes");
            return (Int32.Parse(label.Text) > 0);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected bool HasProductColors() {
            var label = (Label)ProductFormView.FindControl("NumProductColors");
            return (Int32.Parse(label.Text) > 0);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected bool HasSn() {
            var label = (Label)ProductFormView.FindControl("HasSN");
            return (Int32.Parse(label.Text) == 1);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private int GetColorIdFromPage() {
            var ddl = (DropDownList)ProductFormView.FindControl("productColorDDL").Controls[0];
            return !String.IsNullOrEmpty(ddl.SelectedValue) ? Int32.Parse(ddl.SelectedValue) : 0;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private string GetColorNameFromPage() {
            var ddl = (DropDownList)ProductFormView.FindControl("productColorDDL").Controls[0];
            return (ddl.SelectedItem != null) ? ddl.SelectedItem.ToString() : "";
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private int GetSizeIdFromPage() {
            var ddl = (DropDownList)ProductFormView.FindControl("productSizeDDL").Controls[0];
            return !String.IsNullOrEmpty(ddl.SelectedValue) ? Int32.Parse(ddl.SelectedValue) : 0;
        }

        private string GetSizeNameFromPage() {
            var ddl = (DropDownList)ProductFormView.FindControl("productSizeDDL").Controls[0];
            return (ddl.SelectedItem != null) ? ddl.SelectedItem.ToString() : "";
        }

        private static string GetPageTitle(int id) {
            var p = new ProductsBLL();
            return p.GetProductPageTitle(id);
        }
    }
}