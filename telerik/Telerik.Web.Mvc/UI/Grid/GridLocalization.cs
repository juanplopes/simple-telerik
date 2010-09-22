// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Globalization;
    using Infrastructure;

    public class GridLocalization : ViewComponentLocalization, IClientSerializable
    {
        public GridLocalization() : this(null)
        {
        }

        public GridLocalization(CultureInfo culture) : this(null, culture)
        {
        }

        public GridLocalization(string resourceLocation, CultureInfo culture) : base(resourceLocation, "GridLocalization", culture)
        {
        }

        public string AddNew
        {
            get { return GetValue("AddNew"); }
        }

        public string Insert
        {
            get { return GetValue("Insert"); }
        }

        public string Update
        {
            get { return GetValue("Update"); }
        }

        public string Select
        {
            get { return GetValue("Select"); }
        }

        public string Edit
        {
            get { return GetValue("Edit"); }
        }

        public string Cancel
        {
            get { return GetValue("Cancel"); }
        }

        public string Delete
        {
            get { return GetValue("Delete"); }
        }

        public string PageOf
        {
            get
            {
                return GetValue("PageOf");
            }
        }

        public string Page
        {
            get { return GetValue("Page"); }
        }

        public string DisplayingItems
        {
            get { return GetValue("DisplayingItems"); }
        }

        public string GroupHint
        {
            get
            {
                return GetValue("GroupHint");
            }
        }
        
        public void SerializeTo(string key, IClientSideObjectWriter writer)
        {
            if (!IsDefault)
            {
                writer.AppendObject(key, ToJson());
            }
        }
    }
}