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

        protected void SetInt(string variableName, HTTPScope scope, int val) {
            SetVar(variableName, scope, val);
        }

        protected void SetString(string variableName, HTTPScope scope, string val) {
            SetVar(variableName, scope, val);
        }

        protected void SetDouble(string variableName, HTTPScope scope, double val) {
            SetVar(variableName, scope, val);
        }

        protected void SetBool(string variableName, HTTPScope scope, bool val) {
            SetVar(variableName, scope, val);
        }

        protected void SetArrayList(string variableName, HTTPScope scope, ArrayList val) {
            SetVar(variableName, scope, val);
        }

        protected void SetDataSet(string variableName, HTTPScope scope, DataSet val) {
            SetVar(variableName, scope, val);
        }

        protected void SetEcdResponseType(string variableName, HTTPScope scope, GetExpressCheckoutDetailsResponseType val) {
            SetVar(variableName, scope, val);
        }

        protected void SetEcdResponseDetailsType(string variableName, HTTPScope scope, GetExpressCheckoutDetailsResponseDetailsType val) {
            SetVar(variableName, scope, val);
        }

        private void SetVar(string variableName, HTTPScope scope, object objVal) {
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

        protected int GetInt(string variableName, HTTPScope scope) {
            var check = GetScopedValue(variableName, scope);
            return (int)ReturnVal(check, typeof(int));
        }

        protected string GetString(string variableName, HTTPScope scope) {
            var check = GetScopedValue(variableName, scope);
            return (string)ReturnVal(check, typeof(string));
        }

        protected double GetDouble(string variableName, HTTPScope scope) {
            var check = GetScopedValue(variableName, scope);
            //Check for null first
            return check == null ? 0 : (double)check;
        }

        protected bool GetBool(string variableName, HTTPScope scope) {
            var check = GetScopedValue(variableName, scope);
            return (bool)ReturnVal(check, typeof(bool));
        }

        protected ArrayList GetArrayList(string variableName, HTTPScope scope) {
            var check = GetScopedValue(variableName, scope);
            return (ArrayList)ReturnVal(check, typeof(ArrayList));
        }

        protected DataSet GetDataSet(string variableName, HTTPScope scope) {
            var check = GetScopedValue(variableName, scope);
            return (DataSet)ReturnVal(check, typeof(DataSet));
        }

        protected GetExpressCheckoutDetailsResponseType GetEcdResponseType(string variableName, HTTPScope scope) {
            var check = GetScopedValue(variableName, scope);
            return (GetExpressCheckoutDetailsResponseType)ReturnVal(check, typeof(GetExpressCheckoutDetailsResponseType));
        }

        protected GetExpressCheckoutDetailsResponseDetailsType GetEcdResponseDetailsType(string variableName, HTTPScope scope) {
            var check = GetScopedValue(variableName, scope);
            return (GetExpressCheckoutDetailsResponseDetailsType)ReturnVal(check, typeof(GetExpressCheckoutDetailsResponseDetailsType));
        }

        private object GetScopedValue(string variableName, HTTPScope scope) {
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

        private object ReturnVal(object val, Type t) {
            if(t == typeof(int)) {
                return GetInt(val);
            }
            if(t == typeof(double)) {
                return GetDouble(val);
            }
            if(t == typeof(string)) {
                return GetString(val);
            }
            if(t == typeof(bool)) {
                return GetBool(val);
            }
            if(t == typeof(ArrayList)) {
                return GetArrayList(val);
            }
            if(t == typeof(DataSet)) {
                return GetDataSet(val);
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

        private int GetInt(object val) {
            return val == null ? 0 : int.Parse(val.ToString());
        }

        private double GetDouble(object val) {
            return val == null ? 0 : double.Parse(val.ToString());
        }

        private bool GetBool(object val) {
            return val != null && (bool)val;
        }

        private string GetString(object val) {
            return val == null ? null : val.ToString();
        }

        private ArrayList GetArrayList(object val) {
            return val == null ? null : (ArrayList)val;
        }

        private DataSet GetDataSet(object val) {
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