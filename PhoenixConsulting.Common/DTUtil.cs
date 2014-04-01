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
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using phoenixconsulting.common.handlers;
using PhoenixConsulting.Common.Properties;

namespace eStoreWeb {
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    public class DTUtil : Page {
        public static string EmitSizeInformation(string sizeName) {
            return !String.IsNullOrEmpty(sizeName) 
                   ? "<BR>Size: " + sizeName 
                   : "";
        }

        public static string EmitColorInformation(string colorName) {
            return !String.IsNullOrEmpty(colorName) 
                   ? "<BR>Color: " + colorName 
                   : "";
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public static string EmitSubtotal(double subtotal) {
            return ((subtotal * SessionHandler.Instance.CurrencyXRate).ToString("C"));
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public static string EmitShippingModeName(int mode) {
            return mode.ToString() == Settings.Default.ModeExpressAirMail 
                   ? "EXPRESS" 
                   : (mode.ToString() == Settings.Default.ModeAirMail 
                   ? "AIRMAIL" 
                   : "UPS");
        }

        public static string EmitTotalLabel() {
            return SessionHandler.Instance.Currency == Settings.Default.AustralianCurrencyID 
                   ? "Total (Inc GST)" 
                   : "Total (Ex GST)";
        }
    }
}