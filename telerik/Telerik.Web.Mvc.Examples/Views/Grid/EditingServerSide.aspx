<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<EditableCustomer>>" %>


<asp:content contentplaceholderid="MainContent" runat="server">

<%= Html.Telerik().Grid(Model)
        .Name("Grid")
        .DataKeys(keys => keys.Add(c => c.CustomerID))
        .ToolBar(commands => commands.Insert())
        .DataBinding(dataBinding => dataBinding.Server()
           .Select("EditingServerSide", "Grid")
           .Insert("Insert", "Grid")
           .Update("Save", "Grid")
           .Delete("Delete", "Grid"))
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
            }).Width(180).Title("Commands");
        })
        .Pageable()
        .Sortable()
%>

</asp:content>

<asp:content contentplaceholderid="HeadContent" runat="server">
<style type="text/css">
    .field-validation-error
    {
        position: absolute;
        display: block;
    }
    
    * html .field-validation-error { position: relative; }
    *+html .field-validation-error { position: relative; }
    
    .field-validation-error span
    {
        position: absolute;
        white-space: nowrap;
        color: red;
        padding: 17px 5px 3px;
        background: transparent url('<%= Url.Content("~/Content/Common/validation-error-message.png") %>') no-repeat 0 0;
    }
</style>
</asp:content>
