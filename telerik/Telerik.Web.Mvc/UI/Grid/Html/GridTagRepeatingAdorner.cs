// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridTagRepeatingAdorner : IHtmlAdorner
    {
        public GridTagRepeatingAdorner(int repeatCount)
        {
            Guard.IsNotNegative(repeatCount, "repeatCount");
            
            RepeatCount = repeatCount;
            TagName = "td";
            RenderMode = TagRenderMode.Normal;
            Nbsp = true;
            CssClasses = new List<string>();
        }

        public TagRenderMode RenderMode
        {
            get;
            set;
        }

        public int RepeatCount
        {
            get;
            private set;
        } 

        public IList<string> CssClasses
        {
            get;
            private set;
        }

        public bool Nbsp
        {
            get;
            set;
        }

        public string TagName
        {
            get;
            set;
        }

        public void ApplyTo(IHtmlNode target)
        {
            for (int i = 0; i < RepeatCount; i++)
            {
                var tag = new HtmlTag(TagName, RenderMode)
                            .AddClass(CssClasses.ToArray());
                
                if (Nbsp)
                {
                    tag.Html("&nbsp;");
                }

                target.Children.Insert(i, tag);
            }
        }
    }
}