<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content contentPlaceHolderID="MainContent" runat="server">
	<div id="overview">
		<div id="overview-spotlight">
			<div id="big-logo">Telerik Extensions for ASP.NET MVC</div>
			
			<p>Telerik Extensions for ASP.NET MVC extend the ASP.NET MVC framework by delivering a server-based framework that integrates with client-side modules based on popular JavaScript library, jQuery.</p>
			
			<p id="how-to-get">
				<a href="http://www.telerik.com/community/license-agreement.aspx?pId=697" id="download-link">Download Now!</a><br />
				<span id="version-info">Version <%= ViewData["ProductVersion"] %> (<a href="http://www.telerik.com/products/aspnet-mvc/whats-new/release-history.aspx">release notes</a>)</span>
			</p>
		</div>
		
		<ul id="extensions-highlights">
			<li>
				<h2>Pure ASP.NET MVC Components</h2>
				<p>Built on top of <a href="http://www.asp.net/mvc/">ASP.NET MVC</a> to leverage its values - lightweight rendering, clean HTML, separation of concerns, and testability.</p>
			</li>
			<li>
				<h2>Completely Open Source</h2>
				<p>The Extensions are licensed under the widely adopted <strong>GPLv2</strong>. A <a href="http://www.telerik.com/purchase/faqs/aspnet-mvc.aspx">commercial license with support</a> is also available.</p>
			</li>
			<li>
				<h2>Exceptional Performance</h2>
				<p>No postbacks, no ViewState, and no page lifecycle. The Web Asset Managers optimize the delivery of CSS and JavaScript, so no precious HTTP requests are wasted.</p>
			</li>
			<li>
				<h2>Based on jQuery</h2>
				<p>Telerik Extensions draw on the power of <a href="http://jquery.com/">jQuery</a> for visual effects and DOM manipulations.</p>
			</li>
			<li>
				<h2>Search Engine Optimized</h2>
				<p>The Extensions render clean, semantic HTML, which is essential for indexing your content in the major search engines.</p>
			</li>
			<li>
				<h2>Cross-browser Support</h2>
				<p>Telerik Extensions for ASP.NET MVC support all major browsers - Internet&nbsp;Explorer, Firefox, Safari, Opera and Google Chrome.</p>
			</li>
		</ul>
	</div>
		
		<div id="product-first-glance"></div>
</asp:Content>