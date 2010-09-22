// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
using System;
using Telerik.Web.Mvc.Infrastructure;

namespace Telerik.Web.Mvc.UI.Fluent
{
    /// <summary>
    /// Defines the fluent interface for configuring grid editing.
    /// </summary>
    public class GridEditingSettingsBuilder : IHideObjectMembers
    {
        private readonly GridEditingSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridEditingSettingsBuilder"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public GridEditingSettingsBuilder(GridEditingSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Enables or disables grid editing.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid&lt;Order&gt;()
        ///             .Name("Orders")
        ///             .Editable(settings => settings.Enabled(true))
        /// %&gt;
        /// </code>
        /// </example>
        /// <remarks>
        /// The Enabled method is useful when you need to enable grid editing on certain conditions.
        /// </remarks>
        public GridEditingSettingsBuilder Enabled(bool value)
        {
            settings.Enabled = value;
            
            return this;
        }

        public GridEditingSettingsBuilder Mode(GridEditMode mode)
        {
            settings.Mode = mode;

            return this;
        }

        public GridEditingSettingsBuilder Window(Action<WindowBuilder> configurator)
        {
            Guard.IsNotNull(configurator, "configurator");

            configurator(new WindowBuilder(settings.PopUp));

            return this;
        }


        /// <summary>
        /// Enables or disables delete confirmation.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid&lt;Order&gt;()
        ///             .Name("Orders")
        ///             .Editable(settings => settings.DisplayDeleteConfirmation(true))
        /// %&gt;
        /// </code>
        /// </example>
        /// <remarks>
        /// The Enabled method is useful when you need to enable grid editing on certain conditions.
        /// </remarks>
        public GridEditingSettingsBuilder DisplayDeleteConfirmation(bool value)
        {
            settings.DisplayDeleteConfirmation = value;
            
            return this;
        }
    }
}
