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
using System.ComponentModel;
using eStoreDAL;
using eStoreDAL.DALTableAdapters;

namespace eStoreBLL {
    [DataObject]
    public class CurrenciesBLL {
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public DAL.CurrencyDataTable GetCurrencies() {
            return BLLAdapter.Instance.CurrencyAdapter.GetCurrencies();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.CurrencyDataTable getCurrencyByID(int ID) {
            return BLLAdapter.Instance.CurrencyAdapter.GetCurrencyByID(ID);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.CurrencyDataTable getCurrencyByBillingCountry(int ID) {
            return BLLAdapter.Instance.CurrencyAdapter.GetCurrencyByBillingCountryID(ID);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public bool updateExchangeRate(string value, double rate) {
            var rowsAffected = 0;
            var ca = new CurrencyTableAdapter();
            rowsAffected = (int)ca.UpdateExchangeRate(value, rate);
            return rowsAffected == 1;
        }
    }
}