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
using System;
using System.Globalization;
using System.Threading;
using phoenixconsulting.CultureServices.enums;

namespace com.phoenixconsulting.culture {
    public class CultureService {
        public static string toLocalCulture(double d) {
            var numberInfo = CultureInfo.InstalledUICulture.NumberFormat;
            return d.ToString("c", numberInfo);
        }

        public static double getLocalCultureDouble(string s) {
            return double.Parse(s, CultureInfo.InvariantCulture);
        }

        public static string toInternalLocalCulture(double d) {
            var numberInfo = CultureInfo.InstalledUICulture.NumberFormat;
            var temp = d.ToString("c", numberInfo);
            var length = temp.Length;
            return temp.Substring(1, length-1);
        }

        public static string getConvertedPrice(string rawValue,
                                               double xRate,
                                               string targetCurrency) {
            //do Calculation in AUS culture
            var convertedPrice = double.Parse(rawValue) * xRate;

            //change back to target culture
            SetCulture(targetCurrency);

            var numberInfo = CultureInfo.CurrentCulture.NumberFormat;
            var convertedPriceString = convertedPrice.ToString("c", numberInfo);

            //change to AUS culture for next calculation
            SetCulture("AUD");

            return convertedPriceString;
        }

        public static string getCurrencySymbol(){
            return Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol;
        }

        public static void SetCulture(string currencyValue) {
            CultureCode cc;
            if(Enum.IsDefined(typeof(CultureCode), currencyValue)) {
                cc = (CultureCode)Enum.Parse(typeof(CultureCode), currencyValue, true);
                ConfigureCultureInfo(cc);
            } else {
                ConfigureCultureInfo(CultureCode.AUD);
            }
        }

        private static void ConfigureCultureInfo(CultureCode cc) {
            var cultureCode = EnumUtils.descriptionStringValueOf(cc);
            var currencySymbol = EnumUtils.categoryStringValueOf(cc);

            var cultureInfo = new CultureInfo(cultureCode, false);
            cultureInfo.NumberFormat.CurrencyPositivePattern = 0;

            if(!currencySymbol.Equals("null")) {
                cultureInfo.NumberFormat.CurrencySymbol = currencySymbol;
            }

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}