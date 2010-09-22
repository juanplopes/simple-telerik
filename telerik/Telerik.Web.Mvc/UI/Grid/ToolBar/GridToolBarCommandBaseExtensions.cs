// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Extensions;
    using Infrastructure;

    public static class GridToolBarCommandBaseExtensions
    {
#if MVC2
        public static string ButtonContent<T>(this GridToolBarCommandBase<T> command, string localizedText, string cssClass) where T : class
        {
            switch (command.ButtonType)
            {
                case GridButtonType.Image:
                    {
                        return getImageOrSprite(command, cssClass);
                    }
                case GridButtonType.ImageAndText:
                    {
                        return getImageOrSprite(command, cssClass) + localizedText;
                    }
                default:
                    {
                        return localizedText;
                    }
            }
        }

        private static string getImageOrSprite<T>(GridToolBarCommandBase<T> command, string cssClass) where T : class
        {
            IHtmlNode span = new HtmlTag("span")
                                .Attributes(command.ImageHtmlAttributes)
                                .PrependClass(UIPrimitives.Icon);

            if (cssClass.HasValue()) span.PrependClass(cssClass);

            return span.ToString();
        }
#endif
    }
}