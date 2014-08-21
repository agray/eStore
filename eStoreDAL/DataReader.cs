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
using System.Data.SqlClient;
using eStoreDAL.Properties;

namespace eStoreDAL {
    public class DataReader {
        private readonly SqlConnection _conn = new SqlConnection(Settings.Default.eStoreConnectionString);
        private SqlCommand _command;

        public ArrayList GetDepartmentSeoDetails(int depId) {
            return GetData("uspDepartmentGetSEODetailsByID", "@ID", depId);
        }
        
        public ArrayList GetCategorySeoDetails(int catId) {
            return GetData("uspCategoryGetSEODetailsByID", "@ID", catId);
        }

        public ArrayList GetProductSeoDetails(int prodId) {
            return GetData("uspProductGetSEODetailsByID", "@ID", prodId);
        }

        private ArrayList GetData(string storedProcName, string paramName, int param) {
            ArrayList al = null;
            try {
                Initialise(storedProcName);
                AddParameter(paramName, param);
                al = BuildArrayList();
                _conn.Close();
            } catch(SqlException sex) {
                Console.WriteLine(sex.Message);
            } catch(ArrayTypeMismatchException atmex) {
                Console.WriteLine(atmex.Message);
            }
            return al;
        }

        private void Initialise(string storedProcName) {
            _conn.Open();
            _command = new SqlCommand(storedProcName, _conn) {CommandType = CommandType.StoredProcedure};
        }

        private void AddParameter(string name, int param) {
            _command.Parameters.Add(name, SqlDbType.Int).Value = param;
        }

        private ArrayList BuildArrayList() {
            var al = new ArrayList();
            var reader = _command.ExecuteReader();
            while(reader.Read()) {
                var values = new string[reader.FieldCount];
                reader.GetValues(values);
                al.Add(values);
            }
            return al;
        }
    }
}