namespace eStoreWeb {
    public class CartItem {

        //--------------------
        // CART CONSTANTS
        //--------------------

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
        private int _CategoryID;
        private int _DepartmentId;
        private double _Subtotal;

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
            get { return _CategoryID; }
            set { _CategoryID = value; }
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