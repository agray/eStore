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
using phoenixconsulting.common.basepages;

namespace phoenixconsulting.common.handlers {
    public sealed class RequestHandler : SingletonBase<RequestHandler> {
        private RequestHandler() {}

        #region Application Variable Name Constants

        //Declare a string variable to hold the key name of the session variable
        //and use this string variable instead of typing the key name
        //in order to avoid spelling mistakes
        private const string _DepID = "DepID";
        private const string _DepName = "DepName";
        private const string _CatID = "CatID";
        private const string _CatName = "CatName";
        private const string _ProdID = "ProdID";
        private const string _ProdName = "ProdName";
        private const string _ReturnURL = "ReturnURL";

        private const string _Email = "email";
        private const string _OrderID = "orderID";
        private const string _PageID = "pageid";

        #endregion

        #region Properties And Methods

        public int DepartmentID {
            get { return getInt(_DepID); }
            set { setInt(_DepID, value); }
        }

        public string DepartmentName {
            get { return getString(_DepName); }
            set { setString(_DepName, value); }
        }

        public int CategoryID {
            get { return getInt(_CatID); }
            set { setInt(_CatID, value); }
        }

        public string CategoryName {
            get { return getString(_CatName); }
            set { setString(_CatName, value); }
        }

        public int ProductID {
            get { return getInt(_ProdID); }
            set { setInt(_ProdID, value); }
        }

        public string ProductName {
            get { return getString(_ProdName); }
            set { setString(_ProdName, value); }
        }

        public string ReturnURL {
            get { return getString(_ReturnURL); }
            set { setString(_ReturnURL, value); }
        }

        public string OrderID {
            get { return getString(_OrderID); }
            set { setString(_OrderID, value); }
        }

        public string Email {
            get { return getString(_Email); }
            set { setString(_Email, value); }
        }

        public string PageID {
            get { return getString(_PageID); }
            set { setString(_PageID, value); }
        }

        #endregion

        #region Convenience Methods

        private void setInt(string variableName, int val) {
            setInt(variableName, HTTPScope.REQUEST, val);
        }

        private void setString(string variableName, string val) {
            setString(variableName, HTTPScope.REQUEST, val);
        }

        private void setDouble(string variableName, double val) {
            setDouble(variableName, HTTPScope.REQUEST, val);
        }

        private void setBool(string variableName, bool val) {
            setBool(variableName, HTTPScope.REQUEST, val);
        }

        private int getInt(string variableName) {
            return getInt(variableName, HTTPScope.REQUEST);
        }

        private string getString(string variableName) {
            return getString(variableName, HTTPScope.REQUEST);
        }

        private double getDouble(string variableName) {
            return getDouble(variableName, HTTPScope.REQUEST);
        }

        private bool getBool(string variableName) {
            return getBool(variableName, HTTPScope.REQUEST);
        }

        #endregion
    }
}