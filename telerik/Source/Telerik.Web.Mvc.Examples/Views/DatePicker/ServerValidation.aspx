<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
    
    <% using (Html.Configurator("Validation summary").Begin()) { %>
        <%= Html.ValidationSummary() %>
    <% } %>

    <% using (Html.BeginForm("servervalidation", "datepicker")) { %>

        <div>
            <label for="DatePicker-input">Cake delivery date (required):</label>
            <%= Html.Telerik()
                    .DatePicker()
                    .Name("deliveryDate")
            %>
            <%= Html.ValidationMessage("deliveryDate", "*")%>
        </div>

        <p>
            <button class="t-button t-state-default" type="submit">Post</button>
        </p>
    
    <% } %>
    
    <% if (ViewData["deliveryDate"] != null)
       { %>
             <p><strong>Posted value is : <%= ViewData["deliveryDate"]%></strong></p>
    <% } %>
</asp:content>

<asp:content contentPlaceHolderId="HeadContent" runat="server">
   
    <style type="text/css">
        .field-validation-error { color: red; position: absolute; margin: 0 0 0 5px; }
    </style>
</asp:content>