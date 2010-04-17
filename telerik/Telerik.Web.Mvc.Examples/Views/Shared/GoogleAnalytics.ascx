<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% if (Request.Url.Host.Equals("demos.telerik.com", StringComparison.OrdinalIgnoreCase)) {  %>
<script type="text/javascript">
var _gaq=_gaq||[];_gaq.push(['_setAccount','UA-111455-1'],['_setDomainName', '.telerik.com'],['_trackPageview']);(function(){var s=document.createElement('script');s.type='text/javascript';s.async=true;s.src=('https:'==document.location.protocol?'https://ssl':'http://www')+'.google-analytics.com/ga.js';(document.getElementsByTagName('head')[0]||document.getElementsByTagName('body')[0]).appendChild(s);})();
</script>

<% } %>