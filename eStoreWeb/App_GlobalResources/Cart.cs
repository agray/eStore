using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

using eStoreBLL;
using eStoreWeb.Properties;

namespace eStoreWeb {
    public class Cart : Page {
        private CostsBLL _costsAdapter = null;
        protected CostsBLL CostsAdapter {
            get {
                if(_costsAdapter == null) {
                    _costsAdapter = new CostsBLL();
                }

                return _costsAdapter;
            }
        }



        //Declare module level array to store contents of shopping cart 
        private ArrayList ShoppingCart;


        private ArrayList getCartSingleton() {
            if(SessionHandler.ShoppingCart == null) {
                ShoppingCart = new ArrayList();
            } else {
                ShoppingCart = SessionHandler.ShoppingCart;
            }
            return ShoppingCart;
        }


        public void AddItem(CartItem cartItem) {
            int index = ItemIndex(cartItem.ProductId, cartItem.ColorId, cartItem.SizeId);
            if(index == -1) {
                //Doesn't already exist
                getCartSingleton().Add(cartItem);
                SessionHandler.ShoppingCart = ShoppingCart;

                ShoppingCart.TrimToSize();
            } else {
                //Does already exist - increase quantity only
                IncreaseQuantity(index, cartItem.ProductQuantity);
            }

            if(isWrapping(cartItem)) {
                SessionHandler.Wrapping = true;
            }

            CalculateTotals();
        }


        public void CalculateTotals() {
            double totalWeight = 0;
            double totalSubTotals = 0;
            int totalNumItems = 0;
            totalWeight = 0;
            totalSubTotals = 0;
            totalNumItems = 0;

            getCartSingleton();
            foreach(CartItem cartItem in ShoppingCart) {
                totalWeight = totalWeight + cartItem.ProductWeight * cartItem.ProductQuantity;
                totalSubTotals = totalSubTotals + cartItem.Subtotal;
                if(!isWrapping(cartItem)) {
                    totalNumItems = totalNumItems + cartItem.ProductQuantity;
                }
            }

            SessionHandler.TotalShipping = calculateShippingCosts(totalNumItems, totalWeight);
            SessionHandler.TotalCost = totalSubTotals + SessionHandler.TotalShipping;
            SessionHandler.TotalWeight = totalWeight;
        }


        private double calculateShippingCosts(int itemCount, double totalweight) {

            double ShippingRate = 0;

            if(totalweight == 0) {
                return 0;
            }

            ShippingRate = calculateShippingRate(totalweight);

            if(SessionHandler.ShippingCountry == Settings.Default.AustraliaCountryID) {
                //Calculate Shipping for Australia
                if(itemCount > 2) {
                    return ShippingRate * 2;
                } else {
                    return ShippingRate * itemCount;

                }
            } else {
                //Calculate International Shipping for Australia
                return ShippingRate;

            }
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        private double calculateShippingRate(double totalweight) {
            int shipCountry;
            int shippingMode;

            if(String.IsNullOrEmpty(SessionHandler.ShippingCountry)) {
                shipCountry = Int32.Parse(Settings.Default.AustraliaCountryID);
            } else {
                shipCountry = Int32.Parse(SessionHandler.ShippingCountry);
            }

            if(String.IsNullOrEmpty(SessionHandler.ShippingMode)) {
                shippingMode = Int32.Parse(Settings.Default.ModeAirMail);
            } else {
                shippingMode = Int32.Parse(SessionHandler.ShippingMode);
            }

            return (double)CostsAdapter.getShippingCosts(shipCountry,
                                                         shippingMode,
                                                         totalweight)[0]["Price"];
        }


        private void IncreaseQuantity(int index, int quantity) {
            ArrayList cart = getCartSingleton();
            CartItem cartItem = (CartItem)cart[index];

            cartItem.ProductQuantity = cartItem.ProductQuantity + quantity;

            cartItem.Subtotal = cartItem.ProductQuantity * cartItem.ProductPrice;
        }


        private int ItemIndex(int productID, int colorID, int sizeID) {
            ArrayList cart = default(ArrayList);
            CartItem cartItem = default(CartItem);
            int index = -1;
            int count = 0;

            cart = getCartSingleton();
            for(count = 0; count <= cart.Count - 1; count++) {
                cartItem = (CartItem)cart[count];
                if(cartItem.ProductId == productID & cartItem.ColorId == colorID & cartItem.SizeId == sizeID) {
                    index = count;
                    break;
                }
            }

            return index;
        }


        public void RemoveItem(int index) {

            getCartSingleton().RemoveAt(index);
            ShoppingCart.TrimToSize();

            if(SessionHandler.Wrapping) {
                if(NoWrapping()) {
                    SessionHandler.Wrapping = false;
                }
            }

            CalculateTotals();
        }


        private bool NoWrapping() {
            CartItem cartItem = default(CartItem);
            ArrayList cart = default(ArrayList);
            cart = getCartSingleton();
            int itemCount = 0;
            int wrapCount = 0;

            for(itemCount = 0; itemCount <= cart.Count - 1; itemCount++) {
                cartItem = (CartItem)cart[itemCount];
                if(isWrapping(cartItem)) {
                    wrapCount = wrapCount + 1;
                }
            }

            return wrapCount == 0;
        }

        //Does making this static break wrapping text box on checkout4?
        private static bool isWrapping(CartItem cartItem) {
            return cartItem.ProductDetails.Contains("Gift Wrapping");
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static CartItem CreateCartItem(int depId,
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
            CartItem cartItem = new CartItem();
            cartItem.DepartmentId = depId;
            cartItem.CategoryId = catId;
            cartItem.ProductId = prodId;
            cartItem.ProductDetails = productDetails;
            cartItem.ImagePath = imagePath;
            cartItem.ProductPrice = productPrice;
            cartItem.ProductWeight = productWeight;
            cartItem.ProductQuantity = productQuantity;
            cartItem.ProductOnSale = productOnSale;
            cartItem.ProductDiscountPrice = productDiscountPrice;
            cartItem.ColorId = colorId;
            cartItem.ColorName = colorName;
            cartItem.SizeId = sizeId;
            cartItem.SizeName = sizeName;
            cartItem.Subtotal = productQuantity * productPrice;

            return cartItem;
        }
    }
}