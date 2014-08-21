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
using eStoreWeb.Properties;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.navigation;

namespace eStoreWeb {
    public partial class ContactUs : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            RememberPages();
            ContactHyperlink1.NavigateUrl = "mailto:" + Settings.Default.ContactUsMailTo;
            ContactHyperlink1.Text = Settings.Default.ContactUsMailTo;
        }

        protected void StoreEmailInDb(object sender, EventArgs e) {
            var email = new EmailBLL();
            email.addEmail(NameTextBox.Text, EmailTextBox.Text, SubjectTextBox.Text, BodyTextBox.Text);
            GoTo.Instance.ContactUsSentPage();
        }
    }
}