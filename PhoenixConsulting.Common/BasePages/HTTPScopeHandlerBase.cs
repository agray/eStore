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
using System.Collections;
using System.Data;
using System.Web;
using System.Web.SessionState;
using phoenixconsulting.common.handlers;
using PhoenixConsulting.PaymentService.PayPal.com.paypal.sandbox.www;

namespace phoenixconsulting.common.basepages {
    public class HTTPScopeHandlerBase : BasePage {
        #region HTTP Scopes

        protected HttpApplicationState ApplicationState {
            get { return GetCurrentContext().Application; }
        }

        protected HttpSessionState SessionState {
            get { return GetCurrentContext().Session; }
        }

        protected HttpRequest RequestObject {
            get { return GetCurrentContext().Request; }
        }

        protected HttpResponse ResponseObject {
            get { return GetCurrentContext().Response; }
        }

        #endregion

        #region Setters

        protected void setInt(string variableName, HTTPScope scope, int val) {
            setVar(variableName, scope, val);
        }

        protected void setString(string variableName, HTTPScope scope, string val) {
            setVar(variableName, scope, val);
        }

        protected void setDouble(string variableName, HTTPScope scope, double val) {
            setVar(variableName, scope, val);
        }

        protected void setBool(string variableName, HTTPScope scope, bool val) {
            setVar(variableName, scope, val);
        }

        protected void setArrayList(string variableName, HTTPScope scope, ArrayList val) {
            setVar(variableName, scope, val);
        }

        protected void setDataSet(string variableName, HTTPScope scope, DataSet val) {
            setVar(variableName, scope, val);
        }

        protected void setECDResponseType(string variableName, HTTPScope scope, GetExpressCheckoutDetailsResponseType val) {
            setVar(variableName, scope, val);
        }

        protected void setECDResponseDetailsType(string variableName, HTTPScope scope, GetExpressCheckoutDetailsResponseDetailsType val) {
            setVar(variableName, scope, val);
        }

        private void setVar(string variableName, HTTPScope scope, object objVal) {
            switch(scope) {
                case HTTPScope.APPLICATION:
                    ApplicationState[variableName] = objVal;
                    break;
                case HTTPScope.SESSION:
                    SessionState[variableName] = objVal;
                    break;
            }
        }

        #endregion

        #region Getters

        protected int getInt(string variableName, HTTPScope scope) {
            var check = getScopedValue(variableName, scope);
            return (int)returnVal(check, typeof(int));
        }

        protected string getString(string variableName, HTTPScope scope) {
            var check = getScopedValue(variableName, scope);
            return (string)returnVal(check, typeof(string));
        }

        protected double getDouble(string variableName, HTTPScope scope) {
            var check = getScopedValue(variableName, scope);
            //Check for null first
            return check == null ? 0 : (double)check;
        }

        protected bool getBool(string variableName, HTTPScope scope) {
            var check = getScopedValue(variableName, scope);
            return (bool)returnVal(check, typeof(bool));
        }

        protected ArrayList getArrayList(string variableName, HTTPScope scope) {
            var check = getScopedValue(variableName, scope);
            return (ArrayList)returnVal(check, typeof(ArrayList));
        }

        protected DataSet getDataSet(string variableName, HTTPScope scope) {
            var check = getScopedValue(variableName, scope);
            return (DataSet)returnVal(check, typeof(DataSet));
        }

        protected GetExpressCheckoutDetailsResponseType getECDResponseType(string variableName, HTTPScope scope) {
            var check = getScopedValue(variableName, scope);
            return (GetExpressCheckoutDetailsResponseType)returnVal(check, typeof(GetExpressCheckoutDetailsResponseType));
        }

        protected GetExpressCheckoutDetailsResponseDetailsType getECDResponseDetailsType(string variableName, HTTPScope scope) {
            var check = getScopedValue(variableName, scope);
            return (GetExpressCheckoutDetailsResponseDetailsType)returnVal(check, typeof(GetExpressCheckoutDetailsResponseDetailsType));
        }

        private object getScopedValue(string variableName, HTTPScope scope) {
            switch(scope) {
                case HTTPScope.APPLICATION:
                    return ApplicationState[variableName];
                case HTTPScope.SESSION:
                    return SessionState[variableName];
                case HTTPScope.REQUEST:
                    return RequestObject[variableName];
                default:
                    return null;
            }
        }

        private object returnVal(object val, Type t) {
            if(t == typeof(int)) {
                return getInt(val);
            }
            if(t == typeof(double)) {
                return getDouble(val);
            }
            if(t == typeof(string)) {
                return getString(val);
            }
            if(t == typeof(bool)) {
                return getBool(val);
            }
            if(t == typeof(ArrayList)) {
                return getArrayList(val);
            }
            if(t == typeof(DataSet)) {
                return getDataSet(val);
            }
            if(t == typeof(GetExpressCheckoutDetailsResponseType)) {
                return getECDRType(val);
            }
            if(t == typeof(GetExpressCheckoutDetailsResponseDetailsType)) {
                return getECDRDType(val);
            }
            //should never get to this
            return string.Empty;
        }

        private int getInt(object val) {
            return val == null ? 0 : int.Parse(val.ToString());
        }

        private double getDouble(object val) {
            return val == null ? 0 : double.Parse(val.ToString());
        }

        private bool getBool(object val) {
            return val != null && (bool)val;
        }

        private string getString(object val) {
            return val == null ? null : val.ToString();
        }

        private ArrayList getArrayList(object val) {
            return val == null ? null : (ArrayList)val;
        }

        private DataSet getDataSet(object val) {
            return val == null ? null : (DataSet)val;
        }

        private GetExpressCheckoutDetailsResponseType getECDRType(object val) {
            return val == null ? null : (GetExpressCheckoutDetailsResponseType)val;
        }

        private GetExpressCheckoutDetailsResponseDetailsType getECDRDType(object val) {
            return val == null ? null : (GetExpressCheckoutDetailsResponseDetailsType)val;
        }

        #endregion
    }
}