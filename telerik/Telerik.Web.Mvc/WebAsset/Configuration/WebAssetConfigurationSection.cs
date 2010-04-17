// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Configuration
{
    using System.Configuration;
    using System.Diagnostics;

    using Infrastructure;

    /*
    <configSections>
        <sectionGroup name="telerik">
            <section name="webAssets" type="Telerik.Web.Mvc.Configuration.WebAssetConfigurationSection, Telerik.Web.Mvc"/>
        </sectionGroup>
    </configSections>
    <telerik>
        <webAssets useTelerikContentDeliveryNetwork="true">
            <styleSheets>
                <add name="" defaultPath="" contentDeliveryNetworkUrl="">
                    <items>
                        <add source=""/>
                        <add source=""/>
                        <add source=""/>
                    </items>
                </add>
            </styleSheets>
            <scripts>
                <add name="" defaultPath="" contentDeliveryNetworkUrl="">
                    <items>
                        <add source=""/>
                        <add source=""/>
                        <add source=""/>
                    </items>
                </add>
            </scripts>
        </webAssets>
    </telerik>
    */

    /// <summary>
    /// The web asset Configuration.
    /// </summary>
    public class WebAssetConfigurationSection : ConfigurationSection
    {
        private static string sectionName = "telerik/webAssets";

        /// <summary>
        /// Gets the name of the section.
        /// </summary>
        /// <value>The name of the section.</value>
        public static string SectionName
        {
            [DebuggerStepThrough]
            get
            {
                return sectionName;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                sectionName = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use telerik content delivery network.
        /// </summary>
        /// <value>
        /// <c>true</c> if [use telerik content delivery network]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("useTelerikContentDeliveryNetwork")]
        public bool UseTelerikContentDeliveryNetwork
        {
            [DebuggerStepThrough]
            get
            {
                return (bool)this["useTelerikContentDeliveryNetwork"];
            }

            [DebuggerStepThrough]
            set
            {
                this["useTelerikContentDeliveryNetwork"] = value;
            }
        }

        /// <summary>
        /// Gets the style sheets.
        /// </summary>
        /// <value>The style sheets.</value>
        [ConfigurationProperty("styleSheets")]
        public WebAssetItemGroupConfigurationElementCollection StyleSheets
        {
            [DebuggerStepThrough]
            get
            {
                return (WebAssetItemGroupConfigurationElementCollection) base["styleSheets"] ?? new WebAssetItemGroupConfigurationElementCollection();
            }
        }

        /// <summary>
        /// Gets the scripts.
        /// </summary>
        /// <value>The scripts.</value>
        [ConfigurationProperty("scripts")]
        public WebAssetItemGroupConfigurationElementCollection Scripts
        {
            [DebuggerStepThrough]
            get
            {
                return (WebAssetItemGroupConfigurationElementCollection) base["scripts"] ?? new WebAssetItemGroupConfigurationElementCollection();
            }
        }
    }
}