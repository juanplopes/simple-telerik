// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc
{
    using Telerik.Web.Mvc.Extensions;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Web;

    using Infrastructure;

    /// <summary>
    /// The HttpHandler to compress, cache and combine web assets.
    /// </summary>
    public class WebAssetHttpHandler : HttpHandlerBase
    {
        private readonly IWebAssetRegistry assetRegistry;
        private readonly IHttpResponseCompressor httpResponseCompressor;
        private readonly IHttpResponseCacher httpResponseCacher;

        private static string defaultPath = "~/asset.axd";
        private static string idParameterName = "id";

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetHttpHandler"/> class.
        /// </summary>
        /// <param name="assetRegistry">The asset registry.</param>
        /// <param name="httpResponseCompressor">The HTTP response compressor.</param>
        /// <param name="httpResponseCacher">The HTTP response cacher.</param>
        public WebAssetHttpHandler(IWebAssetRegistry assetRegistry, IHttpResponseCompressor httpResponseCompressor, IHttpResponseCacher httpResponseCacher)
        {
            Guard.IsNotNull(assetRegistry, "assetRegistry");
            Guard.IsNotNull(httpResponseCompressor, "httpResponseCompressor");
            Guard.IsNotNull(httpResponseCacher, "httpResponseCacher");

            this.assetRegistry = assetRegistry;
            this.httpResponseCompressor = httpResponseCompressor;
            this.httpResponseCacher = httpResponseCacher;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetHttpHandler"/> class.
        /// </summary>
        public WebAssetHttpHandler(): this(ServiceLocator.Current.Resolve<IWebAssetRegistry>(), ServiceLocator.Current.Resolve<IHttpResponseCompressor>(), ServiceLocator.Current.Resolve<IHttpResponseCacher>())
        {
        }

        /// <summary>
        /// Gets or sets the default path of the asset.
        /// </summary>
        /// <value>The default path.</value>
        public static string DefaultPath
        {
            [DebuggerStepThrough]
            get
            {
                return defaultPath;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                defaultPath = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the id parameter.
        /// </summary>
        /// <value>The name of the id parameter.</value>
        public static string IdParameterName
        {
            [DebuggerStepThrough]
            get
            {
                return idParameterName;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                idParameterName = value;
            }
        }

        /// <summary>
        /// Enables a WebAssetHttpHandler object to process of requests.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void ProcessRequest(HttpContextBase context)
        {
            string id = context.Request.QueryString[IdParameterName];

            if (!string.IsNullOrEmpty(id))
            {
                WebAsset asset = assetRegistry.Retrieve(id);

                if (asset != null)
                {
                    HttpResponseBase response = context.Response;

                    // Set the content type
                    response.ContentType = asset.ContentType;

                    string content = asset.Content;

                    if (!string.IsNullOrEmpty(content))
                    {
                        // Compress
                        if (asset.Compress && !context.IsMono())
                        {
                            httpResponseCompressor.Compress(context);
                        }

                        // Write
                        using (StreamWriter sw = new StreamWriter((response.OutputStream)))
                        {
                            sw.Write(content);
                        }

                        // Cache
                        if (!context.IsDebuggingEnabled)
                        {
                            httpResponseCacher.Cache(context, TimeSpan.FromDays(asset.CacheDurationInDays));
                        }
                    }
                }
            }
        }
    }
}