// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Web.Compilation;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Extensions;
    using Resources;

    public class ControllerTypeCache : IControllerTypeCache
    {
        private readonly object syncLock = new object();
        private IDictionary<string, ILookup<string, Type>> cache;

        private Func<IEnumerable<Assembly>> referencedAssemblies = () => BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(assembly => !assembly.GlobalAssemblyCache);

        public Func<IEnumerable<Assembly>> ReferencedAssemblies
        {
            [DebuggerStepThrough]
            get
            {
                return referencedAssemblies;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNull(value, "value");

                referencedAssemblies = value;
            }
        }

        public Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            Guard.IsNotNull(requestContext, "requestContext");
            Guard.IsNotNullOrEmpty(controllerName, "controllerName");

            object routeNamespacesAsObject;
            Type match;

            if (requestContext != null && requestContext.RouteData.DataTokens.TryGetValue("Namespaces", out routeNamespacesAsObject))
            {
                IEnumerable<string> routeNamespacesAsStrings = routeNamespacesAsObject as IEnumerable<string>;

                if (routeNamespacesAsStrings != null && routeNamespacesAsStrings.Any())
                {
                    HashSet<string> routeNamespaces = new HashSet<string>(routeNamespacesAsStrings, StringComparer.OrdinalIgnoreCase);

                    match = GetControllerTypeWithinNamespaces(requestContext.RouteData.Route,controllerName, routeNamespaces);

                    if (match != null)
                    {
                        return match;
                    }
                }
            }

            // then search in the application's default namespace collection
            if ((requestContext != null) && (ControllerBuilder.Current.DefaultNamespaces.Count > 0))
            {
                HashSet<string> nsDefaults = new HashSet<string>(ControllerBuilder.Current.DefaultNamespaces, StringComparer.OrdinalIgnoreCase);

                match = GetControllerTypeWithinNamespaces(requestContext.RouteData.Route, controllerName, nsDefaults);

                if (match != null)
                {
                    return match;
                }
            }

            return (requestContext != null) ? GetControllerTypeWithinNamespaces(requestContext.RouteData.Route, controllerName, null /* namespaces */) : null;
        }

        private Type GetControllerTypeWithinNamespaces(RouteBase route, string controllerName, IEnumerable<string> namespaces)
        {
            EnsureInitialized();

            ICollection<Type> matchingTypes = GetControllerTypes(controllerName, namespaces);

            switch (matchingTypes.Count)
            {
                case 1:
                    {
                        return matchingTypes.First();
                    }

                case 0:
                    {
                        return null;
                    }

                default:
                    {
                        // we need to generate an exception containing all the controller types
                        StringBuilder typeList = new StringBuilder();
                        foreach (Type matchedType in matchingTypes)
                        {
                            typeList.AppendLine();
                            typeList.Append(matchedType.FullName);
                        }

                        Route castRoute = route as Route;

                        string errorText = castRoute != null ?
                                           String.Format(Culture.CurrentUI, TextResource.ControllerNameAmbiguousWithRouteUrl, controllerName, castRoute.Url, typeList) :
                                           String.Format(Culture.CurrentUI, TextResource.ControllerNameAmbiguousWithoutRouteUrl, controllerName, typeList);

                        throw new InvalidOperationException(errorText);

                        //StringBuilder controllerTypes = new StringBuilder();

                        //foreach (Type matchedType in matchingTypes)
                        //{
                        //    controllerTypes.AppendLine();
                        //    controllerTypes.Append(matchedType.FullName);
                        //}

                        //throw new InvalidOperationException("The controller name '{0}' is ambiguous between the following types:{1}".FormatWith(controllerName, controllerTypes.ToString()));
                    }
            }
        }

        private void EnsureInitialized()
        {
            if (cache == null)
            {
                lock (syncLock)
                {
                    if (cache == null)
                    {
                        IList<Type> controllerTypes = GetAllControllerTypes();

                        IEnumerable<IGrouping<string, Type>> groupedByName = controllerTypes.GroupBy(t => t.Name.Substring(0, t.Name.Length - "Controller".Length), StringComparer.OrdinalIgnoreCase);

                        cache = groupedByName.ToDictionary(g => g.Key, g => g.ToLookup(t => t.Namespace ?? string.Empty, StringComparer.OrdinalIgnoreCase), StringComparer.OrdinalIgnoreCase);
                    }
                }
            }
        }

        private ICollection<Type> GetControllerTypes(string controllerName, IEnumerable<string> namespaces)
        {
            var matchingTypes = new HashSet<Type>();

            ILookup<string, Type> nsLookup;

            if (cache.TryGetValue(controllerName, out nsLookup))
            {
                // this friendly name was located in the cache, now cycle through namespaces
                if (namespaces != null)
                {
                    foreach (string requestedNamespace in namespaces)
                    {
                        foreach (var targetNamespaceGrouping in nsLookup)
                        {
                            if (IsNamespaceMatch(requestedNamespace, targetNamespaceGrouping.Key))
                            {
                                matchingTypes.UnionWith(targetNamespaceGrouping);
                            }
                        }
                    }
                }
                else
                {
                    // if the namespaces parameter is null, search *every* namespace
                    foreach (var nsGroup in nsLookup)
                    {
                        matchingTypes.UnionWith(nsGroup);
                    }
                }
            }

            return matchingTypes;
        }

        private IList<Type> GetAllControllerTypes()
        {
            IList<Type> controllerTypes = new List<Type>();

            Func<Type, bool> isController = type => type != null &&
                                                    type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase) &&
                                                    !type.IsAbstract &&
                                                    typeof(IController).IsAssignableFrom(type);

            foreach (Assembly assembly in ReferencedAssemblies())
            {
                Type[] types;

                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException rte)
                {
                    types = rte.Types;
                }

                controllerTypes.AddRange(types.Where(isController));
            }

            return controllerTypes;
        }

        private static bool IsNamespaceMatch(string requestedNamespace, string targetNamespace)
        {
            // degenerate cases
            if (requestedNamespace == null)
            {
                return false;
            }

            if (requestedNamespace.Length == 0)
            {
                return true;
            }

            if (!requestedNamespace.EndsWith(".*", StringComparison.OrdinalIgnoreCase))
            {
                // looking for exact namespace match
                return String.Equals(requestedNamespace, targetNamespace, StringComparison.OrdinalIgnoreCase);
            }

            // looking for exact or sub-namespace match

            requestedNamespace = requestedNamespace.Substring(0, requestedNamespace.Length - ".*".Length);
            if (!targetNamespace.StartsWith(requestedNamespace, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (requestedNamespace.Length == targetNamespace.Length)
            {
                // exact match
                return true;
            }

            if (targetNamespace[requestedNamespace.Length] == '.')
            {
                // good prefix match, e.g. requestedNamespace = "Foo.Bar" and targetNamespace = "Foo.Bar.Baz"
                return true;
            }

            // bad prefix match, e.g. requestedNamespace = "Foo.Bar" and targetNamespace = "Foo.Bar2"
            return false;
        }
    }
}