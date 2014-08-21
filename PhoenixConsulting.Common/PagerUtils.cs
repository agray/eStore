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
using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace phoenixconsulting.common {
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    public class PagerUtils : Page {
        public static void SetPageDetails(ListView list) {
            var beforeList = (DataPager)list.FindControl("BeforeListDataPager");
            var afterList = (DataPager)list.FindControl("AfterListDataPager");

            if(IsOnePage(beforeList)) {
                HidePagingControls(beforeList, afterList);
            } else {
                if(IsFirstPage(beforeList)) {
                    ((NextPreviousPagerField)beforeList.Fields[0]).Visible = false;
                    ((NextPreviousPagerField)afterList.Fields[0]).Visible = false;
                } else if(PagerUtils.IsLastPage(beforeList)) {
                    ((NextPreviousPagerField)beforeList.Fields[2]).Visible = false;
                    ((NextPreviousPagerField)afterList.Fields[2]).Visible = false;
                } else {
                    ((NextPreviousPagerField)beforeList.Fields[0]).Visible = true;
                    ((NextPreviousPagerField)afterList.Fields[0]).Visible = true;
                    ((NextPreviousPagerField)beforeList.Fields[2]).Visible = true;
                    ((NextPreviousPagerField)afterList.Fields[2]).Visible = true;
                }
            }

            SetTotalProducts(beforeList.TotalRowCount, list);
            SetShowingLabel(beforeList.StartRowIndex, list);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private static void SetTotalProducts(int totalRowCount, Control list) {
            var topTotalProductsLabel = (Label)list.FindControl("TopTotalProductsReturnedLabel");
            var bottomTotalProductsLabel = (Label)list.FindControl("BottomTotalProductsReturnedLabel");

            topTotalProductsLabel.Text = totalRowCount.ToString();
            bottomTotalProductsLabel.Text = totalRowCount.ToString();
        }

        private static void SetShowingLabel(int startRowIndex, ListView list) {
            var topShowingLabel = (Label)list.FindControl("TopShowingLabel");
            var bottomShowingLabel = (Label)list.FindControl("BottomShowingLabel");

            var startIndex = startRowIndex + 1;
            var endIndex = startRowIndex + list.Items.Count;
            var labelText = "(showing " + startIndex + " - " + endIndex + ")";

            labelText = list.Items.Count == 1 
                        ? " product " + labelText 
                        : " products " + labelText;

            topShowingLabel.Text = labelText;
            bottomShowingLabel.Text = labelText;
        }

        private static bool IsOnePage(DataPager list) {
            return list.TotalRowCount <= list.PageSize;
        }

        private static void HidePagingControls(Control beforeList, Control afterList) {
            beforeList.Visible = false;
            afterList.Visible = false;
        }

        public static bool IsFirstPage(DataPager beforeList) {
            return (beforeList.StartRowIndex / beforeList.PageSize + 1) == 1;
        }

        public static bool IsLastPage(DataPager beforeList) {
            var totalPages = Math.Ceiling((beforeList.TotalRowCount / (double)beforeList.PageSize));
            double pageSelected = beforeList.StartRowIndex / beforeList.PageSize + 1;

            return pageSelected == totalPages;
        }
    }
}