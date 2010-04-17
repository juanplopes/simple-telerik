<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<EditableOrder>>" %>
<asp:content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%= Html.Telerik()
        .Grid(Model)
        .Name("Grid")
        .DataKeys(keys => keys.Add(o => o.OrderID).RouteKey("Id"))
        .DataBinding(dataBinding => dataBinding.Server()
           .Select("DisplayAndEditTemplates", "Grid")
           .Update("UpodateOrder", "Grid"))
        .Columns(columns =>
        {
            columns.Bound(o => o.OrderID).Width(100);
            columns.Bound(o => o.Employee).Width(230);
            columns.Bound(o => o.OrderDate).Width(150);
            columns.Bound(o => o.Freight).Width(220);
            columns.Command(commands => commands.Edit()).Title("Edit").Width(200);
        })
        .Pageable()
%>
</asp:Content>
<asp:content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
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
</asp:Content>