// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// The default web asset merger.
    /// </summary>
    public class WebAssetItemMerger : IWebAssetItemMerger
    {
        private readonly IWebAssetRegistry assetRegistry;
        private readonly IUrlResolver urlResolver;
        private readonly IUrlEncoder urlEncoder;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetItemMerger"/> class.
        /// </summary>
        /// <param name="assetRegistry">The asset registry.</param>
        /// <param name="urlResolver">The URL resolver.</param>
        /// <param name="urlEncoder">The URL encoder.</param>
        public WebAssetItemMerger(IWebAssetRegistry assetRegistry, IUrlResolver urlResolver, IUrlEncoder urlEncoder)
        {
            Guard.IsNotNull(assetRegistry, "assetRegistry");
            Guard.IsNotNull(urlResolver, "urlResolver");
            Guard.IsNotNull(urlEncoder, "urlEncoder");

            this.assetRegistry = assetRegistry;
            this.urlResolver = urlResolver;
            this.urlEncoder = urlEncoder;
        }

        /// <summary>
        /// Merges the specified assets.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="assetHandlerPath">The asset handler path.</param>
        /// <param name="isSecured">if set to <c>true</c> [is secure].</param>
        /// <param name="canCompress">if set to <c>true</c> [can compress].</param>
        /// <param name="assets">The assets.</param>
        /// <returns></returns>
        public IList<string> Merge(string contentType, string assetHandlerPath, bool isSecured, bool canCompress, WebAssetItemCollection assets)
        {
            Guard.IsNotNullOrEmpty(contentType, "contentType");
            Guard.IsNotNullOrEmpty(assetHandlerPath, "assetHandlerPath");
            Guard.IsNotNull(assets, "assets");

            IList<string> mergedList = new List<string>();

            foreach (IWebAssetItem asset in assets)
            {
                WebAssetItem item = asset as WebAssetItem;
                WebAssetItemGroup itemGroup = asset as WebAssetItemGroup;

                if (item != null)
                {
                    mergedList.Add(Resolve(item.Source, null));
                }
                else if (itemGroup != null)
                {
                    IList<string> groupResult = MergeGroup(contentType, assetHandlerPath, isSecured, canCompress, itemGroup);

                    if (groupResult != null)
                        mergedList.AddRange(groupResult);
                }
            }

            return mergedList.ToList();
        }

        public IList<string> MergeGroup(string contentType, string assetHandlerPath, bool isSecured, bool canCompress, WebAssetItemGroup group)
        {
            Guard.IsNotNullOrEmpty(contentType, "contentType");
            Guard.IsNotNullOrEmpty(assetHandlerPath, "assetHandlerPath");
            Guard.IsNotNull(group, "group");

            IList<string> mergedList = new List<string>();

            WebAssetItemGroup itemGroup = group;

            if (itemGroup != null)
            {
                if (!itemGroup.Enabled)
                {
                    return null;
                }

                if (!string.IsNullOrEmpty(itemGroup.ContentDeliveryNetworkUrl))
                {
                    mergedList.Add(itemGroup.ContentDeliveryNetworkUrl);
                }
                else
                {
                    WebAssetItemGroup frameworkGroup = null;

                    if (itemGroup.UseTelerikContentDeliveryNetwork)
                    {
                        frameworkGroup = FilterFrameworkGroup(itemGroup);
                    }

                    if ((frameworkGroup != null) && !frameworkGroup.Items.IsEmpty())
                    {
                        ProcessGroup(frameworkGroup, contentType, assetHandlerPath, mergedList);
                    }

                    if (itemGroup.UseTelerikContentDeliveryNetwork)
                    {
                        var nativeFiles = FilterNativeFiles(itemGroup);
                        foreach (string nativefile in nativeFiles)
                        {
                            string fullUrl = GetNativeFileCdnUrl(nativefile, isSecured, canCompress);

                            if (!mergedList.Contains(fullUrl, StringComparer.OrdinalIgnoreCase))
                            {
                                mergedList.Add(fullUrl);
                            }
                        }
                    }

                    if (!itemGroup.Items.IsEmpty())
                    {
                        ProcessGroup(itemGroup, contentType, assetHandlerPath, mergedList);
                    }
                }
            }

            return mergedList.ToList();
        }

        private string Resolve(string path, string version)
        {
            return urlResolver.Resolve(assetRegistry.Locate(path, version));
        }

        private void ProcessGroup(WebAssetItemGroup group, string contentType, string assetHandlerPath, IList<string> urls)
        {
            if (group.Combined)
            {
                var fullUrls = FilterFullUrls(group);
                foreach (string fullUrl in fullUrls)
                {
                    if (!urls.Contains(fullUrl, StringComparer.OrdinalIgnoreCase))
                    {
                        urls.Add(fullUrl);
                    }
                }

                string id = assetRegistry.Store(contentType, group);
                string virtualPath = "{0}?{1}={2}".FormatWith(assetHandlerPath, urlEncoder.Encode(WebAssetHttpHandler.IdParameterName), urlEncoder.Encode(id));
                string relativePath = urlResolver.Resolve(virtualPath);

                if (!urls.Contains(relativePath, StringComparer.OrdinalIgnoreCase))
                {
                    urls.Add(relativePath);
                }
            }
            else
            {
                group.Items.Each(i =>
                {
                    if (!urls.Contains(i.Source, StringComparer.OrdinalIgnoreCase))
                    {
                        urls.Add(Resolve(i.Source, group.Version));
                    }
                });
            }
        }

        private static WebAssetItemGroup FilterFrameworkGroup(WebAssetItemGroup itemGroup)
        {
            WebAssetItemGroup frameworkGroup = new WebAssetItemGroup("framework", false) { Combined = itemGroup.Combined, Compress = itemGroup.Compress, CacheDurationInDays = itemGroup.CacheDurationInDays, DefaultPath = itemGroup.DefaultPath, Version = itemGroup.Version, UseTelerikContentDeliveryNetwork = itemGroup.UseTelerikContentDeliveryNetwork, Enabled = itemGroup.Enabled };

            for (int i = itemGroup.Items.Count - 1; i >= 0; i--)
            {
                WebAssetItem item = itemGroup.Items[i];

                string fileName = Path.GetFileName(item.Source);

                if ((!fileName.Equals(ScriptRegistrar.jQuery, StringComparison.OrdinalIgnoreCase)) && (ScriptRegistrar.FrameworkScriptFileNames.Contains(fileName, StringComparer.OrdinalIgnoreCase)))
                {
                    frameworkGroup.Items.Add(new WebAssetItem(item.Source));
                    itemGroup.Items.RemoveAt(i);
                }
            }

            frameworkGroup.Items.Reverse();

            return frameworkGroup;
        }

        private static IList<string> FilterNativeFiles(WebAssetItemGroup itemGroup)
        {
            List<string> nativeFiles = new List<string>();

            for (int i = itemGroup.Items.Count - 1; i >= 0; i--)
            {
                WebAssetItem item = itemGroup.Items[i];

                if (IsNativeFile(item))
                {
                    nativeFiles.Add(Path.GetFileName(item.Source));
                    itemGroup.Items.RemoveAt(i);
                }
            }

            nativeFiles.Reverse();

            return nativeFiles;
        }

        private static IEnumerable<string> FilterFullUrls(WebAssetItemGroup itemGroup)
        {
            List<string> fullUrls = new List<string>();

            for (int i = itemGroup.Items.Count - 1; i >= 0; i--)
            {
                WebAssetItem item = itemGroup.Items[i];

                if (item.Source.IndexOf("://") > -1)
                {
                    fullUrls.Add(item.Source);
                    itemGroup.Items.RemoveAt(i);
                }
            }

            fullUrls.Reverse();

            return fullUrls;
        }

        private static bool IsNativeFile(WebAssetItem item)
        {
            if (item.Source.StartsWith("~/", StringComparison.Ordinal))
            {
                string fileName = Path.GetFileName(item.Source);

                return fileName.Equals(ScriptRegistrar.jQuery, StringComparison.OrdinalIgnoreCase) ||
                       fileName.Equals(ScriptRegistrar.jQueryValidation, StringComparison.OrdinalIgnoreCase) ||
                       fileName.StartsWith("Telerik.", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        private static string GetNativeFileCdnUrl(string fileName, bool isSecured, bool canCompress)
        {
            Func<string, string, string> append = (path, segment) => path + (path.EndsWith("/", StringComparison.Ordinal) ? string.Empty : "/") + segment;
            string extension = Path.GetExtension(fileName);

            bool isJs = extension.IsCaseInsensitiveEqual(".js");
            bool isCss = extension.IsCaseInsensitiveEqual(".css");
            string basePath;

            if (isJs)
            {
                basePath = isSecured ?
                           WebAssetDefaultSettings.TelerikContentDeliveryNetworkSecureScriptUrl :
                           WebAssetDefaultSettings.TelerikContentDeliveryNetworkScriptUrl;
            }
            else if (isCss)
            {
                basePath = isSecured ?
                           WebAssetDefaultSettings.TelerikContentDeliveryNetworkSecureStyleSheetUrl :
                           WebAssetDefaultSettings.TelerikContentDeliveryNetworkStyleSheetUrl;
            }
            else
            {
                throw new InvalidOperationException("Unknown file type \"{0}\".".FormatWith(extension));
            }

            string productName = canCompress ? "mvcz" : "mvc";

            basePath = append(basePath, productName);
            basePath = append(basePath, WebAssetDefaultSettings.Version);

            extension = isCss ? ".min.css" : ".min.js";

            return append(basePath, Path.ChangeExtension(fileName, extension));
        }
    }
}