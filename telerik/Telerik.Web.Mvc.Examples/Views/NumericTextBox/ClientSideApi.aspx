<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">

<%= Html.Telerik().NumericTextBox()
        .Name("NumericTextBox")
%>

<% using (Html.Configurator("Client API").Begin()) { %>
    <%= Html.TextBox("newValue").ToHtmlString() %><br />
    <button onclick="setValue()">Set Value</button> / <button onclick="getValue()">Get Value</button>
<% } %>
        
	<script type="text/javascript">

	    function setValue() {
	        var input = $("#NumericTextBox").data("tTextBox");

	        input.value($("#newValue").val());
	    }

	    function getValue() {
	        var input = $("#NumericTextBox").data("tTextBox");

	        alert(input.value());
	    }
	</script>
			
</asp:content>

<asp:Content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
		
	    .example .configurator
	    {
	        width: 300px;
	        display: inline-block;
	        *display: inline;
	        zoom: 1;
	        margin-left: 5em;
	        vertical-align: top;
	    }
	    
	    .configurator input
	    {
	        margin: 1.4em 0 .4em;
	    }
	    
	    .example .t-numerictextbox
	    {
	        vertical-align: top;
	        margin-top: 1.3em;
	    }
    </style>
</asp:Content>