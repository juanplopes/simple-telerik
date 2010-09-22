namespace Telerik.Web.Mvc.UI
{
    public interface IDropDownBindingSettings
    {
        bool Enabled
        {
            get;
            set;
        }

        DropDownRequestSettings Select
        {
            get;
        }
    }
}
