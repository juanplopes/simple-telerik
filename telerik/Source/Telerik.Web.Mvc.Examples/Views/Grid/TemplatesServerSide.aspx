<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Customer>>" %>
<asp:Content ID="Content1" contentPlaceHolderID="MainContent" runat="server">
<% 
    Html.Telerik().Grid(Model)
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Template(c => { 
                %><img 
                    alt="<%= c.CustomerID %>" 
                    src="<%= Url.Content("~/Content/Grid/Customers/" + c.CustomerID + ".jpg") %>" 
                  /><% 
            }).Title("Picture");
            columns.Bound(c => c.ContactName).Title("Name");
            columns.Bound(c => c.Phone);
        })
        .Sortable()
        .Scrollable(scrolling => scrolling.Height(250))
        .Pageable()
        .Render();
 %>
</asp:Content>