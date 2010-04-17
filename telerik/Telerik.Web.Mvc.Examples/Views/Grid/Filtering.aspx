<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Order>>" %>

<asp:content contentplaceholderid="MainContent" runat="server">

<% using (Html.Configurator("Filter by ...")
              .PostTo("Filtering", "Grid")
              .Begin())
   { %>
    <p>
        <label for="startsWith"><strong>Contact Name</strong> starts with:</label>
        <%= Html.TextBox("startsWith", "Paul") %>
    </p>
    
    <button class="t-button t-state-default" type="submit">Apply</button>
<% } %>

<%= Html.Telerik().Grid(Model)
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Bound(o => o.OrderID).Width(120);
            columns.Bound(o => o.Customer.ContactName).Width(200);
            columns.Bound(o => o.ShipAddress);
            columns.Bound(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(100);
        })
        .DataBinding(dataBinding => dataBinding.Ajax().Select("_Filtering", "Grid"))
        .Pageable()
        .Sortable()
        .Filterable(filtering => filtering
            .Filters(filters => filters
                .Add(o => o.Customer.ContactName).StartsWith((string)ViewData["startsWith"])
        ))
%>
</asp:content>
