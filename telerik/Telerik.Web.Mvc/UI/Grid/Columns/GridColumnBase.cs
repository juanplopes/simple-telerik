
// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Telerik.Web.Mvc.UI.Html;

    /// <summary>
    /// Represents a column in the <see cref="Grid{T}"/> component
    /// </summary>
    /// <typeparam name="T">The type of the data item</typeparam>
    public abstract class GridColumnBase<T> : IGridColumn where T : class
    {
        public string Format
        {
            get
            {
                return Settings.Format;
            }
            set
            {
                Settings.Format = value;
            }
        }
        
        public string EditorHtml
        {
            get;
            set;
        }

        protected GridColumnBase(Grid<T> grid)
        {
            Grid = grid;
            Settings = new GridColumnSettings();
            Visible = true;
        }

        /// <summary>
        /// Gets or sets the grid.
        /// </summary>
        /// <value>The grid.</value>
        public Grid<T> Grid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the member of the column.
        /// </summary>
        /// <value>The member.</value>
        public string Member
        {
            get
            {
                return Settings.Member;
            }
            
            set
            {
                Settings.Member = value;
            }
        }

        /// <summary>
        /// Gets the template of the column.
        /// </summary>
        public virtual Action<T> Template
        {
            get;
            set;
        }

        public virtual Func<T, object> InlineTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the title of the column.
        /// </summary>
        /// <value>The title.</value>
        public virtual string Title
        {
            get
            {
                return Settings.Title;
            }
            set
            {
                Settings.Title = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the column.
        /// </summary>
        /// <value>The width.</value>
        public string Width
        {
            get
            {
                return Settings.Width;
            }
            set
            {
                Settings.Width = value;
            }
        }

        public string ClientTemplate
        {
            get
            {
                return Settings.ClientTemplate;
            }
            set
            {
                Settings.ClientTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this column is hidden.
        /// </summary>
        /// <value><c>true</c> if hidden; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// Hidden columns are output as HTML but are not visible by the end-user.
        /// </remarks>
        public virtual bool Hidden
        {
            get
            {
                return Settings.Hidden;
            }
            set
            {
                Settings.Hidden = value;
            }
        }

        public virtual bool Encoded
        {
            get
            {
                return Settings.Encoded;
            }
            
            set
            {
                Settings.Encoded = value;
            }
        }

        /// <summary>
        /// Gets the header HTML attributes.
        /// </summary>
        /// <value>The header HTML attributes.</value>
        public IDictionary<string, object> HeaderHtmlAttributes
        {
            get
            {
                return Settings.HeaderHtmlAttributes;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this column is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        /// <remarks>
        /// Invisible columns are not output in the HTML.
        /// </remarks>
        public bool Visible
        {
            get
            {
                return Settings.Visible;
            }
            set
            {
                Settings.Visible = value;
            }
        }

        public virtual IHtmlBuilder CreateDisplayHtmlBuilder(GridCell<T> cell)
        {
            return CreateCellBuilder(CreateDisplayHtmlBuilderCore, cell);
        }

        public virtual IHtmlBuilder CreateEditorHtmlBuilder(GridCell<T> cell)
        {
            return CreateCellBuilder(CreateEditorHtmlBuilderCore, cell);
        }

        private IHtmlBuilder CreateCellBuilder(Func<GridCell<T>, IHtmlBuilder> creator, GridCell<T> cell)
        {
            var builder = creator(cell);

            CellAction(cell);

            Decorate(builder);

            return builder;
        }

        private void CellAction(GridCell<T> cell)
        {
            if (Grid.CellAction != null)
            {
                Grid.CellAction(cell);
            }
        }

        private void Decorate(IHtmlBuilder builder)
        {
            if (builder != null)
            {
                if (Hidden)
                {
                    builder.Adorners.Add(new GridHiddenColumnAdorner());
                }

                if (IsLast)
                {
                    builder.Adorners.Add(new GridCssClassAdorner
                    {
                        CssClasses = { UIPrimitives.Last }
                    });
                }
            }
        }


        protected virtual IHtmlBuilder CreateEditorHtmlBuilderCore(GridCell<T> cell)
        {
            return CreateDisplayHtmlBuilderCore(cell);
        }

        protected virtual IHtmlBuilder CreateDisplayHtmlBuilderCore(GridCell<T> cell)
        {
            if (Template != null || InlineTemplate != null)
            {
                return new GridTemplateCellHtmlBuilder<T>(cell);
            }

            return null;
        }

        /// <summary>
        /// Gets the HTML attributes of the cell rendered for the column
        /// </summary>
        /// <value>The HTML attributes.</value>
        public IDictionary<string, object> HtmlAttributes
        {
            get
            {
                return Settings.HtmlAttributes;
            }
        }

        public virtual IGridColumnSerializer CreateSerializer()
        {
            return new GridColumnSerializer(this);
        }

        IGrid IGridColumn.Grid
        {
            get
            {
                return Grid;
            }
        }

        public bool IsLast
        {
            get
            {
                return Grid.VisibleColumns.LastOrDefault() == this;
            }
        }

        internal GridColumnSettings Settings
        {
            get;
            set;
        }
    }
}