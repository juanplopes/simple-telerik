// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions
{
    using System.Collections.Generic;
    using System.Text;

	public static class HtmlAttributesExtensions
	{
		public static void AppendCssClass(IDictionary<string, object> htmlAttributes, string cssClass)
		{
			htmlAttributes.AppendInValue("class", " ", cssClass);
		}

		public static void PrependCssClass(IDictionary<string, object> htmlAttributes, string cssClass)
		{
			htmlAttributes.PrependInValue("class", " ", cssClass);
		}

		public static void PrependCssClasses(IDictionary<string, object> htmlAttributes, IEnumerable<string> cssClasses)
		{
			var sb = new StringBuilder();

			cssClasses.Each(a =>
			{
				sb.Append(a);
				sb.Append(" ");
			});

			htmlAttributes.PrependInValue("class", " ", sb.ToString().Trim());
		}

		public static void AppendCssClasses(IDictionary<string, object> htmlAttributes, IEnumerable<string> cssClasses)
		{
			var sb = new StringBuilder();

			cssClasses.Each(a =>
			{
				sb.Append(" ");
				sb.Append(a);
			});

			htmlAttributes.AppendInValue("class", " ", sb.ToString().Trim());
		}
	}
}
