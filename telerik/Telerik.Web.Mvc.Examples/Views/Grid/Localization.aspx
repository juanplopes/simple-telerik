<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content contentPlaceHolderID="MainContent" runat="server">

<% Html.RenderPartial("CulturePicker"); %>

<%= Html.Telerik().Grid<EditableCustomer>()
        .Name("Grid")
        .DataKeys(keys => 
        {
            keys.Add(c => c.CustomerID);
        })
        .ToolBar(commands => commands.Insert())
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectAjaxEditing", "Grid")
                .Insert("_InsertAjaxEditing", "Grid")
                .Update("_SaveAjaxEditing", "Grid")
                .Delete("_DeleteAjaxEditing", "Grid");
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.ContactName);
            columns.Bound(c => c.Country).Width(170);
            columns.Bound(c => c.Address).Width(200);
            columns.Bound(c => c.BirthDay).Width(130).Format("{0:d}");
            columns.Command(commands =>
            {
                commands.Edit();
                commands.Delete();
            }).Width(180);
        })
        .Pageable()
        .Sortable()
        .Filterable()
        .Scrollable()
        .Groupable()
%>

<% Html.Telerik().ScriptRegistrar().Globalization(true); %>
</asp:Content>