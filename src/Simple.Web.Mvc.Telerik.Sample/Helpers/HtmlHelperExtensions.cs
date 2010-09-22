using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Collections;
using System.Linq.Expressions;
using Simple.Expressions;
using System.IO;
using System.Web.Routing;
using Simple.Reflection;
using System.Text;
using System.Web.Caching;
using System.Web.Compilation;
using System.Globalization;

public static class HtmlHelperExtensions
{

    public static string Script(this HtmlHelper helper, string fileName)
    {
        TagBuilder builder = new TagBuilder("script");
        builder.MergeAttribute("type", "text/javascript");
        builder.MergeAttribute("language", "javascript");
        builder.MergeAttribute("src", GetAbsolutePath(HttpContext.Current, "Scripts/{0}".FormatWith(fileName)));
        return builder.ToString();
    }

    public static string Stylesheet(this HtmlHelper helper, string fileName)
    {
        TagBuilder builder = new TagBuilder("link");
        builder.MergeAttribute("rel", "stylesheet");
        builder.MergeAttribute("type", "text/css");
        builder.MergeAttribute("href", GetAbsolutePath(HttpContext.Current, "Content/{0}".FormatWith(fileName)));
        return builder.ToString();
    }

    public static string Image(this HtmlHelper helper, string id, string fileName, string alternateText)
    {
        return Image(helper, id, fileName, alternateText, null);
    }

    public static string Image(this HtmlHelper helper, string id, string fileName, string alternateText, object htmlAttributes)
    {
        var builder = new TagBuilder("img");
        builder.GenerateId(id);
        builder.MergeAttribute("src", GetAbsolutePath(HttpContext.Current, "Content/Images/{0}".FormatWith(fileName)));
        builder.MergeAttribute("alt", alternateText);
        builder.MergeAttributes(DictionaryHelper.FromAnonymous(htmlAttributes));
        return builder.ToString(TagRenderMode.SelfClosing);
    }

    private static string GetAbsolutePath(this HttpContext context, string path)
    {
        return Path.Combine(context.Request.ApplicationPath, path);
        //return VirtualPathUtility.MakeRelative(context.Request.Url.AbsolutePath, path);
    }

    public static string FormatWith(this string format, params object[] args)
    {
        if (format == null)
            throw new ArgumentNullException("format");
        return string.Format(format, args);
    }

    public static string Home(this UrlHelper helper)
    {
        return helper.Content("~/");
    }
}

