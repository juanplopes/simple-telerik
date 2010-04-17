<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" contentPlaceHolderID="MainContent" runat="server">
<%= Html.Telerik().Grid<Customer>()
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Bound(c => c.CustomerID)
                .ClientTemplate("<img width='100' height='110' alt='<#= CustomerID #>' src='" 
                    + Url.Content("~/Content/Grid/Customers/") 
                    + "<#= CustomerID #>.jpg' />")
                .Title("Picture");
            columns.Bound(c => c.ContactName).Title("Name");
            columns.Bound(c => c.Phone);
        })
        .DataBinding(dataBinding => dataBinding.Ajax().Select("_TemplatesClientSide", "Grid"))
        .Sortable()
        .Scrollable()
        .Pageable()
 %>
</asp:Content>