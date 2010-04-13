// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    
    using Extensions;
    using Infrastructure;

    public static class GridExtensions
    {
        public static string GroupTitle<T>(this Grid<T> grid, GroupDescriptor group)
            where T : class
        {
            GridColumnBase<T> column = grid.Columns.Where(c => c.Member == group.Member).FirstOrDefault();
            if (column != null)
            {
                return column.Title;
            }

            return group.Member.AsTitle();
        }

        public static bool IsInInsertMode<T>(this Grid<T> grid)
            where T : class
        {
            return grid.GetGridParameter<string>(GridUrlParameters.Mode).IsCaseInsensitiveEqual("insert");
        }
        
        public static bool IsInEditMode<T>(this Grid<T> grid)
            where T : class
        {
            return grid.GetGridParameter<string>(GridUrlParameters.Mode).IsCaseInsensitiveEqual("edit");
        }

        public static bool IsRecordSelected<T>(this Grid<T> grid, T dataItem)
            where T : class
        {
            if (!grid.DataKeys.Any())
            {
                return false;
            }

            if (!grid.GetGridParameter<string>(GridUrlParameters.Mode).IsCaseInsensitiveEqual("select"))
            {
                return false;
            }

            return grid.AreDataKeysEqual(dataItem);
        }

        public static bool IsRecordInEditMode<T>(this Grid<T> grid, T dataItem)
            where T : class
        {
            if (!grid.DataKeys.Any())
            {
                return false;
            }

            if (!grid.GetGridParameter<string>(GridUrlParameters.Mode).IsCaseInsensitiveEqual("edit"))
            {
                return false;
            }

            foreach (IGridDataKey<T> dataKey in grid.DataKeys)
            {
                object key = dataKey.GetValue(dataItem);
                
                if (key == null)
                {
                    return false;
                }

                if (!key.ToString().IsCaseInsensitiveEqual(grid.ViewContext.Controller.ValueOf<string>(dataKey.RouteKey)))
                {
                    return false;
                }
            }

            return grid.AreDataKeysEqual(dataItem);
        }
        
        private static bool AreDataKeysEqual<T>(this Grid<T> grid, T dataItem)
            where T : class
        {
            foreach (IGridDataKey<T> dataKey in grid.DataKeys)
            {
                object key = dataKey.GetValue(dataItem);

                if (key == null)
                {
                    return false;
                }

                if (!key.ToString().IsCaseInsensitiveEqual(grid.ViewContext.Controller.ValueOf<string>(dataKey.RouteKey)))
                {
                    return false;
                }
            }
            
            return true;
        }

#if MVC2
        public static void WriteDataKeys<T>(this Grid<T> grid, T dataItem, IHtmlNode parent)
            where T : class
        {
            HtmlHelper<T> helper = new HtmlHelper<T>(grid.ViewContext, new GridViewDataContainer
            {
                ViewData = new ViewDataDictionary(grid.ViewContext.ViewData)
                {
                    Model = dataItem
                }
            });

            grid.DataKeys.Each(dataKey =>
            {
                new LiteralNode(dataKey.HiddenFieldHtml(helper)).AppendTo(parent);
            });
        }
#endif
    }
}
