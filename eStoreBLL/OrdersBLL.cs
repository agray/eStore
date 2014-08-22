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
using System.ComponentModel;
using eStoreDAL;

namespace eStoreBLL {
    [DataObject]
    public class OrdersBLL {
        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.OrderDataTable GetOrderHistory(Guid userId) {
            return BLLAdapter.Instance.OrderAdapter.GetOrderHistory(userId);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.OrderDetailDataTable GetOrderDetailsByOrderId(int orderId) {
            return BLLAdapter.Instance.OrderDetailAdapter.GetOrderDetailsByOrderID(orderId);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public int AddOrder(string emailAddress, string billingFirstName, string billingLastName, string billingAddress, string billingSuburbCity, string billingStateProvinceRegion, string billingZipPostcode, int billingCountryId, string shippingFirstName, string shippingLastName, string shippingAddress, string shippingSuburbCity, string shippingStateProvinceRegion, string shippingZipPostcode, int shippingCountryId, int shippingModeId, double shippingCost, string orderDetailXml, string orderComments, string giftTagComments, double totalCost, Guid userId) {
            var orderId = (int)BLLAdapter.Instance.OrderAdapter.AddOrder(emailAddress, billingFirstName, billingLastName, billingAddress, 
                                                                         billingSuburbCity, billingStateProvinceRegion, 
                                                                         billingZipPostcode, billingCountryId, shippingFirstName, 
                                                                         shippingLastName, shippingAddress, shippingSuburbCity, 
                                                                         shippingStateProvinceRegion, shippingZipPostcode, 
                                                                         shippingCountryId, shippingModeId, shippingCost, 
                                                                         orderDetailXml, orderComments, giftTagComments, totalCost, 
                                                                         userId);
            return orderId;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public int updateOrderStatus(int ID, int responseCode, string txnID, string settlementDate, string PayPal_Ack, string PayPal_CorrelationID, string PayPal_TimeStamp, double PayPal_FeeAmount, string PayPal_PaymentStatus, string PayPal_ReasonCode, string PayPal_PaymentDate) {
            return (int)BLLAdapter.Instance.OrderAdapter.OrderStatusUpdate(ID, responseCode, txnID, settlementDate, PayPal_Ack, 
                                                       PayPal_CorrelationID, PayPal_TimeStamp, PayPal_FeeAmount, 
                                                       PayPal_PaymentStatus, PayPal_ReasonCode, PayPal_PaymentDate);
        }
    }
}