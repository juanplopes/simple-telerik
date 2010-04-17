// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Web.Mvc;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// Manages ASP.NET MVC views style sheet files.
    /// </summary>
    public class StyleSheetRegistrar
    {
        /// <summary>
        /// Used to ensure that the same instance is used for the same HttpContext.
        /// </summary>
        public static readonly string Key = typeof(StyleSheetRegistrar).AssemblyQualifiedName;

        private string assetHandlerPath;
        private bool hasRendered;

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleSheetRegistrar"/> class.
        /// </summary>
        /// <param name="styleSheets">The style sheets.</param>
        /// <param name="viewContext">The view context.</param>
        /// <param name="assetItemMerger">The asset merger.</param>
        public StyleSheetRegistrar(WebAssetItemCollection styleSheets, ViewContext viewContext, IWebAssetItemMerger assetItemMerger)
        {
            Guard.IsNotNull(styleSheets, "styleSheets");
            Guard.IsNotNull(viewContext, "viewContext");
            Guard.IsNotNull(assetItemMerger, "assetItemMerger");

            if (viewContext.HttpContext.Items[Key] != null)
            {
                throw new InvalidOperationException(Resources.TextResource.OnlyOneStyleSheetRegistrarIsAllowedInASingleRequest);
            }

            viewContext.HttpContext.Items[Key] = this;

            DefaultGroup = new WebAssetItemGroup("default", false) { DefaultPath = WebAssetDefaultSettings.StyleSheetFilesPath };
            StyleSheets = styleSheets;
            ViewContext = viewContext;
            AssetMerger = assetItemMerger;

            AssetHandlerPath = WebAssetHttpHandler.DefaultPath;
        }

        /// <summary>
        /// Gets or sets the asset handler path. Path must be a virtual path. The default value is set to WebAssetHttpHandler.DefaultPath.
        /// </summary>
        /// <value>The asset handler path.</value>
        public string AssetHandlerPath
        {
            [DebuggerStepThrough]
            get
            {
                return assetHandlerPath;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotVirtualPath(value, "value");

                assetHandlerPath = value;
            }
        }

        /// <summary>
        /// Gets or sets the default group.
        /// </summary>
        /// <value>The default group.</value>
        public WebAssetItemGroup DefaultGroup
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the stylesheets that will be rendered in the view.
        /// </summary>
        /// <value>The style sheets.</value>
        public WebAssetItemCollection StyleSheets
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the view context.
        /// </summary>
        /// <value>The view context.</value>
        protected ViewContext ViewContext
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the asset merger.
        /// </summary>
        /// <value>The asset merger.</value>
        protected IWebAssetItemMerger AssetMerger
        {
            get;
            private set;
        }

        /// <summary>
        /// Writes the stylesheets in the response.
        /// </summary>
        public void Render()
        {
            if (hasRendered)
            {
                throw new InvalidOperationException(Resources.TextResource.YouCannotCallRenderMoreThanOnce);
            }

            if (ViewContext.HttpContext.Request.Browser.SupportsCss)
            {
                Write(ViewContext.HttpContext.Response.Output);
            }

            hasRendered = true;
        }

        /// <summary>
        /// Writes all stylesheet source.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected virtual void Write(TextWriter writer)
        {
            IList<string> mergedList = new List<string>();

            bool isSecured = ViewContext.HttpContext.Request.IsSecureConnection;
            bool canCompress = ViewContext.HttpContext.Request.CanCompress();

            Action<WebAssetItemCollection> append = assets =>
                                                    {
                                                        IList<string> result = AssetMerger.Merge("text/css", AssetHandlerPath, isSecured, canCompress, assets);

                                                        if (!result.IsNullOrEmpty())
                                                        {
                                                            mergedList.AddRange(result);
                                                        }
                                                    };

            if (!DefaultGroup.Items.IsEmpty())
            {
                append(new WebAssetItemCollection(DefaultGroup.DefaultPath) { DefaultGroup });
            }

            if (!StyleSheets.IsEmpty())
            {
                append(StyleSheets);
            }

            if (!mergedList.IsEmpty())
            {
                foreach (string stylesheet in mergedList)
                {
                    writer.WriteLine("<link type=\"text/css\" href=\"{0}\" rel=\"stylesheet\"/>".FormatWith(stylesheet));
                }
            }
        }
    }
}