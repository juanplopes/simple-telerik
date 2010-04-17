<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content contentPlaceHolderID="MainContent" runat="server">

	<% Html.Telerik().Menu()
            .Name("Menu")
			.Items(menu => {
                menu.Add()
					.Text("Products > Books")
					.Content(() => 
                    {%>
						<ul class="product-list">
                            <li class="product">
                                <img
									src="<%= Url.Content("~/Content/PanelBar/Templates/exposure.gif")%>"
									alt="Understanding Exposure" />
                                <p>
									Understanding Exposure:<br />
									How to Shoot Great Photographs
								</p>
                            </li>
                            <li class="product">
                                <img
									src="<%= Url.Content("~/Content/PanelBar/Templates/digitalCamera.gif")%>"
									alt="The Ultimage Guide" />
                                <p>
									Get the Most from Your<br />
									Digital Camera: The Ultimage Guide
								</p>
                            </li>
                            <li class="product">
                                <img
									src="<%= Url.Content("~/Content/PanelBar/Templates/slr.gif")%>"
									alt="Digital SLR Cameras" />
                                <p>
									Digital SLR Cameras:<br />
									Photography for Dummies
								</p>
                            </li>
                        </ul>
					<% });

                menu.Add()
					.Text("Customer testmonials")
					.Content(() =>
                    {%>
					    <img
					        src="<%= Url.Content("~/Content/PanelBar/Templates/Testimonials.gif")%>"
					        alt="Testimonials" width="740" height="157" />
					<%});

                menu.Add()
                    .Text("How to find us")
                    .Content(() =>
                    {%>
                        <img
                            src="<%= Url.Content("~/Content/PanelBar/Templates/How-to-find-us.gif")%>"
                            alt="How to find us" width="740" height="156" />               
                    <%});
			})
			.Render();
	%>
</asp:Content>

<asp:Content contentPlaceHolderID="HeadContent" runat="server">
	<style type="text/css">
		#Menu .t-content
		{
			background-color: #edf9fe;
			color: #333;
		}
		
		.product-list
		{
			padding: 0;
			margin: 0;
		}
		
		.product
		{
		    text-align: center;
			list-style: none;
			display: inline-block;
			*display: inline;
			zoom: 1;
			margin: 10px;
			padding: 5px;
			border: 2px solid #ccc;
			-moz-border-radius: 4px;
			-webkit-border-radius: 4px;
			border-radius: 4px;
			background: #fff;
		}
	</style>
</asp:Content>