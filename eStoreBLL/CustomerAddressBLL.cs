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
using System.Collections.Generic;
using System.ComponentModel;
using eStoreDAL;
using phoenixconsulting.businessentities.account;
using phoenixconsulting.common.enums.account;

namespace eStoreBLL {
    [DataObject]
    public class CustomerAddressBLL {
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public DTAddress[] getCustomerAddresses(Guid userID) {
            List<DTAddress> addresses = new List<DTAddress>();
            DAL.CustomerAddressDataTable cadt = BLLAdapter.Instance.CustomerAddressAdapter.GetCustomerAddresses(userID);
            foreach(DAL.CustomerAddressRow row in cadt) {
                addresses.Add(new DTAddress(int.Parse(row["ID"].ToString()),
                                            (AddressType)int.Parse(row["AddressType"].ToString()),
                                            row["FirstName"].ToString(),
                                            row["LastName"].ToString(),
                                            row["Address"].ToString(),
                                            row["SuburbCity"].ToString(),
                                            row["StateProvinceRegion"].ToString(),
                                            row["ZipPostcode"].ToString(),
                                            int.Parse(row["CountryID"].ToString())));
            }
            return addresses.ToArray();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public DTAddress getCustomerBillingAddress(Guid userID) {
            DAL.CustomerAddressDataTable cadt = BLLAdapter.Instance.CustomerAddressAdapter.GetCustomerBillingAddress(userID);
            if(cadt.Rows.Count == 1) {
                DAL.CustomerAddressRow row = (DAL.CustomerAddressRow)cadt.Rows[0];
                AddressType type = (AddressType)int.Parse(row["AddressType"].ToString());
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
        public DTAddress getCustomerShippingAddress(Guid userID) {
            DAL.CustomerAddressDataTable cadt = BLLAdapter.Instance.CustomerAddressAdapter.GetCustomerShippingAddress(userID);
            if(cadt.Rows.Count == 1) {
                DAL.CustomerAddressRow row = (DAL.CustomerAddressRow)cadt.Rows[0];
                AddressType type = (AddressType)int.Parse(row["AddressType"].ToString());
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

        public void saveBillingAddress(Guid userID,
                                       string billingFirstName,
                                       string billingLastName,
                                       string billingAddress,
                                       string billingSuburbCity,
                                       string billingStateProvinceRegion,
                                       string billingPostcode,
                                       int billingCountry) {
            DTAddress DB_BillingAddress = getCustomerBillingAddress(userID);
            if(DB_BillingAddress != null) {
                //update existing billing address
                updateAddress(new DTAddress(DB_BillingAddress.Id, AddressType.BILLING,
                                            billingFirstName, billingLastName,
                                            billingAddress, billingSuburbCity,
                                            billingStateProvinceRegion, billingPostcode,
                                            billingCountry));
            } else {
                //insert new billing address
                addAddress(userID, new DTAddress(0, AddressType.BILLING,
                                                 billingFirstName, billingLastName,
                                                 billingAddress, billingSuburbCity,
                                                 billingStateProvinceRegion, billingPostcode,
                                                 billingCountry));
            }
        }

        public void saveShippingAddress(Guid userID,
                                        string shippingFirstName,
                                        string shippingLastName,
                                        string shippingAddress,
                                        string shippingSuburbCity,
                                        string shippingStateProvinceRegion,
                                        string shippingPostcode,
                                        int shippingCountry) {
            DTAddress DB_ShippingAddress = getCustomerShippingAddress(userID);
            if(DB_ShippingAddress != null) {
                //update existing billing address
                updateAddress(new DTAddress(DB_ShippingAddress.Id, AddressType.SHIPPING,
                                            shippingFirstName, shippingLastName,
                                            shippingAddress, shippingSuburbCity,
                                            shippingStateProvinceRegion, shippingPostcode,
                                            shippingCountry));
            } else {
                //insert new shipping address
                addAddress(userID, new DTAddress(0, AddressType.SHIPPING,
                                                 shippingFirstName, shippingLastName,
                                                 shippingAddress, shippingSuburbCity,
                                                 shippingStateProvinceRegion, shippingPostcode,
                                                 shippingCountry));
            }
        }

        public bool isSameAddress(DAL.CustomerAddressDataTable cadt) {
            return cadt.Rows.Count < 2 || cadt[0].GetHashCode() == cadt[1].GetHashCode();
        }

        public bool saveAddresses(Guid userID,
                                  int currentAddressCount,
                                  DTAddress billingAddress,
                                  DTAddress shippingAddress) {
            switch(currentAddressCount) {
                case 0:
                    //Add billingAddress and ShippingAddress
                    return addBothAddresses(userID, billingAddress, shippingAddress);
                //break;
                case 1:
                    if(getDBAddressType(userID) == AddressType.BILLING) {
                        return updateAddress(billingAddress) && addAddress(userID, shippingAddress);
                    }
                    return updateAddress(shippingAddress) && addAddress(userID, billingAddress);
                //break;
                case 2:
                    //Update billingAddress and ShippingAddress
                    return updateBothAddresses(billingAddress, shippingAddress);
                //break;
                default:
                    return false;
            }
        }

        private AddressType getDBAddressType(Guid userID) {
            //Only one address in the DB what is its type?
            CustomerAddressBLL ca = new CustomerAddressBLL();
            DTAddress[] addresses = ca.getCustomerAddresses(userID);
            return addresses[0].AddressType;
        }

        private bool addBothAddresses(Guid userID, DTAddress billingAddress, DTAddress shippingAddress) {
            return addAddress(userID, billingAddress) && addAddress(userID, shippingAddress);
        }

        private bool updateBothAddresses(DTAddress billingAddress, DTAddress shippingAddress) {
            return updateAddress(billingAddress) && updateAddress(shippingAddress);
        }

        public bool addAddress(Guid userID, DTAddress address) {
            DAL.CustomerAddressDataTable addresses = new DAL.CustomerAddressDataTable();

            //Create a new CustomerAddressRow instance
            DAL.CustomerAddressRow customerAddress = addresses.NewCustomerAddressRow();

            customerAddress.UserID = userID;
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
            int rowsAffected = BLLAdapter.Instance.CustomerAddressAdapter.Update(addresses);

            //Return True if exactly one row was inserted, otherwise False

            return rowsAffected == 1;
        }

        public bool updateAddress(DTAddress address) {
            DAL.CustomerAddressDataTable addresses = BLLAdapter.Instance.CustomerAddressAdapter.GetCustomerAddressByID(address.Id);

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