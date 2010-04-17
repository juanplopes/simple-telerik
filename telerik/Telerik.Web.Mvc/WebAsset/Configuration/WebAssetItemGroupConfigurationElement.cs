// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Configuration
{
    using System.Configuration;
    using System.Diagnostics;

    /// <summary>
    /// Web asset group configuration element.
    /// </summary>
    public class WebAssetItemGroupConfigurationElement : ConfigurationElement
    {
        public WebAssetItemGroupConfigurationElement()
        {
            Enabled = true;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            [DebuggerStepThrough]
            get
            {
                return (string)this["name"];
            }

            [DebuggerStepThrough]
            set
            {
                this["name"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the default path.
        /// </summary>
        /// <value>The default path.</value>
        [ConfigurationProperty("defaultPath")]
        public string DefaultPath
        {
            [DebuggerStepThrough]
            get
            {
                return (string)this["defaultPath"];
            }

            [DebuggerStepThrough]
            set
            {
                this["defaultPath"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use telerik content delivery network.
        /// </summary>
        /// <value>
        /// <c>true</c> if [use telerik content delivery network]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("useTelerikContentDeliveryNetwork")]
        public bool? UseTelerikContentDeliveryNetwork
        {
            [DebuggerStepThrough]
            get
            {
                return (bool?)this["useTelerikContentDeliveryNetwork"];
            }

            [DebuggerStepThrough]
            set
            {
                this["useTelerikContentDeliveryNetwork"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the content delivery network URL.
        /// </summary>
        /// <value>The content delivery network URL.</value>
        [ConfigurationProperty("contentDeliveryNetworkUrl")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Should accepth url as string.")]
        public string ContentDeliveryNetworkUrl
        {
            [DebuggerStepThrough]
            get
            {
                return (string)this["contentDeliveryNetworkUrl"];
            }

            [DebuggerStepThrough]
            set
            {
                this["contentDeliveryNetworkUrl"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WebAssetItemGroupConfigurationElement"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("enabled")]
        public bool Enabled
        {
            [DebuggerStepThrough]
            get
            {
                return (bool)this["enabled"];
            }

            [DebuggerStepThrough]
            set
            {
                this["enabled"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        [ConfigurationProperty("version")]
        public string Version
        {
            [DebuggerStepThrough]
            get
            {
                return (string)this["version"];
            }

            [DebuggerStepThrough]
            set
            {
                this["version"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WebAssetItemGroupConfigurationElement"/> is compress.
        /// </summary>
        /// <value><c>true</c> if compress; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("compress", DefaultValue = true)]
        public bool Compress
        {
            [DebuggerStepThrough]
            get
            {
                return (bool)this["compress"];
            }

            [DebuggerStepThrough]
            set
            {
                this["compress"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the cache duration in days.
        /// </summary>
        /// <value>The cache duration in days.</value>
        [ConfigurationProperty("cacheDurationInDays", DefaultValue = 365f)]
        public float CacheDurationInDays
        {
            [DebuggerStepThrough]
            get
            {
                return (float)this["cacheDurationInDays"];
            }

            [DebuggerStepThrough]
            set
            {
                this["cacheDurationInDays"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WebAssetItemGroupConfigurationElement"/> is combined.
        /// </summary>
        /// <value><c>true</c> if combined; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("combined", DefaultValue = true)]
        public bool Combined
        {
            [DebuggerStepThrough]
            get
            {
                return (bool)this["combined"];
            }

            [DebuggerStepThrough]
            set
            {
                this["combined"] = value;
            }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        [ConfigurationProperty("items", IsDefaultCollection = true, IsRequired = true)]
        public WebAssetItemConfigurationElementCollection Items
        {
            [DebuggerStepThrough]
            get
            {
                return (WebAssetItemConfigurationElementCollection)base["items"] ?? new WebAssetItemConfigurationElementCollection();
            }
        }
    }
}