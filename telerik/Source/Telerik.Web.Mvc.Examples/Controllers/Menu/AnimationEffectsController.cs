namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class MenuController : Controller
    {
        public ActionResult AnimationEffects(
             string animation,
             bool? enableOpacityAnimation,
             int? openDuration,
             int? closeDuration)
        {
            ViewData["animation"] = animation ?? "slide";
            ViewData["enableOpacityAnimation"] = enableOpacityAnimation ?? true;
            ViewData["openDuration"] = openDuration ?? 200;
            ViewData["closeDuration"] = closeDuration ?? 200;

            return View();
        }
    }
}