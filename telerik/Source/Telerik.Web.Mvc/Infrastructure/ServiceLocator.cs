// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.Web;

    using Implementation;

    public static class ServiceLocator
    {
        private static readonly Func<IServiceLocator> defaultSingletonFactory = () => new ServiceLocatorImpl(HttpContext.Current.IsDebuggingEnabled);

        private static readonly object syncLock = new object();
        private static Func<IServiceLocator> singletonFactory;
        private static IServiceLocator singleton;

        public static IServiceLocator Current
        {
            [DebuggerStepThrough]
            get
            {
                if (singleton == null)
                {
                    lock (syncLock)
                    {
                        if (singleton == null)
                        {
                            singleton = (singletonFactory != null) ?
                                        singletonFactory() :
                                        ((HttpContext.Current != null) ? defaultSingletonFactory() : null);
                        }
                    }
                }

                return singleton;
            }
        }

        public static void SetCurrent(Func<IServiceLocator> factory)
        {
            Guard.IsNotNull(factory, "factory");

            lock (syncLock)
            {
                if (singleton != null)
                {
                    IDisposable disposable = singleton as IDisposable;

                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }

                    singleton = null;
                }

                singletonFactory = factory;
            }
        }
    }
}