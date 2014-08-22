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
using System.Collections;
using System.ComponentModel;
using eStoreDAL;

namespace eStoreBLL {
    [DataObject]
    public class CategoriesBLL {
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public DAL.CategoryDataTable GetCategories() {
            return BLLAdapter.Instance.CategoryAdapter.GetCategories();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.CategoryDataTable GetCategoriesByDepartmentId(int departmentId) {
            return BLLAdapter.Instance.CategoryAdapter.GetCategoriesByDepartment(departmentId);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.CategoryDataTable GetCategoryById(int id) {
            return BLLAdapter.Instance.CategoryAdapter.GetCategoryByID(id);
        }

        public string GetCategoryPageTitle(int id) {
            return BLLAdapter.Instance.CategoryAdapter.GetCategoryPageTitle(id).ToString();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public ArrayList GetSeoDetails(int id) {
            return new DataReader().GetCategorySeoDetails(id);
        }
    }
}