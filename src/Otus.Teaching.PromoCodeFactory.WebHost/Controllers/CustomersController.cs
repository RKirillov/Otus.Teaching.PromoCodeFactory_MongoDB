using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.DataAccess.Repositories;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController
        : ControllerBase
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IPreferenceRepository _preferenceRepository;
        //private readonly IPromoCodeRepository _promoCodeRepository;

        private readonly IMapper _mapper;
        public CustomersController(ICustomerRepository customerRepository,
            IMapper mapper,
            IPreferenceRepository preferenceRepository
            //IPromoCodeRepository promoCodeRepository
            )
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _preferenceRepository = preferenceRepository;
            //_promoCodeRepository = promoCodeRepository;
        }

        [HttpGet]
        public async Task<List<CustomerShortResponse>> GetCustomersAsync(CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync(cancellationToken);

            var employeesModelList = customers.Select(x =>
                new CustomerShortResponse()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                }).ToList();

            return employeesModelList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var customer = (await _customerRepository.GetAsync(id, cancellationToken));

            if (customer == null)
                return NotFound();

            //var promoCodesShortsList = _mapper.Map<List<PromoCodeShortResponse>>(customer.PromoCodes);
            /*            var preferencesShortsList = _mapper.Map<List<PreferenceResponse>>(customer.Preferences);
                        var customerModel = new CustomerResponse()
                        {
                            Id = customer.Id,
                            Email = customer.Email,
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            PromoCodesResponse = promoCodesShortsList,
                            PreferencesResponse = preferencesShortsList
                        };*/
            var customerModel = _mapper.Map<CustomerResponse>(customer);

            return customerModel;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(CreateOrEditCustomerRequest request, CancellationToken cancellationToken)
        {
            //TODO: Добавить создание нового клиента вместе с его предпочтениями
            var preferences = await _preferenceRepository.GetRangeAsync(request.PreferenceIds, cancellationToken);
            //var promocodes = await _preferenceRepository.GetRangeAsync(request.PreferenceIds, cancellationToken);
            var customer = new Customer()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
            customer.Preferences = preferences.Select(x => new CustomerPreference()
            {
                Customer = customer,
                Preference = x
            }).ToList();
            //TODO подключать ли промокоды?
            await _customerRepository.AddAsync(customer, cancellationToken);
            await _customerRepository.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(GetCustomerByIdAsync), new { id = customer.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomersAsync(Guid id, CreateOrEditCustomerRequest request, CancellationToken cancellationToken)
        {
            //TODO: Обновить данные клиента вместе с его предпочтениями
            var customer = await _customerRepository.GetAsync(id, cancellationToken);

            if (customer == null)
                return NotFound();

            var preferences = await _preferenceRepository.GetRangeAsync(request.PreferenceIds, cancellationToken);
            //TODO использовать AutoMapper c настройкой

            customer.Email = request.Email;
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            //customer.Preferences.Clear();
            customer.Preferences = preferences.Select(x => new CustomerPreference()
            {
                Customer = customer,
                Preference = x
            }).ToList();

            await _customerRepository.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(Guid id, CancellationToken cancellationToken)
        {
            //TODO: Удаление клиента вместе с выданными ему промокодами
            //By convention, required relationships are configured to cascade delete; this means that when the principal
            //is deleted, all of its dependents are deleted as well,
            if (await _customerRepository.DeleteByIdAsync(id, cancellationToken) == 0)
                return NotFound();
            await _customerRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}