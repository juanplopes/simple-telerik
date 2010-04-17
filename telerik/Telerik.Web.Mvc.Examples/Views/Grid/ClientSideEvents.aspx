<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Examples.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
    function onLoad(e) {
        $console.log("Grid loaded");
    }

    function onDataBinding(e) {
        $console.log("OnDataBinding");
    }

    function onDataBound(e) {
        $console.log("OnDataBound");
    }

    function onRowDataBound(e) {
        var dataItem = e.dataItem;
        $console.log("OnRowDataBound :: " + dataItem.OrderID);
    }

    function onRowSelected(e) {
        var row = e.row;
        $console.log("OnRowSelected :: " + row.cells[0].innerHTML);
    }
    </script>

    <%= Html.Telerik().Grid<Order>()
            .Name("Grid")
            .Columns(columns =>
            {
                columns.Bound(o => o.OrderID).Width(100);
                columns.Bound(o => o.Customer.ContactName).Width(200);
                columns.Bound(o => o.ShipAddress);
                columns.Bound(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(120);
            })
            .ClientEvents(events => events
                .OnLoad("onLoad")
                .OnDataBinding("onDataBinding")
                .OnRowDataBound("onRowDataBound")
                .OnRowSelected("onRowSelected")
                .OnDataBound("onDataBound")
            )
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_ClientSideEvents_Ajax", "Grid"))
            .Pageable(paging => paging.PageSize(4))
            .Sortable()
            .Selectable()
            .Filterable()
            .HtmlAttributes(new { style = "margin-bottom: 1.3em" }) 
    %>

    <% Html.RenderPartial("EventLog"); %>
</asp:content>
