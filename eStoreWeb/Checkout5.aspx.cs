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
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.navigation;

namespace eStoreWeb {
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
    public partial class Checkout5 : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            CameFrom.HandleNavigationRedirect(Request.UrlReferrer);

            //string redirectPage = NavigationUtils.getInvalidNavigationRedirect(Request.UrlReferrer);
            //if(!redirectPage.Equals("")) {
            //    Response.Redirect(redirectPage);
            //}
        }

        protected void GoToPreviousPage(object sender, EventArgs e) {
            GoTo.Instance.Checkout4Page();
        }

        public static bool IsApproved(string approved, string responseCode) {
            return (approved.Equals("Yes") && responseCode.Equals("00"));
        }
    }
}