// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions
{
    using Telerik.Web.Mvc.UI;

    public static class ContentContainerExtensions
    {
        public static void AppendContentCssClass(this IContentContainer container, string cssClass)
        {
            HtmlAttributesExtensions.AppendCssClass(container.ContentHtmlAttributes, cssClass);
        }
    }
}
