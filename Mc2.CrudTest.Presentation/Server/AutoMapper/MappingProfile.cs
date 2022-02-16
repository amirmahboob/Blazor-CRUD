using AutoMapper;
using Mc2.CrudTest.Presentation.Domain;
using Mc2.CrudTest.Shared;

namespace Mc2.CrudTest.Presentation.Server.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<CustomerViewModel, Customer>();
        }
    }
}