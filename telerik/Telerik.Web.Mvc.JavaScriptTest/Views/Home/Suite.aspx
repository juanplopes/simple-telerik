<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Suite</title>

	<script type="text/javascript" src='<%= Url.Content("~/Scripts/jsUnit/app/jsUnitCore.js") %>'></script>
	<script type="text/javascript">
        function suite()
        {
            var allTests = new top.jsUnitTestSuite();
            var suite = new top.jsUnitTestSuite();
            
            suite.addTestPage("Core/DateFormatting");
            suite.addTestPage("Calendar/Navigation");
            suite.addTestPage("DatePicker/DateParsing");
            suite.addTestPage("DatePicker/DatePicker");
            suite.addTestPage("DatePicker/ParseByToken");
            suite.addTestPage("Grid/Localization");
            suite.addTestPage("Grid/Grouping");
            suite.addTestPage("Grid/ClientTemplates");
            suite.addTestPage("Grid/Editing");
            suite.addTestPage("Grid/Selection");
            suite.addTestPage("Grid/Filtering");
            suite.addTestPage("Grid/Binding");
            suite.addTestPage("Grid/Sorting");
            suite.addTestPage("Grid/Paging");
            suite.addTestPage("Menu/OpenOnClick");
            suite.addTestPage("PanelBar/ExpandCollapse");
            suite.addTestPage("TreeView/ClientEvents");
            suite.addTestPage("TreeView/ExpandCollapse");
            
            allTests.addTestSuite(suite);
            return allTests;
        }
    </script>
</head>
<body>
</body>
</html>
