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
using System.ComponentModel;
using eStoreDAL;

namespace eStoreBLL {
    [DataObject]
    public class SavedSearchBLL {
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public DAL.SavedSearchDataTable getSavedSearches(Guid userID) {
            DAL.SavedSearchDataTable ssdt = BLLAdapter.Instance.SavedSearchAdapter.GetSavedSearches(userID);
            return ssdt;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
        public DAL.SavedSearchDataTable getSavedSearchByID(int ID) {
            return BLLAdapter.Instance.SavedSearchAdapter.GetSavedSearchByID(ID);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public bool addSavedSearch(Guid userID, string name, string criteria) {
            if(SavedSearchExists(userID, name, criteria)) {
                return false;
            }
            DAL.SavedSearchDataTable savedSearches = new DAL.SavedSearchDataTable();
            //Create a new SavedSearchRow instance
            DAL.SavedSearchRow savedSearch = savedSearches.NewSavedSearchRow();

            savedSearch.UserID = userID;
            savedSearch.Name = name;
            savedSearch.Criteria = criteria;

            //Add the new SavedSearch
            savedSearches.AddSavedSearchRow(savedSearch);
            int rowsAffected = BLLAdapter.Instance.SavedSearchAdapter.Update(savedSearches);

            //Return True if exactly one row was inserted, otherwise False
            return rowsAffected == 1;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public bool updateSavedSearch(int original_ID, string name, string criteria) {
            DAL.SavedSearchDataTable savedSearches = BLLAdapter.Instance.SavedSearchAdapter.GetSavedSearchByID(original_ID);

            if(savedSearches.Count == 0) {
                //No matching records found, return false
                return false;
            }

            savedSearches.Rows[0]["Name"] = name;
            savedSearches.Rows[0]["Criteria"] = criteria;

            //Update the savedSearch records
            //Return True if exactly one row was updated, otherwise false   
            return BLLAdapter.Instance.SavedSearchAdapter.Update(savedSearches) == 1;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public bool deleteSavedSearch(int original_ID) {
            //Delete the SavedSearch record
            int rowsAffected = (int)BLLAdapter.Instance.SavedSearchAdapter.SavedSearchDelete(original_ID);

            //Return True if exactly one row was updated, otherwise False
            return rowsAffected == 1;
        }

        private bool SavedSearchExists(Guid userID, string name, string criteria) {
            int numMatches = (int)BLLAdapter.Instance.SavedSearchAdapter.SavedSearchExists(userID, name, criteria);
            return numMatches > 0;
        }
    }
}