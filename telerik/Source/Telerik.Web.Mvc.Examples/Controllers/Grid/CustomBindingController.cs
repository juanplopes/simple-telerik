namespace Telerik.Web.Mvc.Examples
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Linq;
    using System.Linq;
    using System.Web.Mvc;

    using Models;

    public partial class GridController : Controller
    {
        public ActionResult CustomBinding()
        {
            IEnumerable<Order> data = GetData(new GridCommand());

            // Required for pager configuration
            ViewData["total"] = GetCount();

            return View(data);
        }

        [GridAction(EnableCustomBinding = true)]
        public ActionResult _CustomBinding(GridCommand command)
        {
            IEnumerable<Order> data = GetData(command);

            return View(new GridModel
                            {
                                Data = data,
                                Total = data.Count()
                            });
        }

        private static IEnumerable<Order> GetData(GridCommand command)
        {
            DataLoadOptions loadOptions = new DataLoadOptions();
            loadOptions.LoadWith<Order>(o => o.Customer);

            var dataContext = new NorthwindDataContext
            {
                LoadOptions = loadOptions
            };

            IQueryable<Order> data = dataContext.Orders;

            //Apply filtering
            if (command.FilterDescriptors.Any())
            {
                data = data.Where(ExpressionBuilder.Expression<Order>(command.FilterDescriptors));
            }

            // Apply sorting
            foreach (SortDescriptor sortDescriptor in command.SortDescriptors)
            {
                if (sortDescriptor.SortDirection == ListSortDirection.Ascending)
                {
                    switch (sortDescriptor.Member)
                    {
                        case "OrderID":
                            data = data.OrderBy(ExpressionBuilder.Expression<Order, int>(sortDescriptor.Member));
                            break;
                        case "Customer.ContactName":
                            data = data.OrderBy(order => order.Customer.ContactName);
                            break;
                        case "ShipAddress":
                            data = data.OrderBy(order => order.ShipAddress);
                            break;
                        case "OrderDate":
                            data = data.OrderBy(order => order.OrderDate);
                            break;
                    }
                }
                else
                {
                    switch (sortDescriptor.Member)
                    {
                        case "OrderID":
                            data = data.OrderByDescending(order => order.OrderID);
                            break;
                        case "Customer.ContactName":
                            data = data.OrderByDescending(order => order.Customer.ContactName);
                            break;
                        case "ShipAddress":
                            data = data.OrderByDescending(order => order.ShipAddress);
                            break;
                        case "OrderDate":
                            data = data.OrderByDescending(order => order.OrderDate);
                            break;
                    }
                }
            }
            

            // ... and paging
            if (command.PageSize > 0)
            {
                data = data.Skip((command.Page - 1) * command.PageSize);
            }

            data = data.Take(command.PageSize);

            return data;
        }

        private static int GetCount()
        {
            using (NorthwindDataContext dataContext = new NorthwindDataContext())
            {
                return dataContext.Orders.Count();
            }
        }
    }
}