// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Web;

    using Extensions;

    public class GridBoundColumnContentHtmlBuilder<TModel, TValue> : GridColumnHtmlBuilderBase<TModel, GridBoundColumn<TModel, TValue>>  where TModel : class
    {
        public GridBoundColumnContentHtmlBuilder(GridBoundColumn<TModel, TValue> column) : base(column)
        {
        }

        public override void Html(GridCell<TModel> context, IHtmlNode td)
        {
            Func<string, string> encoder = columnValue => Column.Encoded ? HttpUtility.HtmlEncode(columnValue) : columnValue;

            string content = string.Empty;

            TValue value = Column.Value(context.DataItem);

            if (value != null)
            {
                if (!value.Equals(default(TValue)))
                {
                    content = value.ToString();
                }
            }

            if (string.IsNullOrEmpty(Column.Format))
            {
                td.Html(encoder(content));
            }
            else
            {
                td.Html(encoder(Column.Format.FormatWith(value)));
            }
        }
    }
}