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
using System.Collections.Specialized;
using System.Security.Principal;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using NLog;

namespace phoenixconsulting.common.logging {
    public class LoggerUtil : Page {
        private static Logger auditLogger = LogManager.GetLogger("AuditLogger");

        //****************************
        // AUDIT LOGGER HELPER METHODS
        //****************************
        public static void auditLog(LogLevel ll,
                                    AuditEventType eventType,
                                    string site,
                                    string userName,
                                    string data2,
                                    string data3,
                                    string data4,
                                    string status,
                                    string exMsg) {
            LogEventInfo lei = createEventInfoObject(ll,
                                                     (int)eventType + 1000,
                                                     site,
                                                     (int)eventType,
                                                     DateTime.Now,
                                                     DateTime.Now,
                                                     userName,
                                                     data2, data3, data4, status, exMsg);
            auditLogger.Log(lei);
        }

        private static LogEventInfo createEventInfoObject(LogLevel ll,
                                                          int errorNum,
                                                          string storeName,
                                                          int typeID,
                                                          DateTime createdDate,
                                                          DateTime endDate,
                                                          string userName,
                                                          string data2,
                                                          string data3,
                                                          string data4,
                                                          string status,
                                                          string longData) {
            LogEventInfo eventInfo = new LogEventInfo(ll, auditLogger.Name, "");
            eventInfo.Properties["ErrorNum"] = errorNum;
            eventInfo.Properties["StoreName"] = storeName;
            eventInfo.Properties["TypeID"] = typeID;
            eventInfo.Properties["CreatedDate"] = createdDate;
            eventInfo.Properties["EndDate"] = endDate;
            eventInfo.Properties["Data1"] = userName;
            eventInfo.Properties["Data2"] = data2;
            eventInfo.Properties["Data3"] = data3;
            eventInfo.Properties["Data4"] = data4;
            eventInfo.Properties["Status"] = status;
            eventInfo.Properties["LongData"] = longData;
            return eventInfo;
        }

        //***********************
        // MULTIPLE ITEM METHODS
        //***********************
        public static string getText(string[] fieldNames, FormViewInsertedEventArgs e, string objectType) {
            return getObjectValues(fieldNames, e, " inserted ", objectType);
        }

        public static string getText(string[] fieldNames, FormViewUpdatedEventArgs e, string objectType) {
            return getObjectValues(fieldNames, e, " updated ", objectType);
        }

        public static string getText(string[] fieldNames, FormViewDeletedEventArgs e, string objectType) {
            return getObjectValues(fieldNames, e, " deleted ", objectType);
        }


        //***********************
        // SINGLE ITEM METHODS
        //***********************
        public static string getText(FormViewInsertedEventArgs e, string objectType) {
            return getObjectValue(e, " inserted ", objectType);
        }

        public static string getText(FormViewUpdatedEventArgs e, string objectType) {
            return getObjectValue(e, " updated ", objectType);
        }

        public static string getText(FormViewDeletedEventArgs e, string objectType) {
            return getObjectValue(e, " deleted ", objectType);
        }
        
        //***********************
        // PRIVATE METHODS
        //***********************
        private static string getObjectValues(string[] fieldNames, EventArgs e, string mode, string objectType) {
            StringBuilder sb = new StringBuilder();
            sb.Append("User " + WindowsIdentity.GetCurrent().Name + mode + objectType + ":\n");

            switch(mode) {
                case " inserted ":
                    for(int i = 0; i < fieldNames.Length; i++) {
                        sb.Append(fieldNames[i] + ": " + ((FormViewInsertedEventArgs)e).Values[i] + "\n");
                    }
                    break;
                case " updated ":
                    sb.Append("New Values:\n");
                    for(int i = 0; i < fieldNames.Length; i++) {
                        sb.Append(fieldNames[i] + ": " + ((FormViewUpdatedEventArgs)e).NewValues[i] + "\n");
                    }
                    break;
                // Display the new and original values.
                //sb.Append(GetOldAndNewValues(fieldNames, 
                //                            (OrderedDictionary)((FormViewUpdatedEventArgs)e).NewValues, 
                //                            (OrderedDictionary)((FormViewUpdatedEventArgs)e).OldValues));
                //break;
                case " deleted ":
                    if(((FormViewDeletedEventArgs)e).Values.Count != 0) {
                        for(int i = 0; i < fieldNames.Length; i++) {
                            sb.Append(fieldNames[i] + ": " + ((FormViewDeletedEventArgs)e).Values[i] + "\n");
                        }
                    } else {
                        sb.Append(objectType);
                    }
                    break;
            }

            return sb.ToString();
        }

        private static string getObjectValue(EventArgs e, string mode, string objectType) {
            StringBuilder sb = new StringBuilder("User " + WindowsIdentity.GetCurrent().Name + mode + objectType + " ");

            switch(mode) {
                case " inserted ":
                    sb.Append(((FormViewInsertedEventArgs)e).Values[0]);
                    break;
                case " updated ":
                    sb.Append(((FormViewUpdatedEventArgs)e).OldValues[0] + " to " + ((FormViewUpdatedEventArgs)e).NewValues[0]);
                    break;
                case " deleted ":
                    sb.Append(((FormViewDeletedEventArgs)e).Values[0]);
                    break;
            }

            return sb.ToString();
        }

        private static string GetOldAndNewValues(string[] fieldNames,
                                                 OrderedDictionary newValues,
                                                 OrderedDictionary oldValues) {
            // Iterate through the new and old values. Display the
            // values on the page.
            StringBuilder sb = new StringBuilder("");

            //for(int i = 0; i < fieldNames.Length; i++) {
            for(int i = 0; i < oldValues.Count; i++) {
                sb.Append(fieldNames[i] + ": " +
                          "Old Value=" + oldValues[i] +
                          ", New Value=" + newValues[i] + "\n");
            }
            return sb.ToString();
        }
    }
}