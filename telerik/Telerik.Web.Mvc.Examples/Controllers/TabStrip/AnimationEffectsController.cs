namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class TabStripController : Controller
    {
        public ActionResult AnimationEffects(
              string animation,
              bool? enableOpacityAnimation,
              int? openDuration,
              int? closeDuration)
        {
            ViewData["animation"] = animation ?? "expand";
            ViewData["enableOpacityAnimation"] = enableOpacityAnimation ?? true;
            ViewData["openDuration"] = openDuration ?? 200;
            ViewData["closeDuration"] = closeDuration ?? 200;

            return View();
        }
    }
}