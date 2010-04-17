// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    
    using Infrastructure;
    
    public class GridDataBindingConfigurationBuilder : IHideObjectMembers
    {
        private readonly GridDataBindingSettings configuration;

        public GridDataBindingConfigurationBuilder(GridDataBindingSettings configuration)
        {
            this.configuration = configuration;
        }

        public virtual GridBindingSettingsBuilder Server()
        {
            return new GridBindingSettingsBuilder(configuration.Server);
        }
        
        public virtual GridBindingSettingsBuilder Ajax()
        {
            configuration.Ajax.Enabled = true;
            
            return new GridBindingSettingsBuilder(configuration.Ajax);
        }
        
        public virtual GridWebServiceBindingSettingsBuilder WebService()
        {
            configuration.WebService.Enabled = true;

            return new GridWebServiceBindingSettingsBuilder(configuration.WebService);
        }
    }
}
