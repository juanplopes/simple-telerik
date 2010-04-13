<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<OrderDto>" %>
<asp:content contentPlaceHolderId="MainContent" runat="server">

    <% Html.EnableClientValidation(); %>
    
    <% using (Html.BeginForm())
    { %>
        <ul id="field-list">
            <li>
                <%= Html.LabelFor(x => x.ContactName)%>
                <%= Html.TextBoxFor(x => x.ContactName) %>
                <%= Html.ValidationMessageFor(x => x.ContactName)%>
            </li>
            <li>
                <%= Html.LabelFor(x => x.OrderDate)%>
                <%= Html.Telerik().DatePicker().Name("OrderDate").Value(Model.OrderDate)%>
                <%= Html.ValidationMessageFor(x => x.OrderDate)%>
            </li>
            <li>
                <%= Html.LabelFor(x => x.ShipAddress)%>
                <%= Html.TextAreaFor(x => x.ShipAddress) %>
                <%= Html.ValidationMessageFor(x => x.ShipAddress)%>
            </li>
            <li>
                <button class="t-button t-state-default" type="submit">Post</button>
            </li>
        </ul>
    <% } %>   
</asp:content>

<asp:content contentPlaceHolderId="HeadContent" runat="server">

    <% Html.Telerik().ScriptRegistrar()
                     .DefaultGroup(group => group
                         .Add("MicrosoftAjax.js")
                         .Add("MicrosoftMvcValidation.js")); %>
                         
    <style type="text/css">
        .field-validation-error { color: red; position: absolute; margin: 0 0 0 5px; }
        
        #field-list
        {
            display: inline-block; *display: inline; zoom: 1;
        }
        
        #field-list li
        {
            list-style-type: none;
            padding-bottom: 5px;
        }
        
        #field-list label
        {
            display: inline-block; *display: inline; zoom: 1;
            width: 120px; text-align: right; padding-right: 5px;
            vertical-align: top;
            padding-top: 2px;
        }
        
        #field-list input,
        #field-list textarea
        {
            font: normal 12px Tahoma;
            width: 130px;
        }
        
        #field-list button
        {
            margin: 0 19px 0 auto;
        }
    </style>
</asp:content>