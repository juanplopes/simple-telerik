// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    using Infrastructure;

    /// <summary>
    /// HTMLHelper extension for providing access to <see cref="ViewComponentFactory"/>.
    /// </summary>
    public static class HtmlHelperExtension
    {
        private static readonly string Key = typeof(ViewComponentFactory).AssemblyQualifiedName;

        /// <summary>
        /// Gets the Telerik View Component Factory
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <returns>The Factory</returns>
        public static ViewComponentFactory Telerik(this HtmlHelper helper)
        {
            Guard.IsNotNull(helper, "helper");

            ViewContext viewContext = helper.ViewContext;
            HttpContextBase httpContext = viewContext.HttpContext;
            ViewComponentFactory factory = httpContext.Items[Key] as ViewComponentFactory;

            if (factory == null)
            {
                IServiceLocator locator = ServiceLocator.Current;

                IWebAssetItemMerger assetItemMerger = locator.Resolve<IWebAssetItemMerger>();
                ScriptWrapperBase scriptWrapper = locator.Resolve<ScriptWrapperBase>();
                IClientSideObjectWriterFactory clientSideObjectWriterFactory = locator.Resolve<IClientSideObjectWriterFactory>();

                StyleSheetRegistrar styleSheetRegistrar = new StyleSheetRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.StyleSheetFilesPath), viewContext, assetItemMerger);
                ScriptRegistrar scriptRegistrar = new ScriptRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.ScriptFilesPath), new List<IScriptableComponent>(), viewContext, assetItemMerger, scriptWrapper);

                StyleSheetRegistrarBuilder styleSheetRegistrarBuilder = StyleSheetRegistrarBuilder.Create(styleSheetRegistrar);
                ScriptRegistrarBuilder scriptRegistrarBuilder = ScriptRegistrarBuilder.Create(scriptRegistrar);

                factory = new ViewComponentFactory(helper, clientSideObjectWriterFactory, styleSheetRegistrarBuilder, scriptRegistrarBuilder);

                httpContext.Items[Key] = factory;
            }
            else
            {
                factory.HtmlHelper = helper;
            }

            return factory;
        }

#if MVC2
        /// <summary>
        /// Gets the Telerik View Component Factory
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <returns>The Factory</returns>
        public static ViewComponentFactory<TModel> Telerik<TModel>(this HtmlHelper<TModel> helper)
        {
            Guard.IsNotNull(helper, "helper");

            ViewContext viewContext = helper.ViewContext;
            HttpContextBase httpContext = viewContext.HttpContext;

            ViewComponentFactory<TModel> factory = httpContext.Items[Key] as ViewComponentFactory<TModel>;

            if (factory == null)
            {
                IServiceLocator locator = ServiceLocator.Current;

                IWebAssetItemMerger assetItemMerger = locator.Resolve<IWebAssetItemMerger>();
                ScriptWrapperBase scriptWrapper = locator.Resolve<ScriptWrapperBase>();
                IClientSideObjectWriterFactory clientSideObjectWriterFactory = locator.Resolve<IClientSideObjectWriterFactory>();

                StyleSheetRegistrar styleSheetRegistrar = httpContext.Items[StyleSheetRegistrar.Key] as StyleSheetRegistrar ??
                                                          new StyleSheetRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.StyleSheetFilesPath), viewContext, assetItemMerger);
                ScriptRegistrar scriptRegistrar = httpContext.Items[ScriptRegistrar.Key] as ScriptRegistrar ??
                                                          new ScriptRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.ScriptFilesPath), new List<IScriptableComponent>(), viewContext, assetItemMerger, scriptWrapper);

                StyleSheetRegistrarBuilder styleSheetRegistrarBuilder = StyleSheetRegistrarBuilder.Create(styleSheetRegistrar);
                ScriptRegistrarBuilder scriptRegistrarBuilder = ScriptRegistrarBuilder.Create(scriptRegistrar);

                factory = new ViewComponentFactory<TModel>(helper, clientSideObjectWriterFactory, styleSheetRegistrarBuilder, scriptRegistrarBuilder);

                httpContext.Items[Key] = factory;
            }
            else
            {
                factory.HtmlHelper = helper;
            }

            return factory;
        }
#endif
    }
}