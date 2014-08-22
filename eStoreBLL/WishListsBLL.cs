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
    public class WishListsBLL {
        

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public DAL.WishListDataTable GetWishListByUserId(Guid guid) {
            return BLLAdapter.Instance.WishListAdapter.GetWishListByUserID(guid);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.WishListDataTable GetWishListItemById(int id) {
            return BLLAdapter.Instance.WishListAdapter.GetWishListByID(id);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public bool AddItem(Guid userId, int prodId, int quantity, int colorId, int sizeId) {
            return BLLAdapter.Instance.WishListAdapter.Insert(userId, prodId, quantity, colorId, sizeId) == 1;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public bool UpdateItem(int originalId, Guid userId, int prodId, int sizeId, int colorId, int quantity) {
            var wishlists = BLLAdapter.Instance.WishListAdapter.GetWishListByID(originalId);

            if(wishlists.Count == 0) {
                //No matching records found, return false
                return false;
            }

            wishlists.Rows[0]["UserID"] = userId;
            wishlists.Rows[0]["ProdID"] = prodId;
            wishlists.Rows[0]["SizeID"] = sizeId;
            wishlists.Rows[0]["ColorID"] = colorId;
            wishlists.Rows[0]["Quantity"] = quantity;

            //Update the wishlist records
            //Return True if exactly one row was updated, otherwise False   
            return BLLAdapter.Instance.WishListAdapter.Update(wishlists) == 1;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, false)]
        public bool IncreaseQuantity(Guid userId, int prodId, int sizeId, int colorId, int quantity) {
            //Increase the quantity of the WishList item
            var rowsAffected = (int)BLLAdapter.Instance.WishListAdapter.WishListIncreaseQuantity(userId, prodId, sizeId, colorId, quantity);

            //Return True if exactly one row was updated, otherwise False
            return rowsAffected == 1;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, false)]
        public bool UpdateQuantity(int original_ID, int quantity) {
            //Increase the quantity of the WishList item
            var rowsAffected = (int)BLLAdapter.Instance.WishListAdapter.WishListUpdateQuantity(original_ID, quantity);

            //Return True if exactly one row was updated, otherwise False
            return rowsAffected == 1;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public bool DeleteItem(int original_ID) {
            //Update the WishList record
            //Return True if exactly one row was deleted, otherwise False   
            return BLLAdapter.Instance.WishListAdapter.Delete(original_ID) == 1;
        }
    }
}