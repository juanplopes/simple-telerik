<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content contentPlaceHolderID="MainContent" runat="server">

    <% Html.Telerik().TabStrip()
            .Name("TabStrip")
            .HtmlAttributes(new {
                @class = "t-tabstrip-rtl"
            })
            .Items(tabstrip =>
            {
                tabstrip.Add()
                    .Text("ASP.NET MVC")
				    .ImageUrl("~/Content/Common/Icons/Suites/mvc.png")
                    .Content(() =>
                    {%>
                        <ul>
                            <li>Pure ASP.NET MVC components</li>
                            <li>Completely Open Source</li>
                            <li>Exceptional Performance</li>
                            <li>Based on jQuery</li>
                            <li>Search Engine Optimized</li>
                            <li>Cross-browser support</li>
                        </ul>
                    <%});

                tabstrip.Add()
                    .Text("Silverlight")
                    .ImageUrl("~/Content/Common/Icons/Suites/sl.png")
                    .Content(() =>
                    {%>
                        <ul>
                            <li>Built on Silverlight 3</li>
                            <li>RIA services support</li>
                            <li>Validation support</li>
                            <li>Out of browser support</li>
                            <li>The first commercial 3D chart</li>
                            <li>Free testing framework</li>
                        </ul>
                    <%});

                tabstrip.Add()
                    .Text("ASP.NET AJAX")
				    .ImageUrl("~/Content/Common/Icons/Suites/ajax.png")
                    .Content(() =>
                    {%>
                        <ul>
                            <li>Built on top of Microsoft ASP.NET AJAX framework</li>
                            <li>Rich client-side capabilities; nearly identical client-side and server-side APIs</li>
                            <li>.NET 3.5 built-in support for LINQ, EntityDataSource, ADO.NET DataServices, WCF, etc</li>
                            <li>Performance optimization helper controls and HTTP compression</li>
                            <li>SharePoint and DotNetNuke Integration; ASP.NET MVC-ready</li>
                            <li>Wide cross-browser compatible and XHTML compliant</li>
                        </ul>
                    <%});

                tabstrip.Add()
                    .Text("OpenAccess ORM")
				    .ImageUrl("~/Content/Common/Icons/Suites/orm.png")
                    .Content(() =>
                    {%>
                        <ul>
                            <li>Model First and Schema First approaches</li>
                            <li>Stored Procedures for Multiple Databases</li>
                            <li>Views for Multiple Databases</li>
                            <li>Generic Metadata Access and artificial fields API in the runtime</li>
                            <li>Support for Ado.Net Data Services and WCF</li>
                            <li>Support for LINQ, OQL, and SQL Languages</li>
                        </ul>
                    <%});

                tabstrip.Add()
                    .Text("Reporting")
				    .ImageUrl("~/Content/Common/Icons/Suites/rep.png")
                    .Content(() =>
                    {%>
                        <ul>
                            <li>Excellent data presentation and analysis: Crosstabs, Charts, Tables, Lists</li>
                            <li>SubReports, Barcodes, Images, Shapes, and more</li>
                            <li>Revolutionary WYSIWYG design surface in Visual Studio</li>
                            <li>Easy conditional formatting, sorting, filtering, grouping</li>
                            <li>Powerful styling, data binging and data processing models</li>
                            <li>Significantly reduced development time through wizards and builders</li>
                        </ul>
                    <%});

                tabstrip.Add()
                    .Text("Sitefinity ASP.NET CMS")
				    .ImageUrl("~/Content/Common/Icons/Suites/sitefinity.png")
                    .Content(() =>
                    {%>
                        <ul>
                            <li>Multi-lingual Content Integration</li>
                            <li>Workflow Engine</li>
                            <li>Document versioning</li>
                            <li>Permissions</li>
                            <li>Interface Localization</li>
                            <li>Wide cross-browser compatible and XHTML compliant</li>
                        </ul>
                    <%});
            })
            .SelectedIndex(0)
            .Render();
    %>
</asp:Content>
