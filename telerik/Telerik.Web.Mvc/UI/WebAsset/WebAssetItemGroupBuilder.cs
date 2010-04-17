// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.ComponentModel;

    using Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring the <see cref="WebAssetItemGroup"/>.
    /// </summary>
    public class WebAssetItemGroupBuilder : IHideObjectMembers
    {
        private readonly WebAssetItemGroup assetItemGroup;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetItemGroupBuilder"/> class.
        /// </summary>
        /// <param name="assetItemGroup">The asset item group.</param>
        public WebAssetItemGroupBuilder(WebAssetItemGroup assetItemGroup)
        {
            Guard.IsNotNull(assetItemGroup, "assetItemGroup");

            this.assetItemGroup = assetItemGroup;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Telerik.Web.Mvc.UI.WebAssetItemGroupBuilder"/> to <see cref="Telerik.Web.Mvc.UI.WebAssetItemGroup"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator WebAssetItemGroup(WebAssetItemGroupBuilder builder)
        {
            Guard.IsNotNull(builder, "builder");

            return builder.ToGroup();
        }

        /// <summary>
        /// Returns the internal group.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public WebAssetItemGroup ToGroup()
        {
            return assetItemGroup;
        }

        /// <summary>
        /// Sets whether Telerik content delivery network would be used.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual WebAssetItemGroupBuilder UseTelerikContentDeliveryNetwork(bool value)
        {
            assetItemGroup.UseTelerikContentDeliveryNetwork = value;

            return this;
        }

        /// <summary>
        /// Sets the content delivery network URL.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().ScriptRegistrar()
        ///           .DefaultGroup(group => group.ContentDeliveryNetworkUrl("http://www.example.com"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual WebAssetItemGroupBuilder ContentDeliveryNetworkUrl(string value)
        {
            assetItemGroup.ContentDeliveryNetworkUrl = value;

            return this;
        }

        /// <summary>
        /// Enables or disables the group
        /// </summary>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().ScriptRegistrar()
        ///           .DefaultGroup(group => group.Enabled((bool)ViewData["enabled"]))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual WebAssetItemGroupBuilder Enabled(bool value)
        {
            assetItemGroup.Enabled = value;

            return this;
        }

        /// <summary>
        /// Sets the version.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().ScriptRegistrar()
        ///           .DefaultGroup(group => group.Version("1.1"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual WebAssetItemGroupBuilder Version(string value)
        {
            assetItemGroup.Version = value;

            return this;
        }

        /// <summary>
        /// Sets whether the groups will be served as compressed. By default asset groups are not compressed.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().ScriptRegistrar()
        ///           .DefaultGroup(group => group.Compress(true))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual WebAssetItemGroupBuilder Compress(bool value)
        {
            assetItemGroup.Compress = value;

            return this;
        }

        /// <summary>
        /// Sets the caches the duration of this group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().ScriptRegistrar()
        ///           .DefaultGroup(group => group.CacheDurationInDays(365))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual WebAssetItemGroupBuilder CacheDurationInDays(float value)
        {
            assetItemGroup.CacheDurationInDays = value;

            return this;
        }

        /// <summary>
        /// Sets whether the groups items will be served as combined.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().ScriptRegistrar()
        ///           .DefaultGroup(group => group.Combined(true))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual WebAssetItemGroupBuilder Combined(bool value)
        {
            assetItemGroup.Combined = value;

            return this;
        }

        /// <summary>
        /// Sets the defaults path of the containing <see cref="WebAssetItem"/>.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public virtual WebAssetItemGroupBuilder DefaultPath(string path)
        {
            assetItemGroup.DefaultPath = path;

            return this;
        }

        /// <summary>
        /// Adds the specified source as <see cref="WebAssetItem"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().ScriptRegistrar()
        ///           .DefaultGroup(group => group.Add("script1.js"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual WebAssetItemGroupBuilder Add(string value)
        {
            assetItemGroup.Items.Add(CreateItem(value));

            return this;
        }

        private WebAssetItem CreateItem(string source)
        {
            Guard.IsNotNullOrEmpty(source, "source");

            string itemSource = source.StartsWith("~/", StringComparison.OrdinalIgnoreCase) ? source : PathHelper.CombinePath(assetItemGroup.DefaultPath, source);

            return new WebAssetItem(itemSource);
        }
    }
}