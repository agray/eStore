using System.Diagnostics.CodeAnalysis;
using eStoreBLL;
using eStoreDAL;
namespace domaintransformations.common.list {
    public class DTItem {

        #region Attributes
        //--------------------
        // ATTRIBUTES
        //--------------------
        private int _DepartmentId;
        private int _CategoryId;
        private int _ProductId;
        private string _ProductDetails;
        private string _ImagePath;
        private double _ProductPrice;
        private double _ProductWeight;
        private int _ProductQuantity;
        private int _ProductOnSale;
        private double _ProductDiscountPrice;
        private int _ColorId;
        private string _ColorName;
        private int _SizeId;
        private string _SizeName;
        private double _Subtotal;

        #endregion

        private static WishListsBLL _wishlistAdapter = null;
        protected static WishListsBLL WishlistAdapter {
            get {
                if(_wishlistAdapter == null) {
                    _wishlistAdapter = new WishListsBLL();
                }

                return _wishlistAdapter;
            }
        }

        #region Constructor
        //contructor
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public DTItem(int depId,
                      int catId,
                      int prodId,
                      string productDetails,
                      string imagePath,
                      double productPrice,
                      double productWeight,
                      int productQuantity,
                      int productOnSale,
                      double productDiscountPrice,
                      int colorId,
                      string colorName,
                      int sizeId,
                      string sizeName) {
            _DepartmentId = depId;
            _CategoryId = catId;
            _ProductId = prodId;
            _ProductDetails = productDetails;
            _ImagePath = imagePath;
            _ProductPrice = productPrice;
            _ProductWeight = productWeight;
            _ProductQuantity = productQuantity;
            _ProductOnSale = productOnSale;
            _ProductDiscountPrice = productDiscountPrice;
            _ColorId = colorId;
            _ColorName = colorName;
            _SizeId = sizeId;
            _SizeName = sizeName;

            if(productOnSale == 0) {
                _Subtotal = productQuantity * productPrice;
            } else {
                _Subtotal = productQuantity * productDiscountPrice;
            }
        }

        public static DTItem CreateDTItem(int ID) {
            DAL.WishListDataTable wldt = WishlistAdapter.getWishListItemByID(ID);
            return new DTItem((int)wldt.Rows[0]["DepID"],
                              (int)wldt.Rows[0]["CatID"],
                              (int)wldt.Rows[0]["ProdID"],
                              (string)wldt.Rows[0]["ProdDetails"],
                              (string)wldt.Rows[0]["ImgPath"],
                              double.Parse(((decimal)wldt.Rows[0]["UnitPrice"]).ToString()),
                              (double)wldt.Rows[0]["ProdWeight"],
                              (int)wldt.Rows[0]["Quantity"],
                              (int)wldt.Rows[0]["IsOnSale"],
                              double.Parse(((decimal)wldt.Rows[0]["DiscPrice"]).ToString()),
                              (int)wldt.Rows[0]["ColorID"],
                              (string)wldt.Rows[0]["ColorName"],
                              (int)wldt.Rows[0]["SizeID"],
                              (string)wldt.Rows[0]["SizeName"]);
        }
        #endregion

        public int ProductId {
            get { return _ProductId; }
            set { _ProductId = value; }
        }

        public string ProductDetails {
            get { return _ProductDetails; }
            set { _ProductDetails = value; }
        }

        public string ImagePath {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        public double ProductPrice {
            get { return _ProductPrice; }
            set { _ProductPrice = value; }
        }

        public double ProductWeight {
            get { return _ProductWeight; }
            set { _ProductWeight = value; }
        }

        public int ProductQuantity {
            get { return _ProductQuantity; }
            set { _ProductQuantity = value; }
        }

        public int ProductOnSale {
            get { return _ProductOnSale; }
            set { _ProductOnSale = value; }
        }

        public double ProductDiscountPrice {
            get { return _ProductDiscountPrice; }
            set { _ProductDiscountPrice = value; }
        }

        public int ColorId {
            get { return _ColorId; }
            set { _ColorId = value; }
        }

        public string ColorName {
            get { return _ColorName; }
            set { _ColorName = value; }
        }

        public int SizeId {
            get { return _SizeId; }
            set { _SizeId = value; }
        }

        public string SizeName {
            get { return _SizeName; }
            set { _SizeName = value; }
        }

        public int CategoryId {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }

        public int DepartmentId {
            get { return _DepartmentId; }
            set { _DepartmentId = value; }
        }

        public double Subtotal {
            get { return _Subtotal; }
            set { _Subtotal = value; }
        }
    }
}