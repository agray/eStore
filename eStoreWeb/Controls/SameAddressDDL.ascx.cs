using System;
using System.Web.UI.WebControls;

namespace eStoreWeb.Controls {
    public partial class SameAddressDDL : BaseUserControl {
        public event IndexChangedEventHandler IndexChanged;

        protected override DropDownList DropList() {
            return SameAddressDropDownList;
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e) {
            IndexChanged(sender, e);
        }
    }
}