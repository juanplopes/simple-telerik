<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:content contentplaceholderid="MainContent" runat="server">

<% Html.Telerik().TreeView()
        .Name("TreeView")
        .Items(treeview =>
        {
            treeview.Add()
                .Text("UI Components")
                .Expanded(true)
                .Content(() =>
                {%>
                    <ul>
                        <li>
                            <img src="<%= Url.Content("~/Content/Common/Icons/Suites_32/AJAX.png") %>" />
                            RadControls for ASP.NET AJAX
                        </li>
                        <li>
                            <img src="<%= Url.Content("~/Content/Common/Icons/Suites_32/MVC.png") %>" />
                            Telerik Extensions for ASP.NET MVC
                        </li>
                        <li>
                            <img src="<%= Url.Content("~/Content/Common/Icons/Suites_32/SL.png") %>" />
                            RadControls for Silverlight
                        </li>
                        <li>
                            <img src="<%= Url.Content("~/Content/Common/Icons/Suites_32/WinForms.png") %>" />
                            RadControls for WinForms
                        </li>
                        <li>
                            <img src="<%= Url.Content("~/Content/Common/Icons/Suites_32/WPF.png") %>" />
                            RadControls for WPF
                        </li>
                    </ul>
                <%});
            
            treeview.Add()
                .Text("Productivity")
                .Content(() =>
                {%>
                    <ul>
                        <li>
                            <img src="<%= Url.Content("~/Content/Common/Icons/Suites_32/JustCode.png") %>" />
                            JustCode
                        </li>
                    </ul>
                <%});
            
            treeview.Add()
                .Text("Data")
                .Content(() =>
                {%>
                    <ul>
                        <li>
                            <img src="<%= Url.Content("~/Content/Common/Icons/Suites_32/ORM.png") %>" />
                            OpenAccess ORM
                        </li>
                        <li>
                            <img src="<%= Url.Content("~/Content/Common/Icons/Suites_32/REP.png") %>" />
                            Reporting
                        </li>
                    </ul>
                <%});
            
            treeview.Add()
                .Text("TFS Tools")
                .Content(() =>
                {%>
                    <ul>
                        <li>
                            <img src="<%= Url.Content("~/Content/Common/Icons/Suites_32/TFS.png") %>" />
                            Work Item Manager
                        </li>
                        <li>
                            <img src="<%= Url.Content("~/Content/Common/Icons/Suites_32/PD.png") %>" />
                            Project Dashboard
                        </li>
                    </ul>
                <%});
            
            treeview.Add()
                .Text("Automated Testing")
                .Content(() =>
                {%>
                    <ul>
                        <li>
                            <img src="<%= Url.Content("~/Content/Common/Icons/Suites_32/Test.png") %>" />
                            Web Testing Tools
                        </li>
                    </ul>
                <%});
            
            treeview.Add()
                .Text("ASP.NET CMS")
                .Content(() =>
                {%>
                    <ul>
                        <li>
                            <img src="<%= Url.Content("~/Content/Common/Icons/Suites_32/CMS.png") %>" />
                            Sitefinity CMS
                        </li>
                    </ul>
                <%});

        }).Render();
%>

</asp:content>

<asp:content contentPlaceHolderId="HeadContent" runat="server">
    <style type="text/css">
        #TreeView .t-content li
        {
            line-height: 32px;
            font-size: 1.2em;
            padding: 3px 0;
            list-style-type: circle;
        }
        
        #TreeView .t-content img
        {
            vertical-align: middle;
        }
    </style>
</asp:content>
