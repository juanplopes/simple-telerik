namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Web.Mvc;

    public partial class DatePickerController : Controller
    {
        public ActionResult FirstLook(FirstLookModelView viewModel)
        {
            viewModel.SelectedDate = viewModel.SelectedDate ?? DateTime.Today;
            viewModel.MinDate = viewModel.MinDate ?? new DateTime(1900, 1, 1);
            viewModel.MaxDate = viewModel.MaxDate ?? new DateTime(2099, 12, 31);
            viewModel.ShowButton = viewModel.ShowButton ?? true;

            return View(viewModel);
        }
    }

    public class FirstLookModelView
    {
        public DateTime? SelectedDate { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
        public bool? ShowButton { get; set; }
    }
}