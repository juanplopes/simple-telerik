namespace Telerik.Web.Mvc.Examples.Models
{
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    
    using Telerik.Web.Mvc.Extensions;
    
    [ServiceContract(Namespace = "")]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CustomersWcf
    {
        [OperationContract]
        public GridModel Select(GridState state)
        {
            return SessionCustomerRepository.All().AsQueryable().ToGridModel(state);
        }

        [OperationContract]
        public GridModel Update(GridState state, EditableCustomer value)
        {
            SessionCustomerRepository.Update(value);

            return SessionCustomerRepository.All().AsQueryable().ToGridModel(state);
        }

        [OperationContract]
        public GridModel Insert(GridState state, EditableCustomer value)
        {
            SessionCustomerRepository.Insert(value);

            return SessionCustomerRepository.All().AsQueryable().ToGridModel(state);
        }

        [OperationContract]
        public GridModel Delete(GridState state, EditableCustomer value)
        {
            SessionCustomerRepository.Delete(value);

            return SessionCustomerRepository.All().AsQueryable().ToGridModel(state);
        }
    }
}
