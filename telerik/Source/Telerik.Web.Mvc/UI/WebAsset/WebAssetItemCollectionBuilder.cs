// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.ComponentModel;

    using Extensions;
    using Infrastructure;
    using Resources;

    /// <summary>
    /// Defines the fluent interface for configuring web assets.
    /// </summary>
    public class WebAssetItemCollectionBuilder : IHideObjectMembers
    {
        private readonly WebAssetType assetType;
        private readonly WebAssetItemCollection assets;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetItemCollectionBuilder"/> class.
        /// </summary>
        /// <param name="assetType">Type of the asset.</param>
        /// <param name="assets">The assets.</param>
        public WebAssetItemCollectionBuilder(WebAssetType assetType, WebAssetItemCollection assets)
        {
            if (assetType == WebAssetType.None)
            {
                throw new ArgumentException(TextResource.NoneIsOnlyUsedForInternalPurpose, "assetType");
            }

            Guard.IsNotNull(assets, "assets");

            this.assetType = assetType;
            this.assets = assets;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Telerik.Web.Mvc.UI.WebAssetItemCollectionBuilder"/> to <see cref="Telerik.Web.Mvc.UI.WebAssetItemCollection"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator WebAssetItemCollection(WebAssetItemCollectionBuilder builder)
        {
            Guard.IsNotNull(builder, "builder");

            return builder.ToCollection();
        }

        /// <summary>
        /// Returns the internal collection.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public WebAssetItemCollection ToCollection()
        {
            return assets;
        }

        /// <summary>
        /// Adds a new web asset
        /// </summary>
        /// <param name="source">The source.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().ScriptRegistrar()
        ///            .Scripts(scripts => scripts.Add("script1.js"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual WebAssetItemCollectionBuilder Add(string source)
        {
            assets.Add(source);

            return this;
        }

        /// <summary>
        /// Adds a new web asset group.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="configureAction">The configure action.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().ScriptRegistrar()
        ///            .Scripts(scripts => scripts.AddGroup("Group1", group => 
        ///                 {
        ///                     group.Add("script1.js");
        ///                 }
        ///            ))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual WebAssetItemCollectionBuilder AddGroup(string name, Action<WebAssetItemGroupBuilder> configureAction)
        {
            Guard.IsNotNullOrEmpty(name, "name");
            Guard.IsNotNull(configureAction, "configureAction");

            WebAssetItemGroup itemGroup = assets.FindGroupByName(name);

            if (itemGroup != null)
            {
                throw new ArgumentException(TextResource.GroupWithSpecifiedNameAlreadyExistsPleaseSpecifyADifferentName.FormatWith(name));
            }

            itemGroup = new WebAssetItemGroup(name, false) { DefaultPath = assets.DefaultPath };
            assets.Add(itemGroup);

            WebAssetItemGroupBuilder builder = new WebAssetItemGroupBuilder(itemGroup);
            configureAction(builder);

            return this;
        }

        /// <summary>
        /// Adds the specified shared group.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().ScriptRegistrar()
        ///            .Scripts(scripts => scripts.AddShareGroup("SharedGroup1"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual WebAssetItemCollectionBuilder AddSharedGroup(string name)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            WebAssetItemGroup group = (assetType == WebAssetType.StyleSheet) ?
                                      SharedWebAssets.FindStyleSheetGroup(name) :
                                      SharedWebAssets.FindScriptGroup(name);

            if (group == null)
            {
                throw new ArgumentException(TextResource.GroupWithSpecifiedNameDoesNotExistInAssetTypeOfSharedWebAssets.FormatWith(name, assetType), "name");
            }

            if (assets.FindGroupByName(name) == null)
            {
                // People might have the same group reference in multiple place.
                // So we will skip it once it is added.

                // throw new ArgumentException(TextResource.LocalGroupWithSpecifiedNameAlreadyExists.FormatWith(name));

                // Add a copy of the shared asset
                WebAssetItemGroup localGroup = new WebAssetItemGroup(group.Name, true)
                                                   {
                                                       DefaultPath = group.DefaultPath,
                                                       UseTelerikContentDeliveryNetwork = group.UseTelerikContentDeliveryNetwork,
                                                       ContentDeliveryNetworkUrl = group.ContentDeliveryNetworkUrl,
                                                       Enabled = group.Enabled,
                                                       Version = group.Version,
                                                       Compress = group.Compress,
                                                       CacheDurationInDays = group.CacheDurationInDays,
                                                       Combined = group.Combined
                                                   };

                foreach (WebAssetItem item in group.Items)
                {
                    localGroup.Items.Add(new WebAssetItem(item.Source));
                }

                assets.Add(localGroup);
            }

            return this;
        }

        /// <summary>
        /// Executes the provided delegate that is used to configure the group fluently.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="configureAction">The configure action.</param>
        public virtual WebAssetItemCollectionBuilder GetGroup(string name, Action<WebAssetItemGroupBuilder> configureAction)
        {
            Guard.IsNotNullOrEmpty(name, "name");
            Guard.IsNotNull(configureAction, "configureAction");

            WebAssetItemGroup itemGroup = assets.FindGroupByName(name);

            if (itemGroup == null)
            {
                throw new ArgumentException(TextResource.GroupWithSpecifiedNameDoesNotExistPleaseMakeSureYouHaveSpecifiedACorrectName.FormatWith(name));
            }

            if (itemGroup.IsShared)
            {
                throw new InvalidOperationException(TextResource.YouCannotConfigureASharedWebAssetGroup);
            }

            WebAssetItemGroupBuilder builder = new WebAssetItemGroupBuilder(itemGroup);

            configureAction(builder);

            return this;
        }
    }
}