// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

using System;
namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Contains constants for CSS class names
    /// </summary>
    public static class UIPrimitives
    {
        /// <summary>
        /// Active state of items
        /// </summary>
        public const string ActiveState = "t-state-active";

        /// <summary>
        /// Button
        /// </summary>
        public const string Button = "t-button";

        /// <summary>
        /// Content - rendered around custom content
        /// </summary>
        public const string Content = "t-content";

        /// <summary>
        /// Default state of items
        /// </summary>
        public const string DefaultState = "t-state-default";

        /// <summary>
        /// Disabled state of items
        /// </summary>
        public const string DisabledState = "t-state-disabled";

        /// <summary>
        /// Group - rendered around grouped items (children)
        /// </summary>
        public const string Group = "t-group";

        /// <summary>
        /// Header - rendered on headers or header items
        /// </summary>
        public const string Header = "t-header";

        /// <summary>
        /// Hovered state of items
        /// </summary>
        public const string HoverState = "t-state-hover";

        /// <summary>
        /// Icon - icon from default icon set
        /// </summary>
        public const string Icon = "t-icon";

        /// <summary>
        /// Image - image rendered through ImageUrl
        /// </summary>
        public const string Image = "t-image";

        /// <summary>
        /// Item - rendered on items
        /// </summary>
        public const string Item = "t-item";

        /// <summary>
        /// First in list of items
        /// </summary>
        public const string First = "t-first";

        /// <summary>
        /// Last in list of items
        /// </summary>
        public const string Last = "t-last";

        /// <summary>
        /// Top in list of items
        /// </summary>
        public const string Top = "t-top";

        /// <summary>
        /// Bottom in list of items
        /// </summary>
        public const string Bottom = "t-bot";

        /// <summary>
        /// Middle in list of items
        /// </summary>
        public const string Middle = "t-mid";

        /// <summary>
        /// Last in list of headers
        /// </summary>
        public const string LastHeader = "t-last-header";

        /// <summary>
        /// Link - rendered on all links
        /// </summary>
        public const string Link = "t-link";

        /// <summary>
        /// Reset - removes inherited styles
        /// </summary>
        public const string ResetStyle = "t-reset";

        /// <summary>
        /// Selected state of items
        /// </summary>
        public const string SelectedState = "t-state-selected";

        /// <summary>
        /// Sprite - sprite rendered in the begging of the item.
        /// </summary>
        public const string Sprite = "t-sprite";

        /// <summary>
        /// Widget - rendered always on the outmost HTML element of a UI component
        /// </summary>
        public const string Widget = "t-widget";

        /// <summary>
        /// Input - input rendered in the div wrapper
        /// </summary>
        public const string Input = "t-input";

        /// <summary>
        /// CheckBox - rendered on all checkbox
        /// </summary>
        public const string CheckBox = "t-checkbox";

        /// <summary>
        /// ToolBar - rendered on all toolbars
        /// </summary>
        public const string ToolBar = "t-toolbar";
        
        /// <summary>
        /// Contains CSS classes, used in the Grid
        /// </summary>
        public static class Icons
        {
            public const string Delete = "t-delete";

            public const string GroupDelete = "t-group-delete";
        }

        /// <summary>
        /// Contains CSS classes, used in the Grid
        /// </summary>
        public static class Grid
        {
            /// <summary>
            /// Grid action
            /// </summary>
            public const string Action = "t-grid-action";

            public const string Edit = "t-grid-edit";
            
            public const string Delete = "t-grid-delete";
            
            public const string Update = "t-grid-update";
            
            public const string Cancel = "t-grid-cancel";
            
            public const string Insert = "t-grid-insert";

            public const string Add = "t-grid-add";
            
            public const string Select = "t-grid-select";

            public const string GroupCell = "t-group-cell";
            
            public const string HierarchyCell = "t-hierarchy-cell";

            public const string GroupCol = "t-group-col";
            
            public const string HierarchyCol = "t-hierarchy-col";

            public const string GroupIndicator = "t-group-indicator";

            /// <summary>
            /// Grid action
            /// </summary>
            public const string ActionForm = "t-grid-actions";

            /// <summary>
            /// Container element for editing / inserting form
            /// </summary>
            public const string EditingContainer = "t-edit-container";

            /// <summary>
            /// Container element for editing / inserting form
            /// </summary>
            public const string EditingForm = "t-edit-form";

            /// <summary>
            /// Toolbar which contains different commands
            /// </summary>
            public const string ToolBar = "t-grid-toolbar";
        }

        public static class TreeView
        {
            /// <summary>
            /// Class that shows treeview lines
            /// </summary>
            public const string Lines = "t-treeview-lines";
        }

        public static class Editor
        {
            /// <summary>
            /// Editor toolbar button
            /// </summary>
            public const string ToolbarButton = "t-editor-button";

            /// <summary>
            /// Editor toolbar color picker
            /// </summary>
            public const string ToolbarColorPicker = "t-editor-colorpicker";

            /// <summary>
            /// Editor tool icon
            /// </summary>
            public const string ToolIcon = "t-tool-icon";
            public const string Custom = "t-custom";
        }
    }
}