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
using eStoreBLL;
using eStoreWeb.Controls;
using phoenixconsulting.common;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.logging;
using phoenixconsulting.common.navigation;

namespace eStoreWeb {
    public partial class Search : BasePage {
        protected void Page_Init(object sender, EventArgs e) {
            Master.CurrencyChanged += CurrencyChangedFromMasterPage;
        }

        private void CurrencyChangedFromMasterPage(object sender, CurrencyChangedEventArgs e) {
            GoTo.Instance.SearchPage();
        }

        protected void Page_Load(object sender, EventArgs e) {
            RememberPages();
        }

        protected void SaveSearch(object sender, EventArgs e) {
            if(isLoggedIn() && SessionHandler.Instance.SearchString != String.Empty) {
                //Session still active
                UserBLL userBLL = new UserBLL();
                string foundUserID = userBLL.getUserIDByEmail(Page.User.Identity.Name, "eStore");

                SavedSearchBLL ss = new SavedSearchBLL();
                ss.addSavedSearch(new Guid(foundUserID),
                                  SavedSearchTextBox.Text,
                                  SessionHandler.Instance.SearchString);
            } else {
                GoTo.Instance.HomePage();
            }
        }

        protected void SearchResultsList_DataBound(object sender, EventArgs e) {
            if(SearchResultsList.Items.Count > 0) {
                PagerUtils.SetPageDetails(SearchResultsList);
            }
            LoggerUtil.auditLog(NLog.LogLevel.Info,
                                AuditEventType.KEYWORD_SEARCH,
                                "StoreSite",
                                null, null, null, null, null, null);
        }
    }
}