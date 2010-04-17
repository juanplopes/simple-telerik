// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;

    public class TreeViewItemModel
    {
        public TreeViewItemModel()
        {
            this.Enabled = true;
            this.Items = new List<TreeViewItemModel>();
        }

        public string Value
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public bool Expanded
        {
            get;
            set;
        }

        public bool Checked
        {
            get;
            set;
        }

        public string NavigateUrl
        {
            get;
            set;
        }

        public string ImageUrl
        {
            get;
            set;
        }

        public bool LoadOnDemand
        {
            get;
            set;
        }

        public List<TreeViewItemModel> Items
        {
            get;
            set;
        }
    }
}