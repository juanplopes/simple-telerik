<%@ Page Language="C#" AutoEventWireup="true" %>

<script runat="server">

    protected override void OnLoad(EventArgs e)
    {
        string originalPath = Request.Path;
        HttpContext.Current.RewritePath(Request.ApplicationPath, false);

        IHttpHandler httpHandler = new MvcHttpHandler();
        httpHandler.ProcessRequest(HttpContext.Current);
        HttpContext.Current.RewritePath(originalPath, false);
    }

</script>
<%-- Please do not delete this file. It is used to ensure that ASP.NET MVC is activated by IIS when a user makes a "/" request to the server. --%>