// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Fluent
{
    public class GridEditingSettingsBuilder : IHideObjectMembers
    {
        private readonly GridEditingSettings settings;

        public GridEditingSettingsBuilder(GridEditingSettings settings)
        {
            this.settings = settings;
        }

        public GridEditingSettingsBuilder Enabled(bool value)
        {
            settings.Enabled = value;
            
            return this;
        }

        public GridEditingSettingsBuilder DisplayDeleteConfirmation(bool value)
        {
            settings.DisplayDeleteConfirmation = value;
            
            return this;
        }
    }
}
