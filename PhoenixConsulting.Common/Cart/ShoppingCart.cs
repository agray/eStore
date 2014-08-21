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
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using eStoreBLL;
using phoenixconsulting.businessentities.list;
using phoenixconsulting.common.handlers;
using PhoenixConsulting.Common.Properties;

namespace phoenixconsulting.common.cart {
    public class ShoppingCart : DTItemList {
        #region Constructor

        public ShoppingCart() {
            initFromSession();
        }

        #endregion

        public new void AddItem(DTItem cartItem) {
            base.AddItem(cartItem);
            CalculateTotals();

            if(isWrapping(cartItem)) {
                SessionHandler.Instance.Wrapping = true;
            }
            saveToSession();
        }

        public new void RemoveItem(int index) {
            base.RemoveItem(index);
            CalculateTotals();

            if(!hasWrapping()) {
                SessionHandler.Instance.Wrapping = false;
            }
            saveToSession();
        }

        public new void IncreaseQuantity(int index, int quantity) {
            base.IncreaseQuantity(index, quantity);
            saveToSession();
        }

        public void CalculateTotals() {
            double totalWeight = 0;
            double totalSubTotals = 0;
            var totalNumItems = 0;

            foreach(DTItem cartItem in itemList) {
                totalWeight = totalWeight + cartItem.ProductWeight * cartItem.ProductQuantity;
                totalSubTotals = totalSubTotals + cartItem.Subtotal;
                if(!isWrapping(cartItem)) {
                    totalNumItems = totalNumItems + cartItem.ProductQuantity;
                }
            }

            SessionHandler.Instance.TotalShipping = calculateShippingCosts(totalNumItems, totalWeight);
            SessionHandler.Instance.TotalCost = totalSubTotals + SessionHandler.Instance.TotalShipping;
            SessionHandler.Instance.TotalWeight = totalWeight;
        }

        private double calculateShippingCosts(int itemCount, double totalweight) {
            if(totalweight == 0) {
                return 0;
            }

            var shippingRate = calculateShippingRate(totalweight);

            if(SessionHandler.Instance.ShippingCountry == Settings.Default.AustraliaCountryID) {
                //Calculate Shipping for Australia
                return itemCount > 2 ? shippingRate * 2 : shippingRate * itemCount;
            }
            //Calculate International Shipping for Australia
            return shippingRate;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private double calculateShippingRate(double totalweight) {
            var shipCountry = Int32.Parse(String.IsNullOrEmpty(SessionHandler.Instance.ShippingCountry) 
                                              ? Settings.Default.AustraliaCountryID 
                                              : SessionHandler.Instance.ShippingCountry);

            var shippingMode = Int32.Parse(String.IsNullOrEmpty(SessionHandler.Instance.ShippingMode) 
                                               ? Settings.Default.ModeAirMail 
                                               : SessionHandler.Instance.ShippingMode);

            return (double)BLLAdapter.Instance.CostAdapter.GetShippingCosts(shipCountry, shippingMode, totalweight)[0]["Price"];
        }

        #region SessionMethods

        private void initFromSession() {
            itemList = SessionHandler.Instance.ShoppingCart ?? new ArrayList();
        }

        private void saveToSession() {
            SessionHandler.Instance.ShoppingCart = itemList;
        }

        #endregion
    }
}