// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Web.Mvc;
    using Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring child menu items.
    /// </summary>
    public class MenuItemBuilder : NavigationItemBuilder<MenuItem, MenuItemBuilder>, IHideObjectMembers
    {
        private readonly MenuItem item;
        private readonly ViewContext viewContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemBuilder"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public MenuItemBuilder(MenuItem item, ViewContext viewContext)
            : base(item, viewContext)
        {
            Guard.IsNotNull(item, "item");
            Guard.IsNotNull(viewContext, "viewContext");

            this.item = item;
            this.viewContext = viewContext;
        }

        /// <summary>
        /// Configures the child items of a <see cref="MenuItem"/>.
        /// </summary>
        /// <param name="addAction">The add action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items =>
        ///             {
        ///                 items.Add().Text("First Item").Items(firstItemChildren => 
        ///                 {
        ///                     firstItemChildren.Add().Text("Child Item 1");
        ///                     firstItemChildren.Add().Text("Child Item 2");
        ///                 });
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public virtual MenuItemBuilder Items(Action<MenuItemFactory> addAction)
        {
            Guard.IsNotNull(addAction, "addAction");

            MenuItemFactory factory = new MenuItemFactory(item, viewContext);

            addAction(factory);

            return this;
        }
    }
}