using Mc2.CrudTest.Presentation.Data;
using Mc2.CrudTest.Presentation.Domain;
using Mc2.CrudTest.Presentation.Infrastructure;
using Mc2.CrudTest.Presentation.Service.Interface;

namespace Mc2.CrudTest.Presentation.Service.Services
{
    public class CustomerService : GenericService<Customer>, ICustomerService
    {
        public CustomerService(IUnitOfWork uow)
           : base(uow)
        {
        }

        public override void Update(Customer entity)
        {
            if (Utility.IsPhoneNumber(entity.PhoneNumber))
            {
                base.Update(entity);
            }
            else
            {
                throw new Exception("The phone number is not valid");
            }
        }
        public override Customer Add(Customer entity)
        {
            if (Utility.IsPhoneNumber(entity.PhoneNumber))
            {
                return base.Add(entity);
            }
            else
            {
                throw new Exception("The phone number is not valid");
            }
        }
    }
}