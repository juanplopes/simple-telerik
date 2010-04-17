<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Order>>" %>

<asp:content contentplaceholderid="MainContent" runat="server">

<%= Html.Telerik().Grid(Model)
		.Name("Grid")
        .ToolBar(commands => commands
            .Custom()
                .HtmlAttributes(new { id = "export" })
                .Text("Export to CSV")
                .Action("ExportCsv", "Grid", new { page = 1, orderBy = "~", filter = "~" }))
        .Columns(columns =>
        {
            columns.Bound(o => o.OrderID).Width(100);
            columns.Bound(o => o.Customer.ContactName).Width(200);
            columns.Bound(o => o.ShipAddress);
            columns.Bound(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(100);
        })
		.DataBinding(dataBinding => dataBinding.Ajax().Select("_CustomCommand", "Grid"))
		.ClientEvents(events => events.OnDataBound("onDataBound"))
        .Pageable()
        .Sortable()
        .Filterable()
%>
<script type="text/javascript">
    function onDataBound() {
        var grid = $(this).data('tGrid');
        
        // Get the export link as jQuery object
        var $exportLink = $('#export');
        
        // Get its 'href' attribute - the URL where it would navigate to
        var href = $exportLink.attr('href');
        
        // Update the 'page' parameter with the grid's current page
        href = href.replace(/page=([^&]*)/, 'page=' + grid.currentPage);
        
        // Update the 'orderBy' parameter with the grids' current sort state
        href = href.replace(/orderBy=([^&]*)/, 'orderBy=' + (grid.orderBy || '~'));
        
        // Update the 'filter' parameter with the grids' current filtering state
        href = href.replace(/filter=(.*)/, 'filter=' + (grid.filterBy || '~'));
        
        // Update the 'href' attribute
        $exportLink.attr('href', href);
    }
</script>
</asp:content>