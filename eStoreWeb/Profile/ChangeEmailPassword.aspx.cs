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
using System.Web.UI.WebControls;
using com.phoenixconsulting.AspNet.Membership;
using com.phoenixconsulting.common.mail;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.navigation;

namespace eStoreWeb.Profile {
    public partial class ChangeEmailPassword : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            if(!Page.IsPostBack) {
                TextBox emailTextBox = getTextBox("EmailTextBox");
                emailTextBox.Text = ChangePassword1.UserName;
            }
        }

        protected void Cancel_Click(object sender, EventArgs e) {
            GoTo.Instance.AccountInfoPage();
        }

        protected void ChangePassword_Click(object sender, EventArgs e) {
            DTMembershipProvider dtmp = (DTMembershipProvider)Membership.Providers["DTMembershipProvider"];
            TextBox emailTextBox = getTextBox("EmailTextBox");

            dtmp.ChangePassword(ChangePassword1.UserName,
                                ChangePassword1.CurrentPassword,
                                ChangePassword1.NewPassword);

            if(!emailTextBox.Text.Equals(ChangePassword1.UserName)) {
                //email address has been changed
                dtmp.LockUser(ChangePassword1.UserName);
                dtmp.ChangeUsername(ChangePassword1.UserName, emailTextBox.Text);
                
                MailMessageBuilder.SendActivationEmail(emailTextBox.Text,
                                                       SessionHandler.Instance.LoginFirstName,
                                                       SessionHandler.Instance.LoginLastName,
                                                       getTextBox("NewPassword").Text);
                ChangePassword1.SuccessText = "Your email and password have been changed successfully.";
            }
        }

        protected void ChangePassword1_ChangedPassword(object sender, EventArgs e) {}

        protected void PasswordChangedContinue_Click(object sender, EventArgs e) {
            GoTo.Instance.HomePage();
        }

        private TextBox getTextBox(string controlName) {
            return (TextBox)ChangePassword1.ChangePasswordTemplateContainer.FindControl(controlName);
        }
    }
}