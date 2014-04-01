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
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eStoreBLL;
using eStoreDAL;
using eStoreWeb.Controls;
using eStoreWeb.Properties;
using phoenixconsulting.businessentities.list;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.cart;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.navigation;

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

                ProductsBLL products = new ProductsBLL();
                products.incrementNumViews(RequestHandler.Instance.ProductID);
            }
            int prodID = RequestHandler.Instance.ProductID;
            setProductMetaTags(prodID);
            Page.Title = getPageTitle(prodID);
        }

        protected void ProductFormView_DataBound(object sender, EventArgs e) {
            if(ProductFormView.DataSourceID != "") {
                Label ProductNameLabel = (Label)ProductFormView.FindControl("HeaderNameLabel");
                if(ProductNameLabel != null) {
                    setDiggURL(ProductNameLabel.Text);
                    setDeliciousURL(ProductNameLabel.Text);
                    setRedditURL(ProductNameLabel.Text);
                    setGoogleURL(ProductNameLabel.Text);
                    setEmailURL(ProductNameLabel.Text);
                } else {
                    Page.Title = "Product not found.";
                }
            } else {
                Page.Title = "Product not found.";
            }
        }

        protected void MiniImageListView_ItemDataBound(object sender, ListViewItemEventArgs e) {
            if(e.Item.ItemType == ListViewItemType.DataItem) {
                Image miniImage = (Image)e.Item.FindControl("MiniImage");
                string imageURL = getImageURL(miniImage.ImageUrl);
                miniImage.Attributes.Add("onMouseOver", "ctl00_ContentPlaceHolder1_ProductFormView_MainImage.src='" + imageURL + "';ctl00_ContentPlaceHolder1_ProductFormView_MainImage.alt='" + miniImage.AlternateText + "'");
            }
        }

        private static string getImageURL(string URL) {
            return URL.Remove(0, 2);
        }

        private void setDiggURL(string ProductName) {
            HyperLink hyperLink = (HyperLink)ProductFormView.FindControl("DiggHyperLink");
            hyperLink.NavigateUrl = Settings.Default.diggURLPrefix + Request.Url;
            hyperLink.NavigateUrl = hyperLink.NavigateUrl + "&title=" + ProductName;
        }

        private void setDeliciousURL(string ProductName) {
            HyperLink hyperLink = (HyperLink)ProductFormView.FindControl("DeliciousHyperLink");
            hyperLink.NavigateUrl = Settings.Default.deliciousURLPrefix + Request.Url;
            hyperLink.NavigateUrl = hyperLink.NavigateUrl + "&title=" + ProductName;
        }

        private void setRedditURL(string ProductName) {
            HyperLink hyperLink = (HyperLink)ProductFormView.FindControl("RedditHyperLink");
            hyperLink.NavigateUrl = Settings.Default.redditURLPrefix + Request.Url;
            hyperLink.NavigateUrl = hyperLink.NavigateUrl + "&title=" + ProductName;
        }

        private void setGoogleURL(string ProductName) {
            HyperLink hyperLink = (HyperLink)ProductFormView.FindControl("GoogleHyperLink");
            hyperLink.NavigateUrl = Settings.Default.googleURLPrefix + Request.Url;
            hyperLink.NavigateUrl = hyperLink.NavigateUrl + "&title=" + ProductName;
        }

        private void setEmailURL(string ProductName) {
            HttpServerUtility MySU = Server;

            HyperLink hyperLink = (HyperLink)ProductFormView.FindControl("EmailHyperLink");
            hyperLink.NavigateUrl = Settings.Default.emailURLPrefix + ProductName + "&body=" + MySU.UrlEncode(Request.Url.ToString());
        }

        protected string ConcatKeys(string DepID, string CatID, string ProdID) {
            return DepID + " " + CatID + " " + ProdID;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected void AddToCart(object sender, ImageClickEventArgs e) {
            ShoppingCart cart = new ShoppingCart();
            DTItem cartItem = default(DTItem);
            string[] keys = ((ImageButton)sender).CommandArgument.Split(' ');
            cartItem = new DTItem(Int32.Parse(keys[0]), Int32.Parse(keys[1]), Int32.Parse(keys[2]), GetProductDetailsFromPage(), GetImagePathFromPage(), GetUnitPriceFromPage(), GetProductWeightFromPage(), GetProductQuantityFromPage(), GetProductOnSaleFromPage(), GetProductDiscountPriceFromPage(), GetColorIDFromPage(), GetColorNameFromPage(), GetSizeIDFromPage(), GetSizeNameFromPage());

            cart.AddItem(cartItem);

            //Go to ViewCart Page
            GoTo.Instance.ViewCartPage();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected void AddToWishList(object sender, ImageClickEventArgs e) {
            Guid userID = BaseUserControl.getUserID(this);
            WishListsBLL wishListBLL = new WishListsBLL();
            DAL.WishListDataTable currentList = wishListBLL.getWishListByUserID(userID);
            int prodID = RequestHandler.Instance.ProductID;
            int colorID = GetColorIDFromPage();
            int sizeID = GetSizeIDFromPage();
            int quantity = GetProductQuantityFromPage();

            if(currentList.Rows.Count == 0 || !existsInWishList(currentList, prodID, colorID, sizeID)) {
                //current list is empty OR list not empty but not in list
                wishListBLL.addItem(userID, prodID, quantity, colorID, sizeID);
            } else {
                //item exists in list, increase quantity
                wishListBLL.increaseQuantity(userID, prodID, sizeID, colorID, quantity);
            }

            //Go to WishList Page
            GoTo.Instance.WishListPage();
        }

        private bool existsInWishList(DAL.WishListDataTable currentList, int prodID, int colorID, int sizeID) {
            foreach(DataRow dr in currentList.Rows) {
                int itemProdID = int.Parse(dr["ProdID"].ToString());
                int itemColorID = int.Parse(dr["ColorID"].ToString());
                int itemSizeID = int.Parse(dr["SizeID"].ToString());
                if((itemProdID == prodID) && (itemColorID == colorID) && (itemSizeID == sizeID)) {
                    return true;
                }
            }
            return false;
        }

        private string GetProductDetailsFromPage() {
            Label prodName = (Label)ProductFormView.FindControl("ProductNameLabel");
            Label companyName = (Label)ProductFormView.FindControl("CompanyNameLabel");
            return prodName.Text + " From " + companyName.Text;
        }

        private string GetImagePathFromPage() {
            Label label = (Label)ProductFormView.FindControl("ImagePathLabel");
            return label.Text;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private double GetUnitPriceFromPage() {
            Label onSaleLabel = (Label)ProductFormView.FindControl("OnSaleLabel");
            Label label = Int32.Parse(onSaleLabel.Text) == 1
                              ? (Label)ProductFormView.FindControl("DiscountUnitPriceLabel")
                              : (Label)ProductFormView.FindControl("HiddenUnitPriceLabel");
            return Convert.ToDouble(label.Text);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private double GetProductWeightFromPage() {
            Label label = (Label)ProductFormView.FindControl("ProductWeightLabel");
            return Convert.ToDouble(label.Text);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private int GetProductQuantityFromPage() {
            DropDownList DDL = (DropDownList)ProductFormView.FindControl("productQuantityDDL").Controls[0];
            return Int32.Parse(DDL.SelectedValue);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private int GetProductOnSaleFromPage() {
            Label label = (Label)ProductFormView.FindControl("OnSaleLabel");
            return Int32.Parse(label.Text);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private double GetProductDiscountPriceFromPage() {
            Label label = (Label)ProductFormView.FindControl("DiscountUnitPriceLabel");
            return String.IsNullOrEmpty(label.Text) ? 0 : Convert.ToDouble(label.Text);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected bool HasProductSizes() {
            Label label = (Label)ProductFormView.FindControl("NumProductSizes");
            return (Int32.Parse(label.Text) > 0);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected bool HasProductColors() {
            Label label = (Label)ProductFormView.FindControl("NumProductColors");
            return (Int32.Parse(label.Text) > 0);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        protected bool HasSN() {
            Label label = (Label)ProductFormView.FindControl("HasSN");
            return (Int32.Parse(label.Text) == 1);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private int GetColorIDFromPage() {
            DropDownList DDL = (DropDownList)ProductFormView.FindControl("productColorDDL").Controls[0];
            return !String.IsNullOrEmpty(DDL.SelectedValue) ? Int32.Parse(DDL.SelectedValue) : 0;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private string GetColorNameFromPage() {
            DropDownList DDL = (DropDownList)ProductFormView.FindControl("productColorDDL").Controls[0];
            return (DDL.SelectedItem != null) ? DDL.SelectedItem.ToString() : "";
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private int GetSizeIDFromPage() {
            DropDownList DDL = (DropDownList)ProductFormView.FindControl("productSizeDDL").Controls[0];
            return !String.IsNullOrEmpty(DDL.SelectedValue) ? Int32.Parse(DDL.SelectedValue) : 0;
        }

        private string GetSizeNameFromPage() {
            DropDownList DDL = (DropDownList)ProductFormView.FindControl("productSizeDDL").Controls[0];
            return (DDL.SelectedItem != null) ? DDL.SelectedItem.ToString() : "";
        }

        private string getPageTitle(int ID) {
            ProductsBLL p = new ProductsBLL();
            return p.getProductPageTitle(ID);
        }
    }
}