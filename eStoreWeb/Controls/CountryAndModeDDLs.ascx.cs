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
using System.Web.UI.WebControls;
using phoenixconsulting.common.cart;
using phoenixconsulting.common.handlers;
using PhoenixConsulting.Common.Properties;

namespace eStoreWeb.Controls {
    public partial class CountryAndModeDDLs : BaseDDL {

         protected void cartShipCountry_DataBound(object sender, EventArgs e) {
             if(String.IsNullOrEmpty(SessionHandler.Instance.ShippingCountry)) {
                SetCountryDefault();
            } else {
                SetCountry(SessionHandler.Instance.ShippingCountry);
            }
        }

        protected void cartShipMode_DataBound(object sender, EventArgs e) {
            var sc = new ShoppingCart();
            if(String.IsNullOrEmpty(SessionHandler.Instance.ShippingMode)) {
                SetModeDefault();
            } else if(IsAustralia(SessionHandler.Instance.ShippingCountry)) {
                SetModeDefault();
            } else {
                SetMode(SessionHandler.Instance.ShippingMode);
            }
        }

        private void SetCountryDefault() {
            SessionHandler.Instance.ShippingCountry = Settings.Default.AustraliaCountryID;

            cartShipCountry.ClearSelection();
            cartShipCountry.Items.FindByValue(SessionHandler.Instance.ShippingCountry).Selected = true;
        }

        private void SetCountry(string country) {
            cartShipCountry.ClearSelection();
            cartShipCountry.Items.FindByValue(country).Selected = true;
        }

        private void SetModeDefault() {
            SessionHandler.Instance.ShippingMode = Settings.Default.ModeExpressAirMail;

            cartShipMode.ClearSelection();
            cartShipMode.Items.FindByValue(SessionHandler.Instance.ShippingMode).Selected = true;

        }

        private void SetMode(string mode) {
            cartShipMode.ClearSelection();
            cartShipMode.Items.FindByValue(mode).Selected = true;
        }
        
        private void resetMode(string mode) {
            cartShipMode.ClearSelection();
            cartShipMode.Items.FindByValue(mode).Selected = true;
            SessionHandler.Instance.ShippingMode = mode;
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        protected void cartShipCountry_SelectedIndexChanged(object sender, EventArgs e) {
            var sc = new ShoppingCart();
            var DDL = (DropDownList)sender;
            resetMode(IsAustralia((DDL.SelectedValue))
                          ? Settings.Default.ModeExpressAirMail
                          : Settings.Default.ModeAirMail);
            SessionHandler.Instance.ShippingCountry = cartShipCountry.Text;

            sc.CalculateTotals();
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        protected void cartShipMode_SelectedIndexChanged(object sender, EventArgs e) {
            var sc = new ShoppingCart();
            SessionHandler.Instance.ShippingMode = cartShipMode.Text;

            if(IsAustralia(SessionHandler.Instance.ShippingCountry)) {
                SetModeDefault();
            }
            sc.CalculateTotals();
        }
    }
}