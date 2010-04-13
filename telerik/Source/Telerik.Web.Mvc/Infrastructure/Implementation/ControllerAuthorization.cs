// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class ControllerAuthorization : IControllerAuthorization
    {
        private static readonly Type defaultAuthorizeAttributeType = typeof(AuthorizeAttribute);

        private readonly IAuthorizeAttributeCache authorizeAttributeCache;
        private readonly IReflectedAuthorizeAttributeCache reflectedAuthorizeAttributeCache;
        private readonly IObjectCopier objectCopier;
        private readonly RouteCollection routes;

        public ControllerAuthorization(IAuthorizeAttributeCache authorizeAttributeCache, IReflectedAuthorizeAttributeCache reflectedAuthorizeAttributeCache, IObjectCopier objectCopier, RouteCollection routes)
        {
            Guard.IsNotNull(authorizeAttributeCache, "authorizeAttributeCache");
            Guard.IsNotNull(reflectedAuthorizeAttributeCache, "reflectedAuthorizeAttributeCache");
            Guard.IsNotNull(objectCopier, "objectCopier");
            Guard.IsNotNull(routes, "routes");

            this.authorizeAttributeCache = authorizeAttributeCache;
            this.reflectedAuthorizeAttributeCache = reflectedAuthorizeAttributeCache;
            this.objectCopier = objectCopier;
            this.routes = routes;
        }

        public bool IsAccessibleToUser(RequestContext requestContext, string routeName)
        {
            Guard.IsNotNull(requestContext, "requestContext");
            Guard.IsNotNullOrEmpty(routeName, "routeName");

            RouteBase route = routes[routeName];
            RouteData routeData = route.GetRouteData(requestContext.HttpContext);

            string controllerName = routeData.GetRequiredString("controller");
            string actionName = routeData.GetRequiredString("action");

            return IsAccessibleToUser(requestContext, controllerName, actionName);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We will not allow if there is any exception.")]
        public bool IsAccessibleToUser(RequestContext requestContext, string controllerName, string actionName)
        {
            Guard.IsNotNull(requestContext, "requestContext");
            Guard.IsNotNullOrEmpty(controllerName, "controllerName");
            Guard.IsNotNullOrEmpty(actionName, "actionName");

            IEnumerable<AuthorizeAttribute> authorizeAttributes = authorizeAttributeCache.GetAuthorizeAttributes(requestContext, controllerName, actionName);
            bool allowed = true;

            foreach (AuthorizeAttribute authorizeAttribute in authorizeAttributes)
            {
                if (authorizeAttribute != null)
                {
                    try
                    {
                        Type currentAuthorizationAttributeType = authorizeAttribute.GetType();
                        bool isDefaultAttribute = (currentAuthorizationAttributeType == defaultAuthorizeAttributeType);

                        IAuthorizeAttribute subclassedAttribute = isDefaultAttribute ?
                                                                  new InternalAuthorizeAttribute() : // No need to use Reflection.Emit if it is the asp.net mvc built-in attribute
                                                                  reflectedAuthorizeAttributeCache.GetAttribute(currentAuthorizationAttributeType);

                        subclassedAttribute.Order = authorizeAttribute.Order;
                        subclassedAttribute.Roles = authorizeAttribute.Roles;
                        subclassedAttribute.Users = authorizeAttribute.Users;

                        if (!isDefaultAttribute)
                        {
                            // Copy the remaining properties (if there is any)
                            objectCopier.Copy(authorizeAttribute, subclassedAttribute, "Order", "Roles", "Users" /* Excluded properties */);
                        }

                        allowed = subclassedAttribute.IsAuthorized(requestContext.HttpContext);
                    }
                    catch
                    {
                        // do not allow on exception
                        allowed = false;
                    }

                    if (!allowed)
                    {
                        break;
                    }
                }
            }

            return allowed;
        }

        private sealed class InternalAuthorizeAttribute : AuthorizeAttribute, IAuthorizeAttribute
        {
            public bool IsAuthorized(HttpContextBase httpContext)
            {
                return AuthorizeCore(httpContext);
            }
        }
    }
}