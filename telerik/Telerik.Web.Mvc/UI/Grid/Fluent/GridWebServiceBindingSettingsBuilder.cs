// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;

    public class GridWebServiceBindingSettingsBuilder : IHideObjectMembers
    {
        private GridBindingSettings settings;

        public GridWebServiceBindingSettingsBuilder(GridBindingSettings settings)
        {
            this.settings = settings;
        }

        public GridWebServiceBindingSettingsBuilder Select(string webServiceUrl)
        {
            settings.Select.Url = webServiceUrl;

            return this;
        }

        public GridWebServiceBindingSettingsBuilder Insert(string webServiceUrl)
        {
            settings.Insert.Url = webServiceUrl;

            return this;
        }

        public GridWebServiceBindingSettingsBuilder Update(string webServiceUrl)
        {
            settings.Update.Url = webServiceUrl;

            return this;
        }

        public GridWebServiceBindingSettingsBuilder Delete(string webServiceUrl)
        {
            settings.Delete.Url = webServiceUrl;

            return this;
        }

        public GridWebServiceBindingSettingsBuilder Enabled(bool value)
        {
            settings.Enabled = value;

            return this;
        }
    }
}
