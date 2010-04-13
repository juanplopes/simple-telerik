// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// Represents a list of <see cref="IWebAssetItem"/> that can be accessed by index. Provides methods to search, sort and manipulate lists.
    /// </summary>
    public class WebAssetItemCollection : Collection<IWebAssetItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetItemCollection"/> class.
        /// </summary>
        /// <param name="defaultPath">The default path.</param>
        public WebAssetItemCollection(string defaultPath)
        {
            Guard.IsNotVirtualPath(defaultPath, "defaultPath");

            DefaultPath = defaultPath;
        }

        /// <summary>
        /// Gets or sets the default path.
        /// </summary>
        /// <value>The default path.</value>
        public string DefaultPath
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the asset groups.
        /// </summary>
        /// <value>The asset groups.</value>
        public virtual IEnumerable<WebAssetItemGroup> AssetGroups
        {
            [DebuggerStepThrough]
            get
            {
                return this.OfType<WebAssetItemGroup>();
            }
        }

        /// <summary>
        /// Gets the asset items.
        /// </summary>
        /// <value>The asset items.</value>
        public virtual IEnumerable<WebAssetItem> AssetItems
        {
            [DebuggerStepThrough]
            get
            {
                return this.OfType<WebAssetItem>();
            }
        }

        /// <summary>
        /// Finds the group with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual WebAssetItemGroup FindGroupByName(string name)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            return AssetGroups.SingleOrDefault(group => group.Name.IsCaseInsensitiveEqual(name));
        }

        /// <summary>
        /// Finds the item with the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public virtual WebAssetItem FindItemBySource(string source)
        {
            Guard.IsNotNullOrEmpty(source, "source");

            return AssetItems.SingleOrDefault(item => item.Source.IsCaseInsensitiveEqual(source));
        }

        /// <summary>
        /// Adds the specified source as <see cref="WebAssetItem"/>.
        /// </summary>
        /// <param name="itemSource">The item source.</param>
        public virtual void Add(string itemSource)
        {
            Add(CreateItem(itemSource));
        }

        /// <summary>
        /// Adds the specified source as <see cref="WebAssetItem"/> in the specified <see cref="WebAssetItemGroup"/>.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="itemSource">The item source.</param>
        public virtual void Add(string groupName, string itemSource)
        {
            Guard.IsNotNullOrEmpty(groupName, "groupName");
            Guard.IsNotVirtualPath(itemSource, "itemSource");

            WebAssetItemGroup itemGroup = FindGroupByName(groupName);

            if (itemGroup == null)
            {
                itemGroup = CreateGroup(groupName);
                Add(itemGroup);
            }

            itemGroup.Items.Add(CreateItem(itemSource));
        }

        /// <summary>
        /// Inserts the specified source as <see cref="WebAssetItem"/> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="itemSource">The item source.</param>
        public virtual void Insert(int index, string itemSource)
        {
            Insert(index, CreateItem(itemSource));
        }

        /// <summary>
        /// Inserts the specified source as <see cref="WebAssetItem"/> at the specified index in the specified <see cref="WebAssetItemGroup"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="itemSource">The item source.</param>
        public virtual void Insert(int index, string groupName, string itemSource)
        {
            Guard.IsNotNegative(index, "index");
            Guard.IsNotNullOrEmpty(groupName, "groupName");
            Guard.IsNotVirtualPath(itemSource, "itemSource");

            WebAssetItemGroup itemGroup = FindGroupByName(groupName);

            if (itemGroup == null)
            {
                itemGroup = CreateGroup(groupName);
                Insert(index, itemGroup);
            }

            itemGroup.Items.Add(CreateItem(itemSource));
        }

        /// <summary>
        /// Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than zero.
        /// -or-
        /// <paramref name="index"/> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count"/>.
        /// </exception>
        protected override void InsertItem(int index, IWebAssetItem item)
        {
            if (!AlreadyExists(item))
            {
                base.InsertItem(index, item);
            }
        }

        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to replace.</param>
        /// <param name="item">The new value for the element at the specified index. The value can be null for reference types.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than zero.
        /// -or-
        /// <paramref name="index"/> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count"/>.
        /// </exception>
        protected override void SetItem(int index, IWebAssetItem item)
        {
            if (AlreadyExists(item))
            {
                if (item is WebAssetItem)
                {
                    throw new ArgumentException(Resources.TextResource.ItemWithSpecifiedSourceAlreadyExists, "item");
                }

                if (item is WebAssetItemGroup)
                {
                    throw new ArgumentException(Resources.TextResource.GroupWithSpecifiedNameAlreadyExists, "item");
                }
            }

            base.SetItem(index, item);
        }

        private WebAssetItemGroup CreateGroup(string name)
        {
            return new WebAssetItemGroup(name, false) { DefaultPath = DefaultPath };
        }

        private WebAssetItem CreateItem(string source)
        {
            Guard.IsNotNullOrEmpty(source, "source");

            string itemSource = source.StartsWith("~/", StringComparison.OrdinalIgnoreCase) ? source : PathHelper.CombinePath(DefaultPath, source);

            return new WebAssetItem(itemSource);
        }

        private bool AlreadyExists(IWebAssetItem item)
        {
            WebAssetItem assetItem = item as WebAssetItem;
            WebAssetItemGroup assetItemGroup = item as WebAssetItemGroup;

            if (assetItem != null)
            {
                return AssetItems.Any(i => i != item && i.Source.IsCaseInsensitiveEqual(assetItem.Source));
            }

            if (assetItemGroup != null)
            {
                return AssetGroups.Any(i => i != item && i.Name.IsCaseInsensitiveEqual(assetItemGroup.Name));
            }

            return false;
        }
    }
}