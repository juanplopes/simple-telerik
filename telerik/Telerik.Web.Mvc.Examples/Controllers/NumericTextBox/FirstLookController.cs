namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class NumericTextBoxController : Controller
    {
        public ActionResult FirstLook(TextBoxFirstLookModelView model)
        {
            model.NumericShowSpinners = model.NumericShowSpinners ?? true;
            model.NumericMinValue = model.NumericMinValue ?? -127;
            model.NumericMaxValue = model.NumericMaxValue ?? 128;

            model.CurrencyShowSpinners = model.CurrencyShowSpinners ?? true;
            model.CurrencyMinValue = model.CurrencyMinValue ?? 0;
            model.CurrencyMaxValue = model.CurrencyMaxValue ?? 1000;

            model.PercentShowSpinners = model.PercentShowSpinners ?? true;
            model.PercentMinValue = model.PercentMinValue ?? 0;
            model.PercentMaxValue = model.PercentMaxValue ?? 100;

            model.IntegerShowSpinners = model.IntegerShowSpinners ?? true;
            model.IntegerMinValue = model.IntegerMinValue ?? 0;
            model.IntegerMaxValue = model.IntegerMaxValue ?? 100;

            return View(model);
        }
    }

    public class TextBoxFirstLookModelView 
    {
        public double? NumericMinValue { get; set; }
        public double? NumericMaxValue { get; set; }
        public bool? NumericShowSpinners { get; set; }

        public decimal? CurrencyMinValue { get; set; }
        public decimal? CurrencyMaxValue { get; set; }
        public bool? CurrencyShowSpinners { get; set; }

        public double? PercentMinValue { get; set; }
        public double? PercentMaxValue { get; set; }
        public bool? PercentShowSpinners { get; set; }

        public int? IntegerMinValue { get; set; }
        public int? IntegerMaxValue { get; set; }
        public bool? IntegerShowSpinners { get; set; }
    }
}