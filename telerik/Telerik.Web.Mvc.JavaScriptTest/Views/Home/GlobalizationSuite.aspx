<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Suite</title>

    <script type="text/javascript" src='<%= Url.Content("~/Scripts/jsUnit/app/jsUnitCore.js") %>'></script>

    <script type="text/javascript">
        var cultures = [];
    </script>

    <%
        Response.Write("<script type='text/javascript'>");

        foreach (System.Globalization.CultureInfo ci in System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures))
        {
            if (!ci.IsNeutralCulture)
            {
                Response.Write("cultures.push(\"" + ci.Name + "\");");
            }
        }

        Response.Write("</script>");
    %>

    <script type="text/javascript">
        function suite() {
            var allTests = new top.jsUnitTestSuite();
            var suite = new top.jsUnitTestSuite();

            for (var i = 0; i < cultures.length; i++) {
                 suite.addTestPage("Core/DateFormatting?culture=" + cultures[i]);
            }

            allTests.addTestSuite(suite);
            return allTests;
        }
    </script>

</head>
<body>
</body>
</html>
