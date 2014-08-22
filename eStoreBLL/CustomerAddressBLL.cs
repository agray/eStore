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
using System.Linq;
using eStoreDAL;
using phoenixconsulting.businessentities.account;
using phoenixconsulting.common.enums.account;

namespace eStoreBLL {
    [DataObject]
    public class CustomerAddressBLL {
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public DTAddress[] GetCustomerAddresses(Guid userId) {
            var cadt = BLLAdapter.Instance.CustomerAddressAdapter.GetCustomerAddresses(userId);
            return cadt.Select(row => new DTAddress(int.Parse(row["ID"].ToString()), 
                                                    (AddressType)int.Parse(row["AddressType"].ToString()), 
                                                    row["FirstName"].ToString(), 
                                                    row["LastName"].ToString(), 
                                                    row["Address"].ToString(), 
                                                    row["SuburbCity"].ToString(), 
                                                    row["StateProvinceRegion"].ToString(), 
                                                    row["ZipPostcode"].ToString(), 
                                                    int.Parse(row["CountryID"].ToString()))).ToArray();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public DTAddress GetCustomerBillingAddress(Guid userId) {
            var cadt = BLLAdapter.Instance.CustomerAddressAdapter.GetCustomerBillingAddress(userId);
            if(cadt.Rows.Count == 1) {
                var row = (DAL.CustomerAddressRow)cadt.Rows[0];
                var type = (AddressType)int.Parse(row["AddressType"].ToString());
                return type == AddressType.BILLING
                           ? new DTAddress(int.Parse(row["ID"].ToString()),
                                           type,
                                           row["FirstName"].ToString(),
                                           row["LastName"].ToString(),
                                           row["Address"].ToString(),
                                           row["SuburbCity"].ToString(),
                                           row["StateProvinceRegion"].ToString(),
                                           row["ZipPostcode"].ToString(),
                                           int.Parse(row["CountryID"].ToString()))
                           : null;
            }
            return null;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public DTAddress GetCustomerShippingAddress(Guid userId) {
            var cadt = BLLAdapter.Instance.CustomerAddressAdapter.GetCustomerShippingAddress(userId);
            if(cadt.Rows.Count == 1) {
                var row = (DAL.CustomerAddressRow)cadt.Rows[0];
                var type = (AddressType)int.Parse(row["AddressType"].ToString());
                return type == AddressType.SHIPPING
                           ? new DTAddress(int.Parse(row["ID"].ToString()),
                                           type,
                                           row["FirstName"].ToString(),
                                           row["LastName"].ToString(),
                                           row["Address"].ToString(),
                                           row["SuburbCity"].ToString(),
                                           row["StateProvinceRegion"].ToString(),
                                           row["ZipPostcode"].ToString(),
                                           int.Parse(row["CountryID"].ToString()))
                           : null;
            }
            return null;
        }

        public void SaveBillingAddress(Guid userId,
                                       string billingFirstName,
                                       string billingLastName,
                                       string billingAddress,
                                       string billingSuburbCity,
                                       string billingStateProvinceRegion,
                                       string billingPostcode,
                                       int billingCountry) {
            var dbBillingAddress = GetCustomerBillingAddress(userId);
            if(dbBillingAddress != null) {
                //update existing billing address
                UpdateAddress(new DTAddress(dbBillingAddress.Id, AddressType.BILLING,
                                            billingFirstName, billingLastName,
                                            billingAddress, billingSuburbCity,
                                            billingStateProvinceRegion, billingPostcode,
                                            billingCountry));
            } else {
                //insert new billing address
                AddAddress(userId, new DTAddress(0, AddressType.BILLING,
                                                 billingFirstName, billingLastName,
                                                 billingAddress, billingSuburbCity,
                                                 billingStateProvinceRegion, billingPostcode,
                                                 billingCountry));
            }
        }

        public void SaveShippingAddress(Guid userId,
                                        string shippingFirstName,
                                        string shippingLastName,
                                        string shippingAddress,
                                        string shippingSuburbCity,
                                        string shippingStateProvinceRegion,
                                        string shippingPostcode,
                                        int shippingCountry) {
            var dbShippingAddress = GetCustomerShippingAddress(userId);
            if(dbShippingAddress != null) {
                //update existing billing address
                UpdateAddress(new DTAddress(dbShippingAddress.Id, AddressType.SHIPPING,
                                            shippingFirstName, shippingLastName,
                                            shippingAddress, shippingSuburbCity,
                                            shippingStateProvinceRegion, shippingPostcode,
                                            shippingCountry));
            } else {
                //insert new shipping address
                AddAddress(userId, new DTAddress(0, AddressType.SHIPPING,
                                                 shippingFirstName, shippingLastName,
                                                 shippingAddress, shippingSuburbCity,
                                                 shippingStateProvinceRegion, shippingPostcode,
                                                 shippingCountry));
            }
        }

        public bool IsSameAddress(DAL.CustomerAddressDataTable cadt) {
            return cadt.Rows.Count < 2 || cadt[0].GetHashCode() == cadt[1].GetHashCode();
        }

        public bool SaveAddresses(Guid userId,
                                  int currentAddressCount,
                                  DTAddress billingAddress,
                                  DTAddress shippingAddress) {
            switch(currentAddressCount) {
                case 0:
                    //Add billingAddress and ShippingAddress
                    return AddBothAddresses(userId, billingAddress, shippingAddress);
                //break;
                case 1:
                    if(GetDbAddressType(userId) == AddressType.BILLING) {
                        return UpdateAddress(billingAddress) && AddAddress(userId, shippingAddress);
                    }
                    return UpdateAddress(shippingAddress) && AddAddress(userId, billingAddress);
                //break;
                case 2:
                    //Update billingAddress and ShippingAddress
                    return UpdateBothAddresses(billingAddress, shippingAddress);
                //break;
                default:
                    return false;
            }
        }

        private static AddressType GetDbAddressType(Guid userId) {
            //Only one address in the DB what is its type?
            var ca = new CustomerAddressBLL();
            var addresses = ca.GetCustomerAddresses(userId);
            return addresses[0].AddressType;
        }

        private bool AddBothAddresses(Guid userId, DTAddress billingAddress, DTAddress shippingAddress) {
            return AddAddress(userId, billingAddress) && AddAddress(userId, shippingAddress);
        }

        private bool UpdateBothAddresses(DTAddress billingAddress, DTAddress shippingAddress) {
            return UpdateAddress(billingAddress) && UpdateAddress(shippingAddress);
        }

        public bool AddAddress(Guid userId, DTAddress address) {
            var addresses = new DAL.CustomerAddressDataTable();

            //Create a new CustomerAddressRow instance
            var customerAddress = addresses.NewCustomerAddressRow();

            customerAddress.UserID = userId;
            customerAddress.AddressType = (int)address.AddressType;
            customerAddress.FirstName = address.FirstName;
            customerAddress.LastName = address.LastName;
            customerAddress.Address = address.StreetAddress;
            customerAddress.SuburbCity = address.SuburbCity;
            customerAddress.StateProvinceRegion = address.StateProvinceRegion;
            customerAddress.ZipPostcode = address.ZipPostCode;
            customerAddress.CountryID = address.CountryId;

            //Add the new CustomerAddress
            addresses.AddCustomerAddressRow(customerAddress);
            var rowsAffected = BLLAdapter.Instance.CustomerAddressAdapter.Update(addresses);

            //Return True if exactly one row was inserted, otherwise False
            return rowsAffected == 1;
        }

        public bool UpdateAddress(DTAddress address) {
            var addresses = BLLAdapter.Instance.CustomerAddressAdapter.GetCustomerAddressByID(address.Id);

            if(addresses.Count == 0) {
                //No matching records found, return false
                return false;
            }

            addresses.Rows[0]["AddressType"] = (int)address.AddressType;
            addresses.Rows[0]["FirstName"] = address.FirstName;
            addresses.Rows[0]["LastName"] = address.LastName;
            addresses.Rows[0]["Address"] = address.StreetAddress;
            addresses.Rows[0]["SuburbCity"] = address.SuburbCity;
            addresses.Rows[0]["StateProvinceRegion"] = address.StateProvinceRegion;
            addresses.Rows[0]["ZipPostcode"] = address.ZipPostCode;
            addresses.Rows[0]["CountryID"] = address.CountryId;

            //Update the customer address record
            //Return True if exactly one row was updated, otherwise False
            return BLLAdapter.Instance.CustomerAddressAdapter.Update(addresses) == 1;
        }
    }
}