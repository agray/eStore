﻿#region Licence
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
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.navigation;

namespace eStoreWeb.Profile {
    public partial class AddSavedSearch : BasePage {
        protected void SaveSearch(object sender, EventArgs e) {
            if(IsLoggedIn()) {
                //Session still active
                var userBLL = new UserBLL();
                var foundUserID = userBLL.getUserIDByEmail(Page.User.Identity.Name, "eStore");

                var ss = new SavedSearchBLL();
                ss.addSavedSearch(new Guid(foundUserID), NameTextBox.Text, CriteriaTextBox.Text);
                GoTo.Instance.SavedSearchPage();
            } else {
                GoTo.Instance.HomePage();
            }
        }
    }
}