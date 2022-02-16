using AutoMapper;
using Mc2.CrudTest.Presentation.Data;
using Mc2.CrudTest.Presentation.Domain;
using Mc2.CrudTest.Presentation.Service.Interface;
using Mc2.CrudTest.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [AllowAnonymous]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(
            IUnitOfWork unitOfWork,
            ICustomerService customerService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerViewModel>> Get()
        {
            var customers = await _customerService.GetAll();
            var result = customers.Select(r => new CustomerViewModel
            {
                Id = r.Id,
                FirstName = r.FirstName,
                LastName = r.LastName,
                DateOfBirth = r.DateOfBirth,
                PhoneNumber = r.PhoneNumber,
                Email = r.Email,
                BankAccountNumber = r.BankAccountNumber
            });
            return result;
        }

        [HttpGet]
        [Route("GetById")]
        public CustomerViewModel GetById(int id)
        {
            var customer = _customerService.Find(r => r.Id.Equals(id));
            var model = _mapper.Map<CustomerViewModel>(customer);
            return model;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] CustomerViewModel model)
        {
            try
            {
                var customer = _mapper.Map<Customer>(model);
                _customerService.Add(customer);
                await _unitOfWork.SaveChangesAsync(true);
                return Ok();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] CustomerViewModel model)
        {
            try
            {
                var customer = _mapper.Map<Customer>(model);
                _customerService.Update(customer);
                await _unitOfWork.SaveChangesAsync(true);
                return Ok();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] CustomerViewModel model)
        {
            try
            {
                var customer = _customerService.Find(r => r.Id == model.Id);
                _customerService.Delete(customer);
                await _unitOfWork.SaveChangesAsync(true);
                return Ok();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        [Route("DeleteById")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                var customer = _customerService.Find(r => r.Id.Equals(id));
                _customerService.Delete(customer);
                await _unitOfWork.SaveChangesAsync(true);
                return Ok();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
