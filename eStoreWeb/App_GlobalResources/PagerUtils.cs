using System;
using System.Collections;
using System.Web.UI;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Web.UI.WebControls;
using System.Net.Mail;
using eStoreBLL;
using eStoreDAL;
using eStoreWeb.Properties;

namespace eStoreWeb {
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    public class PagerUtils : Page {
        public static void SetPageDetails(ListView list) {
            DataPager beforeList;
            DataPager afterList;

            beforeList = (DataPager)list.FindControl("BeforeListDataPager");
            afterList = (DataPager)list.FindControl("AfterListDataPager");

            SetTotalProducts(beforeList, list);
            SetShowingLabel(beforeList, list);
            SetPagingControlVisibility(beforeList, afterList);

        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private static void SetTotalProducts(DataPager beforeList, Control list) {
            Label TopTotalProductsLabel = (Label)list.FindControl("TopTotalProductsReturnedLabel");
            Label BottomTotalProductsLabel = (Label)list.FindControl("BottomTotalProductsReturnedLabel");

            TopTotalProductsLabel.Text = beforeList.TotalRowCount.ToString();
            BottomTotalProductsLabel.Text = beforeList.TotalRowCount.ToString();
        }

        private static void SetShowingLabel(DataPager beforeList, ListView list) {
            Label TopShowingLabel = (Label)list.FindControl("TopShowingLabel");
            Label BottomShowingLabel = (Label)list.FindControl("BottomShowingLabel");

            int startIndex = beforeList.StartRowIndex + 1;
            int endIndex = beforeList.StartRowIndex + list.Items.Count;
            string labelText;

            if(list.Items.Count == 1) {
                labelText = " product (showing " + startIndex + " - " + endIndex + ")";
            } else {
                labelText = " products (showing " + startIndex + " - " + endIndex + ")";
            }

            TopShowingLabel.Text = labelText;
            BottomShowingLabel.Text = labelText;

        }

        private static void SetPagingControlVisibility(DataPager beforeList, DataPager afterList) {
            if(IsOnePage(beforeList)) {
                HidePagingControl(beforeList, afterList);
            } else {
                ShowPagingControl(beforeList, afterList);
            }
        }

        private static bool IsOnePage(DataPager beforeList) {
            return beforeList.TotalRowCount <= beforeList.PageSize;
        }

        private static void HidePagingControl(Control beforeList, Control afterList) {
            beforeList.Visible = false;
            afterList.Visible = false;
        }

        private static void ShowPagingControl(DataPager beforeList, DataPager afterList) {
            beforeList.Visible = true;
            afterList.Visible = true;
            SetButtonVisibility(beforeList, afterList);
        }

        private static void SetButtonVisibility(DataPager beforeList, DataPager afterList) {

            if(IsFirstPage(beforeList)) {
                HideButton((NextPreviousPagerField)beforeList.Fields[0], "Previous");
                HideButton((NextPreviousPagerField)afterList.Fields[0], "Previous");
                ShowButton((NextPreviousPagerField)beforeList.Fields[2], "Next");
                ShowButton((NextPreviousPagerField)afterList.Fields[2], "Next");
            } else {
                if(IsLastPage(beforeList)) {
                    ShowButton((NextPreviousPagerField)beforeList.Fields[0], "Previous");
                    ShowButton((NextPreviousPagerField)afterList.Fields[0], "Previous");
                    HideButton((NextPreviousPagerField)beforeList.Fields[2], "Next");
                    HideButton((NextPreviousPagerField)afterList.Fields[2], "Next");
                } else {
                    //Not first page and not last page
                    ShowButton((NextPreviousPagerField)beforeList.Fields[0], "Previous");
                    ShowButton((NextPreviousPagerField)afterList.Fields[0], "Previous");
                    ShowButton((NextPreviousPagerField)beforeList.Fields[2], "Next");
                    ShowButton((NextPreviousPagerField)afterList.Fields[2], "Next");
                }
            }
        }

        private static bool IsFirstPage(DataPager beforeList) {
            return (beforeList.StartRowIndex / beforeList.PageSize + 1) == 1;
        }

        private static bool IsLastPage(DataPager beforeList) {
            double totalPages = Math.Ceiling((double)((double)beforeList.TotalRowCount / (double)beforeList.PageSize));
            double pageSelected = beforeList.StartRowIndex / beforeList.PageSize + 1;

            return pageSelected == totalPages;

        }


        public static void HideButton(NextPreviousPagerField pagerField, string mode) {
            if(mode == "Previous") {
                pagerField.ShowPreviousPageButton = false;
            } else {
                pagerField.ShowNextPageButton = false;
            }

        }

        private static void ShowButton(NextPreviousPagerField pagerField, string mode) {
            if(mode == "Previous") {
                pagerField.ShowPreviousPageButton = true;
            } else {
                pagerField.ShowNextPageButton = true;
            }
        }
    }
}