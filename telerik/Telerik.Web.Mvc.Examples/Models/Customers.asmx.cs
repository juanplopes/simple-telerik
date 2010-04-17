namespace Telerik.Web.Mvc.Examples.Models
{
    using System.ComponentModel;
    using System.Linq;
    using System.Web.Script.Services;
    using System.Web.Services;
    using Telerik.Web.Mvc.Extensions;

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class CustomersAsmx : WebService
    {
        [WebMethod(EnableSession = true)] //Session required by SessionCustomerRepository
        public GridModel Select(GridState state)
        {
            return SessionCustomerRepository.All().AsQueryable().ToGridModel(state);
        }

        [WebMethod(EnableSession = true)] //Session required by SessionCustomerRepository
        public GridModel Update(EditableCustomer value, GridState state)
        {
            SessionCustomerRepository.Update(value);

            return SessionCustomerRepository.All().AsQueryable().ToGridModel(state);
        }

        [WebMethod(EnableSession = true)] //Session required by SessionCustomerRepository
        public GridModel Insert(EditableCustomer value, GridState state)
        {
            SessionCustomerRepository.Insert(value);

            return SessionCustomerRepository.All().AsQueryable().ToGridModel(state);
        }
        
        [WebMethod(EnableSession = true)] //Session required by SessionCustomerRepository
        public GridModel Delete(EditableCustomer value, GridState state)
        {
            SessionCustomerRepository.Delete(value);

            return SessionCustomerRepository.All().AsQueryable().ToGridModel(state);
        }
    }
}
