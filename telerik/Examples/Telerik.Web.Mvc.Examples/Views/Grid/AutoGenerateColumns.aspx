<%@ Page Title="Auto generate columns" Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<EditableCustomer>>" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
	
<%= Html.Telerik().Grid(Model)
        .Name("Grid")
        .Sortable()
        .Pageable()
%>
</asp:content>
