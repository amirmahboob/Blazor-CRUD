using Mc2.CrudTest.Presentation.Data;
using Mc2.CrudTest.Presentation.Domain;
using Mc2.CrudTest.Presentation.Service.Interface;

namespace Mc2.CrudTest.Presentation.Service.Services
{
    public class CustomerService : GenericService<Customer>, ICustomerService
    {
        public CustomerService(IUnitOfWork uow)
           : base(uow)
        {
        }
    }
}