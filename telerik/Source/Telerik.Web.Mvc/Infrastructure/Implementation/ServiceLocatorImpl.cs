// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.Web.Routing;

    using UI;
    
    /// <summary>
    /// Default Service locator.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "Not sure why should I use core instead of Impl.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Impl", Justification = "Suffix of implementation.")]
    public class ServiceLocatorImpl : DisposableBase, IServiceLocator
    {
        private readonly IDictionary<RuntimeTypeHandle, object> services;
        private readonly object servicesSyncLock;
        private readonly IDictionary<RuntimeTypeHandle, Func<IServiceLocator, object>> factories;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocatorImpl"/> class.
        /// </summary>
        /// <param name="debugMode">if set to <c>true</c> [debug mode].</param>
        public ServiceLocatorImpl(bool debugMode)
        {
            services = new Dictionary<RuntimeTypeHandle, object>();
            servicesSyncLock = new object();
            factories = CreateDefaultFactories(debugMode);
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The service type is unknown.")]
        public TService Resolve<TService>()
        {
            RuntimeTypeHandle handle = typeof(TService).TypeHandle;
            object service;

            if (!services.TryGetValue(handle, out service))
            {
                lock (servicesSyncLock)
                {
                    if (!services.TryGetValue(handle, out service))
                    {
                        Func<IServiceLocator, object> factory;

                        if (factories.TryGetValue(handle, out factory))
                        {
                            service = factory(this);
                            services.Add(handle, service);
                        }
                    }
                }
            }

            return (TService) service;
        }

        /// <summary>
        /// Registers the specified service as singleton.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public IServiceLocator Register<TService>(TService service)
        {
            Guard.IsNotNull(service, "service");

            RuntimeTypeHandle handle = typeof(TService).TypeHandle;

            lock (servicesSyncLock)
            {
                object existing;

                if (services.TryGetValue(handle, out existing))
                {
                    if (existing != null)
                    {
                        IDisposable disposable = existing as IDisposable;

                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }
                }

                services[handle] = service;
            }

            return this;
        }

        /// <summary>
        /// Registers the specified factory.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="factory">The factory.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Factory method.")]
        public IServiceLocator Register<TService>(Func<IServiceLocator, object> factory)
        {
            Guard.IsNotNull(factory, "factory");

            factories[typeof(TService).TypeHandle] = factory;

            return this;
        }

        /// <summary>
        /// Disposes the resources.
        /// </summary>
        protected override void DisposeCore()
        {
            lock (servicesSyncLock)
            {
                foreach (KeyValuePair<RuntimeTypeHandle, object> pair in services)
                {
                    if (pair.Value != null)
                    {
                        IDisposable disposable = pair.Value as IDisposable;

                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }
                }

                services.Clear();
            }

            base.DisposeCore();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Can't find a wrokaround.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Can't find a wrokaround.")]
        private static IDictionary<RuntimeTypeHandle, Func<IServiceLocator, object>> CreateDefaultFactories(bool debugMode)
        {
            IDictionary<RuntimeTypeHandle, Func<IServiceLocator, object>> defaultFactories = new Dictionary<RuntimeTypeHandle, Func<IServiceLocator, object>>
                 {
                     { typeof(RouteCollection).TypeHandle, locator => RouteTable.Routes },
                     { typeof(IVirtualPathProvider).TypeHandle, locator => new VirtualPathProviderWrapper() },
                     { typeof(IControllerTypeCache).TypeHandle, locator => new ControllerTypeCache() },
                     { typeof(IActionMethodCache).TypeHandle, locator => new ActionMethodCache(locator.Resolve<IControllerTypeCache>()) },
                     { typeof(IFieldCache).TypeHandle, locator => new FieldCache() },
                     { typeof(IPropertyCache).TypeHandle, locator => new PropertyCache() },
                     { typeof(IObjectCopier).TypeHandle, locator => new ObjectCopier(locator.Resolve<IFieldCache>(), locator.Resolve<IPropertyCache>()) },
                     { typeof(IAuthorizeAttributeCache).TypeHandle, locator => new AuthorizeAttributeCache(locator.Resolve<IControllerTypeCache>(), locator.Resolve<IActionMethodCache>()) },
                     { typeof(IAuthorizeAttributeBuilder).TypeHandle, locator => new AuthorizeAttributeBuilder() },
                     { typeof(IReflectedAuthorizeAttributeCache).TypeHandle, locator => new ReflectedAuthorizeAttributeCache(locator.Resolve<IAuthorizeAttributeBuilder>()) },
                     { typeof(IControllerAuthorization).TypeHandle, locator => new ControllerAuthorization(locator.Resolve<IAuthorizeAttributeCache>(), locator.Resolve<IReflectedAuthorizeAttributeCache>(), locator.Resolve<IObjectCopier>(), locator.Resolve<RouteCollection>()) },
                     { typeof(IUrlAuthorization).TypeHandle, locator => new UrlAuthorization() },
                     { typeof(INavigationItemAuthorization).TypeHandle, locator => new NavigationItemAuthorization(locator.Resolve<IControllerAuthorization>(), locator.Resolve<IUrlAuthorization>()) },
                     { typeof(ICacheManager).TypeHandle, locator => new CacheManagerWrapper() },
                     { typeof(IConfigurationManager).TypeHandle, locator => new ConfigurationManagerWrapper() },
                     { typeof(IFileSystem).TypeHandle, locator => new FileSystemWrapper() },
                     { typeof(IHttpResponseCacher).TypeHandle, locator => new HttpResponseCacher() },
                     { typeof(IHttpResponseCompressor).TypeHandle, locator => new HttpResponseCompressor() },
                     { typeof(IPathResolver).TypeHandle, locator => new PathResolver() },
                     { typeof(IUrlResolver).TypeHandle, locator => new UrlResolver() },
                     { typeof(IWebAssetLocator).TypeHandle, locator => new WebAssetLocator(debugMode, locator.Resolve<IVirtualPathProvider>()) },
                     { typeof(IWebAssetRegistry).TypeHandle, locator => new WebAssetRegistry(debugMode, locator.Resolve<ICacheManager>(), locator.Resolve<IWebAssetLocator>(), locator.Resolve<IUrlResolver>(), locator.Resolve<IPathResolver>(), locator.Resolve<IVirtualPathProvider>()) },
                     { typeof(IUrlEncoder).TypeHandle, locator => new UrlEncoder() },
                     { typeof(IWebAssetItemMerger).TypeHandle, locator => new WebAssetItemMerger(locator.Resolve<IWebAssetRegistry>(), locator.Resolve<IUrlResolver>(), locator.Resolve<IUrlEncoder>()) },
                     { typeof(IUrlGenerator).TypeHandle, locator => new UrlGenerator() },
                     { typeof(IClientSideObjectWriterFactory).TypeHandle, locator => new ClientSideObjectWriterFactory() },
                     { typeof(ScriptWrapperBase).TypeHandle, locator => new ScriptWrapper() },
                     { typeof(IGridHtmlBuilderFactory).TypeHandle, locator => new GridHtmlBuilderFactory() },
                     { typeof(IPanelBarHtmlBuilderFactory).TypeHandle, locator => new PanelBarHtmlBuilderFactory(locator.Resolve<IActionMethodCache>()) },
                     { typeof(IMenuHtmlBuilderFactory).TypeHandle, locator => new MenuHtmlBuilderFactory(locator.Resolve<IActionMethodCache>()) },
                     { typeof(ITabStripHtmlBuilderFactory).TypeHandle, locator => new TabStripHtmlBuilderFactory(locator.Resolve<IActionMethodCache>()) },
                     { typeof(IDatePickerHtmlBuilderFactory).TypeHandle, locator => new DatePickerHtmlBuilderFactory() },
                     { typeof(ICalendarHtmlBuilderFactory).TypeHandle, locator => new CalendarHtmlBuilderFactory() },
                     { typeof(ITreeViewHtmlBuilderFactory).TypeHandle, locator => new TreeViewHtmlBuilderFactory(locator.Resolve<IActionMethodCache>()) },
                     { typeof(ITextboxBaseHtmlBuilderFactory<short>).TypeHandle, locator => new TextboxBaseHtmlBuilderFactory<short>() },
                     { typeof(ITextboxBaseHtmlBuilderFactory<int>).TypeHandle, locator => new TextboxBaseHtmlBuilderFactory<int>() },
                     { typeof(ITextboxBaseHtmlBuilderFactory<float>).TypeHandle, locator => new TextboxBaseHtmlBuilderFactory<float>() },
                     { typeof(ITextboxBaseHtmlBuilderFactory<double>).TypeHandle, locator => new TextboxBaseHtmlBuilderFactory<double>() },
                     { typeof(ITextboxBaseHtmlBuilderFactory<decimal>).TypeHandle, locator => new TextboxBaseHtmlBuilderFactory<decimal>() }
                 };
            
            return defaultFactories;
        }
    }
}