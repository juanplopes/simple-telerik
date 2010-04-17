// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.UI;

    public static class HtmlAttributesContainerExtensions
	{
		public static void AppendCssClass(this IHtmlAttributesContainer container, string cssClass)
		{
			HtmlAttributesExtensions.AppendCssClass(container.HtmlAttributes, cssClass);
		}

		public static void PrependCssClass(this IHtmlAttributesContainer container, string cssClass)
		{
			HtmlAttributesExtensions.PrependCssClass(container.HtmlAttributes, cssClass);
		}

		public static void PrependCssClasses(this IHtmlAttributesContainer container, IEnumerable<string> cssClasses)
		{
			HtmlAttributesExtensions.PrependCssClasses(container.HtmlAttributes, cssClasses);
		}

		public static void AppendCssClasses(this IHtmlAttributesContainer container, IEnumerable<string> cssClasses)
		{
			HtmlAttributesExtensions.AppendCssClasses(container.HtmlAttributes, cssClasses);
		}
	}
}
