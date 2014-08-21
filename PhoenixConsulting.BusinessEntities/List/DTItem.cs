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
using System.Diagnostics.CodeAnalysis;
namespace phoenixconsulting.businessentities.list {
    public class DTItem {

        #region Attributes
        //--------------------
        // ATTRIBUTES
        //--------------------

        #endregion

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
            DepartmentId = depId;
            CategoryId = catId;
            ProductId = prodId;
            ProductDetails = productDetails;
            ImagePath = imagePath;
            ProductPrice = productPrice;
            ProductWeight = productWeight;
            ProductQuantity = productQuantity;
            ProductOnSale = productOnSale;
            ProductDiscountPrice = productDiscountPrice;
            ColorId = colorId;
            ColorName = colorName;
            SizeId = sizeId;
            SizeName = sizeName;

            var price = productOnSale == 0 ? productPrice : productDiscountPrice;
            Subtotal = productQuantity * price;
        }
        #endregion

        #region GettersAndSetters
        //--------------------
        // GETTERS AND SETTERS
        //--------------------
        public int ProductId { get; set; }
        public string ProductDetails { get; set; }
        public string ImagePath { get; set; }
        public double ProductPrice { get; set; }
        public double ProductWeight { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductOnSale { get; set; }
        public double ProductDiscountPrice { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public double Subtotal { get; set; }

        #endregion
    }
}