namespace Telerik.Web.Mvc.Examples.Models
{
    using System.Linq;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using Telerik.Web.Mvc.Extensions;

    [ServiceContract(Namespace = "")]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class OrdersWcf
    {
        [OperationContract]
        public GridModel GetOrders(GridState state)
        {
            NorthwindDataContext northwind = new NorthwindDataContext();
            IQueryable<OrderDto> orders = from o in northwind.Orders
                                          select new OrderDto
                                          {
                                              OrderID = o.OrderID,
                                              ContactName = o.Customer.ContactName,
                                              ShipAddress = o.ShipAddress,
                                              OrderDate = o.OrderDate
                                          };

            return orders.ToGridModel(state);
        }
    }
}
