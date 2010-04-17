namespace Telerik.Web.Mvc.Examples.Models
{
    using System.ComponentModel;
    using System.Web.Script.Services;
    using System.Web.Services;
    using Telerik.Web.Mvc.Extensions;

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class OrdersAsmx : System.Web.Services.WebService
    {
        [WebMethod]
        public GridModel GetOrders(GridState state)
        {
            NorthwindDataContext northwind = new NorthwindDataContext();

            return northwind.Orders.ToGridModel(state);
        }
    }
}
