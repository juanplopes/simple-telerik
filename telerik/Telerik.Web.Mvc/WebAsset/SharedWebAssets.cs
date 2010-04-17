// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc
{
    using System;
    using System.Collections.Generic;

    using Configuration;
    using Infrastructure;
    using UI;

    /// <summary>
    /// 
    /// </summary>
    public static class SharedWebAssets
    {
        private static readonly IDictionary<string, WebAssetItemGroup> styleSheets = new Dictionary<string, WebAssetItemGroup>(StringComparer.OrdinalIgnoreCase);
        private static readonly IDictionary<string, WebAssetItemGroup> scripts = new Dictionary<string, WebAssetItemGroup>(StringComparer.OrdinalIgnoreCase);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Ignore this issue since we want the configured assets available when the class is loaded.")]
        static SharedWebAssets()
        {
            LoadFromConfiguration(ServiceLocator.Current.Resolve<IConfigurationManager>());
        }

        /// <summary>
        /// Executes the provided delegate that is used to configure stylesheets.
        /// </summary>
        /// <param name="configureAction">The configure action.</param>
        public static void StyleSheets(Action<SharedWebAssetGroupBuilder> configureAction)
        {
            Configure(WebAssetDefaultSettings.StyleSheetFilesPath, styleSheets, configureAction);
        }

        /// <summary>
        /// Executes the provided delegate that is used to configure scripts.
        /// </summary>
        /// <param name="configureAction">The configure action.</param>
        public static void Scripts(Action<SharedWebAssetGroupBuilder> configureAction)
        {
            Configure(WebAssetDefaultSettings.ScriptFilesPath, scripts, configureAction);
        }

        internal static WebAssetItemGroup FindStyleSheetGroup(string name)
        {
            return FindInternal(styleSheets, name);
        }

        internal static WebAssetItemGroup FindScriptGroup(string name)
        {
            return FindInternal(scripts, name);
        }

        private static WebAssetItemGroup FindInternal(IDictionary<string, WebAssetItemGroup> lookup, string name)
        {
            WebAssetItemGroup group;

            return lookup.TryGetValue(name, out group) ? group : null;
        }

        private static void Configure(string defaultPath, IDictionary<string, WebAssetItemGroup> target, Action<SharedWebAssetGroupBuilder> configureAction)
        {
            Guard.IsNotNull(configureAction, "configureAction");

            SharedWebAssetGroupBuilder builder = new SharedWebAssetGroupBuilder(defaultPath, target);

            configureAction(builder);
        }

        private static void LoadFromConfiguration(IConfigurationManager configurationManager)
        {
            WebAssetConfigurationSection section = configurationManager.GetSection<WebAssetConfigurationSection>(WebAssetConfigurationSection.SectionName);

            if (section != null)
            {
                LoadGroups(section.StyleSheets, styleSheets, WebAssetDefaultSettings.StyleSheetFilesPath, WebAssetDefaultSettings.Version);
                LoadGroups(section.Scripts, scripts, WebAssetDefaultSettings.ScriptFilesPath, WebAssetDefaultSettings.Version);
            }
        }

        private static void LoadGroups(WebAssetItemGroupConfigurationElementCollection source, IDictionary<string, WebAssetItemGroup> destination, string defaultPath, string defaultVersion)
        {
            foreach (WebAssetItemGroupConfigurationElement configurationGroup in source)
            {
                WebAssetItemGroup group = new WebAssetItemGroup(configurationGroup.Name, true)
                                              {
                                                  DefaultPath = !string.IsNullOrEmpty(configurationGroup.DefaultPath) ? configurationGroup.DefaultPath : defaultPath,
                                                  ContentDeliveryNetworkUrl = configurationGroup.ContentDeliveryNetworkUrl,
                                                  Enabled = configurationGroup.Enabled,
                                                  Version = !string.IsNullOrEmpty(configurationGroup.Version) ? configurationGroup.Version : defaultVersion,
                                                  Compress = configurationGroup.Compress,
                                                  CacheDurationInDays = configurationGroup.CacheDurationInDays,

                                                  Combined = configurationGroup.Combined
                                              };

                if (configurationGroup.UseTelerikContentDeliveryNetwork.HasValue)
                {
                    group.UseTelerikContentDeliveryNetwork = configurationGroup.UseTelerikContentDeliveryNetwork.Value;
                }

                foreach (WebAssetItemConfigurationElement configurationItem in configurationGroup.Items)
                {
                    string itemSource = configurationItem.Source.StartsWith("~/", StringComparison.OrdinalIgnoreCase) ?
                                        configurationItem.Source :
                                        PathHelper.CombinePath(group.DefaultPath, configurationItem.Source);

                    group.Items.Add(new WebAssetItem(itemSource));
                }

                destination.Add(group.Name, group);
            }
        }
    }
}