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
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoenixConsulting.Common.Properties;

namespace eStoreWeb.Controls {
    public delegate void IndexChangedEventHandler(object sender, EventArgs e);

    public class BaseDDL : BaseUserControl {
        private ListItemCollection _mItems;

        protected virtual DropDownList DropList() {
            throw new InvalidOperationException("BaseUserControl.DropList not overridden in " + ToString());
        }

        public bool AutoPostBack {
            get { return GetDropDownList.AutoPostBack; }
            set { GetDropDownList.AutoPostBack = value; }
        }

        public bool AppendDataBoundItems {
            get { return GetDropDownList.AppendDataBoundItems; }
            set { GetDropDownList.AppendDataBoundItems = value; }
        }

        public override void DataBind() {
            ChildControlsCreated = true;
            base.DataBind();
        }

        public bool Enabled {
            get { return GetDropDownList.Enabled; }
            set { GetDropDownList.Enabled = value; }
        }

        public ListItem SelectedItem {
            get { return GetDropDownList.SelectedItem; }
        }

        public string SelectedValue {
            get { return GetDropDownList.SelectedValue; }
            set { GetDropDownList.SelectedValue = value; }
        }

        public string Text {
            get { return GetDropDownList.Text; }
            set { GetDropDownList.Text = value; }
        }

        [DefaultValue((string)null), MergableProperty(false), PersistenceMode(PersistenceMode.InnerDefaultProperty), Editor("System.Web.UI.Design.WebControls.ListItemsCollectionEditor,System.Design, Version=2.0.0.0,Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual ListItemCollection Items {
            get { return _mItems ?? (_mItems = new ListItemCollection()); }
        }

        protected override void CreateChildControls() {
            if(_mItems == null) {
                _mItems = new ListItemCollection();
            }

            if(_mItems.Count > 0) {
                foreach(ListItem item in _mItems) {
                    if(!GetDropDownList.Items.Contains(item)) {
                        GetDropDownList.Items.Add(item);
                    }
                }
            }
        }

        private DropDownList GetDropDownList {
            get { return DropList(); }
        }

        public static bool IsAustralia(string countryId) {
            return (countryId == Settings.Default.AustraliaCountryID);
        }
    }
}