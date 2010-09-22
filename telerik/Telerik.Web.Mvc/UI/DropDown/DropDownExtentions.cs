// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Linq;

    public static class DropDownExtentions
    {
        public static void PrepareItemsAndDefineSelectedIndex(this IDropDown instance)
        {
            var selectedItemIndex = instance.Items.IndexOf(instance.Items.LastOrDefault(item => item.Selected == true));

            if (selectedItemIndex != -1)
            {
                for (int i = 0, length = instance.Items.Count; i < length; i++)
                {
                    instance.Items[i].Selected = false;
                }
                instance.SelectedIndex = -1;
            }

            if (instance.SelectedIndex != -1 && instance.SelectedIndex < instance.Items.Count)
            {
                instance.Items[instance.SelectedIndex].Selected = true;
            }
            else if (selectedItemIndex != -1)
            {
                instance.Items[selectedItemIndex].Selected = true;
                instance.SelectedIndex = selectedItemIndex;
            }
            else if (instance is DropDownList)
            {
                instance.Items[0].Selected = true;
                instance.SelectedIndex = 0;
            }
        }
    }
}
