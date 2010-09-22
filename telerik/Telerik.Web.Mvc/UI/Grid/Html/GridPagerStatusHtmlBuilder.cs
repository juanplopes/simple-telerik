namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridPagerStatusHtmlBuilder : HtmlBuilderBase
    {
        private readonly GridLocalization localization;

        public GridPagerStatusHtmlBuilder(GridLocalization localization)
        {
            this.localization = localization;
        }

        public int FirstItemInPage
        {
            get;
            set;
        }

        public int LastItemInPage
        {
            get;
            set;
        }

        public int Total
        {
            get;
            set;
        }

        protected override IHtmlNode BuildCore()
        {
            return new HtmlTag("div")
                        .AddClass("t-status-text")
                        .Text(localization.DisplayingItems.FormatWith(FirstItemInPage, LastItemInPage, Total));
        }
    }
}
