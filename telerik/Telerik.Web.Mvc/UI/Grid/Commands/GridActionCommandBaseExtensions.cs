// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Infrastructure;

    public static class GridActionCommandBaseExtensions
    {
#if MVC2
        public static string ButtonContent(this GridActionCommandBase command, string localizedText, string cssClass)
        {
            switch (command.ButtonType)
            {
                case GridButtonType.Image:
                    {
                        return getSpriteHtml(command, cssClass);
                    }
                case GridButtonType.ImageAndText:
                    {
                        return getSpriteHtml(command, cssClass) + localizedText;
                    }
                default:
                    {
                        return localizedText;
                    }
            }
        }

        private static string getSpriteHtml(GridActionCommandBase command, string cssClass)
        {
                return new HtmlTag("span")
                            .Attributes(command.ImageHtmlAttributes)
                            .PrependClass(UIPrimitives.Icon, cssClass)
                            .ToString();
        }
#endif
    }
}