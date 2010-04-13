<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


<asp:content contentplaceholderid="MainContent" runat="server">

<h3>Bound to ASMX Web Service</h3>
<%= Html.Telerik().Grid<EditableCustomer>()
        .Name("AsmxGrid")
        .ToolBar(commands => commands.Insert())
        .DataKeys(keys => 
        {
            keys.Add(c => c.CustomerID);
        })
        .DataBinding(dataBinding => dataBinding
            .WebService()
                .Select("~/Models/Customers.asmx/Select")
                .Update("~/Models/Customers.asmx/Update")
                .Insert("~/Models/Customers.asmx/Insert")
                .Delete("~/Models/Customers.asmx/Delete")
        )
        .Columns(columns =>
        {
            columns.Bound(c => c.ContactName);
            columns.Bound(c => c.Country).Width(170);
            columns.Bound(c => c.Address).Width(180);
            columns.Bound(c => c.BirthDay).Width(130).Format("{0:d}");
            columns.Command(commands =>
            {
                commands.Edit();
                commands.Delete();
            }).Width(190).Title("Commands");
        })
        .Scrollable()
        .Pageable()
        .Sortable()
%>

<h3>Bound to WCF Web Service</h3>
<%= Html.Telerik().Grid<EditableCustomer>()
        .Name("WcfGrid")
        .ToolBar(commands => commands.Insert())
        .DataKeys(keys => 
        {
            keys.Add(c => c.CustomerID);
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.WebService()
                .Select("~/Models/Customers.svc/Select")
                .Update("~/Models/Customers.svc/Update")
                .Insert("~/Models/Customers.svc/Insert")
                .Delete("~/Models/Customers.svc/Delete");
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.ContactName);
            columns.Bound(c => c.Country).Width(170);
            columns.Bound(c => c.Address).Width(180);
            columns.Bound(c => c.BirthDay).Width(130).Format("{0:d}");
            columns.Command(commands =>
            {
                commands.Edit();
                commands.Delete();
            }).Width(190);
        })
        .Scrollable()
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
