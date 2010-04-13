
namespace Telerik.Web.Mvc.Examples
{
    using System.Globalization;
    using System.Threading;
    using System.Web.Mvc;
    
    public class CultureAwareActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!string.IsNullOrEmpty(filterContext.HttpContext.Request["culture"]))
            {
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(filterContext.HttpContext.Request["culture"]);
            }
        }
    }
}
