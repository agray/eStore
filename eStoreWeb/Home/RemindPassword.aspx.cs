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
using System.Web.Security;
using com.phoenixconsulting.AspNet.Membership;
using com.phoenixconsulting.common.mail;
using eStoreBLL;
using phoenixconsulting.common.basepages;

namespace eStoreWeb.Home {
    public partial class RemindPassword : BasePage {
        protected void RemindPassword_OnClick(object sender, EventArgs e) {
            DTMembershipProvider dtmp = (DTMembershipProvider)Membership.Providers["DTMembershipProvider"];
            UserBLL user = new UserBLL();
            string newPassword;

            if(dtmp == null) {
                return;
            } 
            newPassword = dtmp.ResetPassword(EmailTextBox.Text, null);

            string fullname = user.getFullName(EmailTextBox.Text, "eStore");
            string[] names = getSeparateNames(fullname);

            if(names != null) {
                MailMessageBuilder.SendPasswordResetEmail(EmailTextBox.Text, names[0], names[1], newPassword);
            }
            PasswordChangeLabel.Text = "An email has been sent to " + EmailTextBox.Text;
            PasswordChangeLabel.Visible = true;
        }

        private string[] getSeparateNames(string fullname) {
            string[] names = fullname.Split(' ');
            return names.Length.Equals(2) ? names : null;
        }
    }
}