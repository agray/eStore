using System.Collections;
using com.domaintransformations.common.handlers;
using domaintransformations.common.list;
using eStoreBLL;

namespace domaintransformations.common.wishlist {
    public class WishListClass : DTItemList, Hibernatable {
        #region Constructor
        public WishListClass(): base() {
            initFromSession();
        }
        #endregion

        public new void AddItem(DTItem listItem) {
            base.AddItem(listItem);
            saveToSession();
        }

        public new void RemoveItem(int index) {
            base.RemoveItem(index);
            saveToSession();
        }

        #region SessionMethods
        protected void initFromSession() {
            if(SessionHandler.WishList == null) {
                itemList = new ArrayList();
            } else {
                itemList = SessionHandler.WishList;
            }
        }

        protected void saveToSession() {
            SessionHandler.WishList = itemList;
        }
        #endregion

        #region DatabaseMethods
        protected void initFromDB() {
            
        }

        protected void addToDB() {
            WishListsBLL wishListBLL = new WishListsBLL();
            //TODO...
        }
        #endregion
    }
}