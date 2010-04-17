// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    
    using Infrastructure;
    
    public class TreeViewDataBindingConfigurationBuilder : IHideObjectMembers
    {
        private readonly TreeViewDataBindingConfiguration configuration;

        public TreeViewDataBindingConfigurationBuilder(TreeViewDataBindingConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Use it to configure Ajax binding.
        /// </summary>
        /// <param name="configurator">Use builder to set different ajax binding settings.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .DataBinding(dataBinding => dataBinding
        ///                .Ajax().Select("_AjaxLoading", "TreeView")
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TreeViewBindingSettingsBuilder Ajax()
        {
            configuration.Ajax.Enabled = true;
            
            return new TreeViewBindingSettingsBuilder(configuration.Ajax);
        }

        /// <summary>
        /// Use it to configure Ajax binding.
        /// </summary>
        /// <param name="configurator">Use builder to set different ajax binding settings.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .DataBinding(dataBinding => dataBinding
        ///                .WebService().Select("~/Models/Employees.asmx/GetEmployees")
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TreeViewWebServiceBindingSettingsBuilder WebService()
        {
            configuration.WebService.Enabled = true;

            return new TreeViewWebServiceBindingSettingsBuilder(configuration.WebService);
        }
    }
}
