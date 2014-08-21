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
using System.ComponentModel;
using System.Reflection;

namespace phoenixconsulting.CultureServices.enums {
    public class EnumUtils {
        public static string descriptionStringValueOf(Enum value) {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 
                   ? attributes[0].Description 
                   : value.ToString();
        }

        public static string categoryStringValueOf(Enum value) {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (CategoryAttribute[])fi.GetCustomAttributes(typeof(CategoryAttribute), false);
            return attributes.Length > 0 
                   ? attributes[0].Category 
                   : value.ToString();
        }

        public static object enumValueOf(string value, Type enumType) {
            var names = Enum.GetNames(enumType);
            foreach(var name in names) {
                if(descriptionStringValueOf((Enum)Enum.Parse(enumType, name)).Equals(value)) {
                    return Enum.Parse(enumType, name);
                }
            }

            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }
    }
}