// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Web.Script.Serialization;
    using Extensions;
    using Infrastructure;
    using UI;

    /// <summary>
    /// The default web asset registry.
    /// </summary>
    public class WebAssetRegistry : IWebAssetRegistry
    {
        private static readonly Regex urlRegEx = new Regex(@"url\s*\((\""|\')?(?<path>[^)]+)?(\""|\')?\)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

        private static readonly ReaderWriterLockSlim syncLock = new ReaderWriterLockSlim();

        private readonly bool isInDebugMode;
        private readonly ICacheManager cacheManager;
        private readonly IWebAssetLocator assetLocator;
        private readonly IUrlResolver urlResolver;
        private readonly IPathResolver pathResolver;
        private readonly IVirtualPathProvider virtualPathProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetRegistry"/> class.
        /// </summary>
        /// <param name="isInDebugMode">if set to <c>true</c> [is in debug mode].</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="assetLocator">The asset locator.</param>
        /// <param name="urlResolver">The URL resolver.</param>
        /// <param name="pathResolver">The path resolver.</param>
        /// <param name="virtualPathProvider">The virtual path provider.</param>
        public WebAssetRegistry(bool isInDebugMode, ICacheManager cacheManager, IWebAssetLocator assetLocator, IUrlResolver urlResolver, IPathResolver pathResolver, IVirtualPathProvider virtualPathProvider)
        {
            Guard.IsNotNull(cacheManager, "cacheManager");
            Guard.IsNotNull(assetLocator, "assetLocator");
            Guard.IsNotNull(urlResolver, "urlResolver");
            Guard.IsNotNull(pathResolver, "pathResolver");
            Guard.IsNotNull(virtualPathProvider, "virtualPathProvider");

            this.isInDebugMode = isInDebugMode;
            this.cacheManager = cacheManager;
            this.assetLocator = assetLocator;
            this.urlResolver = urlResolver;
            this.pathResolver = pathResolver;
            this.virtualPathProvider = virtualPathProvider;
        }

        /// <summary>
        /// Stores the specified asset group.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="assetGroup">The asset group.</param>
        /// <returns></returns>
        public string Store(string contentType, WebAssetItemGroup assetGroup)
        {
            Guard.IsNotNullOrEmpty(contentType, "contentType");
            Guard.IsNotNull(assetGroup, "assetGroup");

            MergedAsset mergedAsset = CreateMergedAssetWith(contentType, assetGroup);
            string id = assetGroup.IsShared ? assetGroup.Name : CreateIdFrom(mergedAsset);

            EnsureAsset(mergedAsset, id);

            return id;
        }

        /// <summary>
        /// Retrieves the web asset by specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public WebAsset Retrieve(string id)
        {
            Guard.IsNotNullOrEmpty(id, "id");

            MergedAsset mergedAsset = CreateMergedAssetFromConfiguration(id) ?? CreateMergedAssetFromUrl(id);
            WebAssetHolder assetHolder = EnsureAsset(mergedAsset, id);

            return new WebAsset(assetHolder.Asset.ContentType, assetHolder.Asset.Version, assetHolder.Asset.Compress, assetHolder.Asset.CacheDurationInDays, assetHolder.Content);
        }

        /// <summary>
        /// Returns the correct virtual path based upon the debug mode and version.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public string Locate(string virtualPath, string version)
        {
            return assetLocator.Locate(virtualPath, version);
        }

        private static MergedAsset CreateMergedAssetFromConfiguration(string id)
        {
            WebAssetItemGroup assetGroup = SharedWebAssets.FindScriptGroup(id);
            string contentType = "application/x-javascript";

            if (assetGroup == null)
            {
                assetGroup = SharedWebAssets.FindStyleSheetGroup(id);
                contentType = "text/css";
            }

            return (assetGroup != null) ? CreateMergedAssetWith(contentType, assetGroup) : null;
        }

        private static MergedAsset CreateMergedAssetFromUrl(string id)
        {
            string decompressed = Decompress(Decode(id));
            MergedAsset mergedAsset = Deserialize(decompressed);

            return mergedAsset;
        }

        private static string CreateIdFrom(MergedAsset mergedAsset)
        {
            string serialized = Serialize(mergedAsset);
            string id = Encode(Compress(serialized));

            return id;
        }

        private static MergedAsset CreateMergedAssetWith(string contentType, WebAssetItemGroup assetGroup)
        {
            Func<string, string> getDirectory = path => path.Substring(2, path.LastIndexOf("/", StringComparison.Ordinal) - 2);
            Func<string, string> getFile = path => path.Substring(path.LastIndexOf("/", StringComparison.Ordinal) + 1);

            MergedAsset asset = new MergedAsset
                                    {
                                        ContentType = contentType,
                                        Version = assetGroup.Version,
                                        Compress = assetGroup.Compress,
                                        CacheDurationInDays = assetGroup.CacheDurationInDays
                                    };

            IEnumerable<string> directories = assetGroup.Items.Select(item => getDirectory(item.Source)).Distinct(StringComparer.OrdinalIgnoreCase);

            directories.Each(directory => asset.Directories.Add(new MergedAssetDirectory { Path = directory }));

            for (int i = 0; i < assetGroup.Items.Count; i++)
            {
                string item = assetGroup.Items[i].Source;
                string directory = getDirectory(item);
                string file = getFile(item);

                MergedAssetDirectory assetDirectory = asset.Directories.Single(d => d.Path.IsCaseInsensitiveEqual(directory));

                assetDirectory.Files.Add(new MergedAssetFile { Order = i, Name = file });
            }

            return asset;
        }

        private static string Serialize(MergedAsset mergedAsset)
        {
            JavaScriptSerializer serializer = CreateSerializer();

            string json = serializer.Serialize(mergedAsset);

            return json;
        }

        private static MergedAsset Deserialize(string json)
        {
            JavaScriptSerializer serializer = CreateSerializer();

            MergedAsset mergedAsset = serializer.Deserialize<MergedAsset>(json);

            return mergedAsset;
        }

        private static string Encode(string target)
        {
            return target.Replace("/", "_").Replace("+", "-");
        }

        private static string Decode(string target)
        {
            return target.Replace("-", "+").Replace("_", "/");
        }

        private static string Compress(string target)
        {
            return target.Compress();
        }

        private static string Decompress(string target)
        {
            return target.Decompress();
        }

        private static JavaScriptSerializer CreateSerializer()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            serializer.RegisterConverters(new JavaScriptConverter[] { new MergedAssetJsonConverter(), new MergedAssetDirectoryJsonConverter(), new MergedAssetFileJsonConverter() });

            return serializer;
        }

        private WebAssetHolder EnsureAsset(MergedAsset asset, string id)
        {
            string key = "{0}:{1}".FormatWith(GetType().AssemblyQualifiedName, id);
            WebAssetHolder assetHolder;

            using (syncLock.ReadAndWrite())
            {
                assetHolder = GetWebAssetHolder(key);

                if (assetHolder == null)
                {
                    using (syncLock.Write())
                    {
                        assetHolder = GetWebAssetHolder(key);

                        if (assetHolder == null)
                        {
                            List<string> physicalPaths = new List<string>();
                            StringBuilder contentBuilder = new StringBuilder();

                            var files = asset.Directories
                                             .SelectMany(d => d.Files.Select(f => new { Directory = d, File = f }))
                                             .OrderBy(f => f.File.Order);

                            foreach (var pair in files)
                            {
                                string path = "~/" + pair.Directory.Path + "/" + pair.File.Name;

                                string virtualPath = assetLocator.Locate(path, asset.Version);
                                string fileContent = virtualPathProvider.ReadAllText(virtualPath);

                                if (string.Compare(asset.ContentType, "text/css", StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    string baseDiretory = virtualPathProvider.GetDirectory(virtualPath);

                                    fileContent = ReplaceImagePath(baseDiretory, fileContent);
                                }

                                contentBuilder.AppendLine(fileContent);

                                physicalPaths.Add(pathResolver.Resolve(virtualPath));
                            }

                            assetHolder = new WebAssetHolder { Asset = asset, Content = contentBuilder.ToString() };
                            cacheManager.Insert(key, assetHolder, null, physicalPaths.ToArray());
                        }
                    }
                }
            }

            return assetHolder;
        }

        private WebAssetHolder GetWebAssetHolder(string key)
        {
            return isInDebugMode ? null : cacheManager.GetItem(key) as WebAssetHolder;
        }

        private string ReplaceImagePath(string baseDiretory, string content)
        {
            content = urlRegEx.Replace(content, new MatchEvaluator(match =>
            {
                string path = match.Groups["path"].Value.Trim("'\"".ToCharArray());

                if (path.HasValue()
                    && !path.StartsWith("http://", StringComparison.OrdinalIgnoreCase) 
                    && !path.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                    path = virtualPathProvider.CombinePaths(baseDiretory, path);

                    return "url('{0}')".FormatWith(urlResolver.Resolve(path));
                }

                return "url('{0}')".FormatWith(path);
            }));

            return content;
        }

        [Serializable]
        private sealed class WebAssetHolder
        {
            public MergedAsset Asset
            {
                get;
                set;
            }

            public string Content
            {
                get;
                set;
            }
        }

        [Serializable]
        private sealed class MergedAsset
        {
            public MergedAsset()
            {
                Directories = new List<MergedAssetDirectory>();
            }

            public string ContentType
            {
                get;
                set;
            }

            public string Version
            {
                get;
                set;
            }

            public bool Compress
            {
                get;
                set;
            }

            public float CacheDurationInDays
            {
                get;
                set;
            }

            public IList<MergedAssetDirectory> Directories
            {
                get;
                private set;
            }
        }

        [Serializable]
        private sealed class MergedAssetDirectory
        {
            public MergedAssetDirectory()
            {
                Files = new List<MergedAssetFile>();
            }

            public string Path
            {
                get;
                set;
            }

            public IList<MergedAssetFile> Files
            {
                get;
                private set;
            }
        }

        [Serializable]
        private sealed class MergedAssetFile
        {
            public int Order
            {
                get;
                set;
            }

            public string Name
            {
                get;
                set;
            }
        }

        private sealed class MergedAssetJsonConverter : JavaScriptConverter
        {
            public override IEnumerable<Type> SupportedTypes
            {
                [DebuggerStepThrough]
                get
                {
                    yield return typeof(MergedAsset);
                }
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                MergedAsset mergedAsset = (MergedAsset) obj;

                IDictionary<string, object> dictionary = new Dictionary<string, object>
                                                             {
                                                                 { "ct", mergedAsset.ContentType },
                                                                 { "v", mergedAsset.Version },
                                                                 { "c", mergedAsset.Compress },
                                                                 { "cd", mergedAsset.CacheDurationInDays },
                                                                 { "d", mergedAsset.Directories }
                                                             };

                return dictionary;
            }

            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                MergedAsset mergedAsset = new MergedAsset
                                              {
                                                  ContentType = serializer.ConvertToType<string>(dictionary["ct"]),
                                                  Version = serializer.ConvertToType<string>(dictionary["v"]),
                                                  Compress = serializer.ConvertToType<bool>(dictionary["c"]),
                                                  CacheDurationInDays = serializer.ConvertToType<float>(dictionary["cd"])
                                              };

                mergedAsset.Directories.AddRange(serializer.ConvertToType<IList<MergedAssetDirectory>>(dictionary["d"]));

                return mergedAsset;
            }
        }

        private sealed class MergedAssetDirectoryJsonConverter : JavaScriptConverter
        {
            public override IEnumerable<Type> SupportedTypes
            {
                [DebuggerStepThrough]
                get
                {
                    yield return typeof(MergedAssetDirectory);
                }
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                MergedAssetDirectory mergedAssetDirectory = (MergedAssetDirectory) obj;

                IDictionary<string, object> dictionary = new Dictionary<string, object>
                                                             {
                                                                 { "p", mergedAssetDirectory.Path },
                                                                 { "f", mergedAssetDirectory.Files }
                                                             };

                return dictionary;
            }

            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                MergedAssetDirectory mergedAssetDirectory = new MergedAssetDirectory
                                                                {
                                                                    Path = serializer.ConvertToType<string>(dictionary["p"])
                                                                };

                mergedAssetDirectory.Files.AddRange(serializer.ConvertToType<IList<MergedAssetFile>>(dictionary["f"]));

                return mergedAssetDirectory;
            }
        }

        private sealed class MergedAssetFileJsonConverter : JavaScriptConverter
        {
            public override IEnumerable<Type> SupportedTypes
            {
                [DebuggerStepThrough]
                get
                {
                    yield return typeof(MergedAssetFile);
                }
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                MergedAssetFile mergedAssetFile = (MergedAssetFile) obj;

                IDictionary<string, object> dictionary = new Dictionary<string, object>
                                                             {
                                                                 { "o", mergedAssetFile.Order },
                                                                 { "n", mergedAssetFile.Name }
                                                             };

                return dictionary;
            }

            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                MergedAssetFile mergedAssetFile = new MergedAssetFile
                                                      {
                                                          Order = serializer.ConvertToType<int>(dictionary["o"]),
                                                          Name = serializer.ConvertToType<string>(dictionary["n"])
                                                      };

                return mergedAssetFile;
            }
        }
    }
}