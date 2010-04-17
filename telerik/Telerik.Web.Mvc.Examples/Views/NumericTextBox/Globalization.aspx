<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:content contentplaceholderid="MainContent" runat="server">

<div class="section">
    <%= Html.Telerik().NumericTextBox()
            .Name("NumericTextBox")
            .Value(1234.12)
            .MinValue(-10000)
            .MaxValue(10000)
    %>

    <%= Html.Telerik().CurrencyTextBox()
            .Name("CurrencyTextBox")
            .Value(1234.12m)
            .MinValue(-10000)
            .MaxValue(10000)
    %>

    <%= Html.Telerik().PercentTextBox()
            .Name("PercentTextBox")
            .Value(87.12)
            .MinValue(-10000)
            .MaxValue(10000)
    %>
</div>

<% Html.RenderPartial("CulturePicker"); %>

<% Html.Telerik().ScriptRegistrar().Globalization(true); %>

</asp:content>

<asp:content contentplaceholderid="HeadContent" runat="server">
    <style type="text/css">
        .section,
        .configurator
        {
            float: left;
            margin: 0 200px 1.3em 0;
            width: 200px;
        }
        
        .example .t-numerictextbox
        {
            display: block;
            margin-bottom: 1.3em;
        }
    </style>
</asp:content>
