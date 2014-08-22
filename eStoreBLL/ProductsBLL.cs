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
        public DAL.ProductDataTable GetProducts() {
            return BLLAdapter.Instance.ProductAdapter.GetProducts();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.ProductDataTable GetProductsByCategoryId(int categoryId) {
            return BLLAdapter.Instance.ProductAdapter.GetProductsByCategory(categoryId);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.ProductDataTable GetProductsByCategoryIdAndCurrencyId(int categoryId, int currencyId) {
            return BLLAdapter.Instance.ProductAdapter.GetProductsByCategoryAndCurrency(categoryId, currencyId);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.ProductDataTable GetProductByIdAndCurrencyId(int id, int currencyId) {
            return BLLAdapter.Instance.ProductAdapter.GetProductByIDAndCurrency(id, currencyId);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.ProductDataTable GetProductById(int id) {
            return BLLAdapter.Instance.ProductAdapter.GetProductByID(id);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.ProductDataTable GetSearchResults(String query, int currencyId) {
            return BLLAdapter.Instance.ProductAdapter.GetSearchResults(query, currencyId);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, false)]
        public int IncrementNumViews(int id) {
            return (int)BLLAdapter.Instance.ProductAdapter.ProductIncrementNumViews(id);
        }

        public string GetProductPageTitle(int id) {
            return BLLAdapter.Instance.ProductAdapter.GetProductPageTitle(id).ToString();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public ArrayList GetSeoDetails(int id) {
            return new DataReader().GetProductSeoDetails(id);
        }
    }
}