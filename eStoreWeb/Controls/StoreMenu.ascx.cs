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
using System.Data;
using System.Globalization;
using System.Web.Caching;
using System.Web.UI;
using eStoreBLL;

namespace eStoreWeb.Controls {
    public partial class StoreMenu : UserControl {
        private const string ID = "ID";
        private const string DEP_ID = "DepID";
        private const string FK_NAME = "FK_tCategory_tDepartment";

        protected void Page_Load(object sender, EventArgs e) {
            if(!Page.IsPostBack) {
                BuildCatalogueMenu(FK_NAME, ID, DEP_ID);
            }
        }

        private void BuildCatalogueMenu(string relationName, string parentID, string childID) {
            DepartmentsBLL depTableAdapter = new DepartmentsBLL();
            CategoriesBLL catTableAdapter = new CategoriesBLL();

            DataSet dsDepartments = (DataSet)Cache.Get("Menu");

            if(dsDepartments == null) {
                //Not in the cache, so have to go back to the database
                dsDepartments = new DataSet();
                //Satisfies rule: SetLocaleForDataTypes.
                dsDepartments.Locale = CultureInfo.InvariantCulture;
                dsDepartments.Tables.Add(depTableAdapter.getDepartments());
                dsDepartments.Tables.Add(catTableAdapter.getCategories());

                DataTable depTable = dsDepartments.Tables[0];
                DataTable catTable = dsDepartments.Tables[1];

                dsDepartments.Relations.Add(relationName, depTable.Columns[parentID], catTable.Columns[childID]);

                SqlCacheDependency dependency = new SqlCacheDependency("eStore", "Department");
                Cache.Insert("Menu", dsDepartments, dependency);
            }

            NavCatalogue_DepartmentRepeater.DataSource = dsDepartments;
            NavCatalogue_DepartmentRepeater.DataBind();
        }
    }
}