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
using eStoreBLL;
using phoenixconsulting.businessentities.list;
using System;
using System.Globalization;

namespace phoenixconsulting.businessentities.helpers {
    public class WishListHelper {
        private static WishListsBLL _wishlistAdapter;

        protected static WishListsBLL WishlistAdapter {
            get { return _wishlistAdapter ?? (_wishlistAdapter = new WishListsBLL()); }
        }

        public static DTItem CreateDtItem(int id) {
            var wldt = WishlistAdapter.GetWishListItemById(id);
            return new DTItem((int)wldt.Rows[0]["DepID"], 
                              (int)wldt.Rows[0]["CatID"], 
                              (int)wldt.Rows[0]["ProdID"], 
                              (string)wldt.Rows[0]["ProdDetails"], 
                              (string)wldt.Rows[0]["ImgPath"], 
                              double.Parse(((decimal)wldt.Rows[0]["UnitPrice"]).ToString(CultureInfo.InvariantCulture)), 
                              (double)wldt.Rows[0]["ProdWeight"], 
                              (int)wldt.Rows[0]["Quantity"], 
                              (int)wldt.Rows[0]["IsOnSale"], 
                              double.Parse(((decimal)wldt.Rows[0]["DiscPrice"]).ToString(CultureInfo.InvariantCulture)), 
                              (int)wldt.Rows[0]["ColorID"], 
                              IsNull(wldt.Rows[0]["ColorName"]) 
                                ? string.Empty 
                                : (string)wldt.Rows[0]["ColorName"], 
                              (int)wldt.Rows[0]["SizeID"], 
                              IsNull(wldt.Rows[0]["SizeName"]) 
                                ? string.Empty 
                                : (string)wldt.Rows[0]["SizeName"]);
        }

        private static bool IsNull(object column) {
            return (column) is DBNull;
        }

        public static bool DeleteItem(int id) {
            return WishlistAdapter.DeleteItem(id);
        }

        public static bool UpdateQuantity(int id, int quantity) {
            return WishlistAdapter.UpdateQuantity(id, quantity);
        }
    }
}