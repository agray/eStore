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
using eStoreWeb.Properties;
using phoenixconsulting.common.basepages;
using phoenixconsulting.common.handlers;

namespace eStoreWeb {
    public partial class Policies : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            RememberPages();
            
            ContactHyperlink1.NavigateUrl = "mailto:" + Settings.Default.ContactUsMailTo;
            ContactHyperlink2.NavigateUrl = "mailto:" + Settings.Default.ContactUsMailTo;
            ContactHyperlink3.NavigateUrl = "mailto:" + Settings.Default.ContactUsMailTo;

            ContactHyperlink1.Text = Settings.Default.ContactUsMailTo;
            ContactHyperlink2.Text = Settings.Default.ContactUsMailTo;
            ContactHyperlink3.Text = Settings.Default.ContactUsMailTo;

            TradingName1.Text = ApplicationHandler.Instance.TradingName;
            TradingName2.Text = ApplicationHandler.Instance.TradingName;
            TradingName3.Text = ApplicationHandler.Instance.TradingName;
            TradingName4.Text = ApplicationHandler.Instance.TradingName;
            TradingName5.Text = ApplicationHandler.Instance.TradingName;
            TradingName6.Text = ApplicationHandler.Instance.TradingName;
            TradingName7.Text = ApplicationHandler.Instance.TradingName;
            TradingName8.Text = ApplicationHandler.Instance.TradingName;
            TradingName9.Text = ApplicationHandler.Instance.TradingName;
            TradingName10.Text = ApplicationHandler.Instance.TradingName;
            TradingName11.Text = ApplicationHandler.Instance.TradingName;
            TradingName12.Text = ApplicationHandler.Instance.TradingName;
            TradingName13.Text = ApplicationHandler.Instance.TradingName;
            TradingName14.Text = ApplicationHandler.Instance.TradingName;
            TradingName15.Text = ApplicationHandler.Instance.TradingName;
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        protected double getRefundAdminFee() {
            return ApplicationHandler.Instance.RefundAdminFeePercent;
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        protected double getCancellationAdminFee() {
            return ApplicationHandler.Instance.CancelAdminFeePercent;
        }
    }
}