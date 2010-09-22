namespace Telerik.Web.Mvc.UI
{
    public class DropDownBindingSettings : IDropDownBindingSettings
    {
        public DropDownBindingSettings()
        {
            Select = new DropDownRequestSettings();
        }

        public bool Enabled
        {
            get;
            set;
        }

        public DropDownRequestSettings Select
        {
            get;
            private set;
        }
    }
}
