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
using System.ComponentModel;
using eStoreDAL;

namespace eStoreBLL {
    [DataObject]
    public class ProductsBLL {
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public DAL.ProductDataTable getProducts() {
            return BLLAdapter.Instance.ProductAdapter.GetProducts();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.ProductDataTable getProductsByCategoryID(int categoryID) {
            return BLLAdapter.Instance.ProductAdapter.GetProductsByCategory(categoryID);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.ProductDataTable getProductsByCategoryIDAndCurrencyID(int categoryID, int currencyID) {
            return BLLAdapter.Instance.ProductAdapter.GetProductsByCategoryAndCurrency(categoryID, currencyID);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.ProductDataTable getProductByIDAndCurrencyID(int ID, int currencyID) {
            return BLLAdapter.Instance.ProductAdapter.GetProductByIDAndCurrency(ID, currencyID);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.ProductDataTable getProductByID(int ID) {
            return BLLAdapter.Instance.ProductAdapter.GetProductByID(ID);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.ProductDataTable getSearchResults(String query, int currencyID) {
            return BLLAdapter.Instance.ProductAdapter.GetSearchResults(query, currencyID);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, false)]
        public int incrementNumViews(int ID) {
            return (int)BLLAdapter.Instance.ProductAdapter.ProductIncrementNumViews(ID);
        }

        public string getProductPageTitle(int ID) {
            return BLLAdapter.Instance.ProductAdapter.GetProductPageTitle(ID).ToString();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public ArrayList getSEODetails(int ID) {
            return new DataReader().getProductSEODetails(ID);
        }
    }
}