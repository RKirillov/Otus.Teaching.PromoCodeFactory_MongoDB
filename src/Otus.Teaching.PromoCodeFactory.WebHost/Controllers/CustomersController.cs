using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.DataAccess.Repositories;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Microsoft.AspNetCore.Http;

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

        private readonly IMapper _mapper;
        public CustomersController(ICustomerRepository customerRepository,
            IMapper mapper,
            IPreferenceRepository preferenceRepository
            )
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _preferenceRepository = preferenceRepository;
        }

        /// <summary>
        /// Получить всех клиентов
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Коллекция всех клиентов</returns>
        /// <response code="200">Получение всех клиентов успешно</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult> GetCustomersAsync(CancellationToken cancellationToken)
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

            return Ok(employeesModelList);
        }
        /// <summary>
        /// Получить клиента по id
        /// </summary>
        /// <param name="id">Id клиента</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Клиент</returns>
        /// <response code="200">Получение клиентов успешно</response>
        /// <response code="404">Такого клиента не существует</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var customer = (await _customerRepository.GetAsync(id, cancellationToken));

            if (customer == null)
                return NotFound();

            var customerModel = _mapper.Map<CustomerResponse>(customer);

            return Ok(customerModel);
        }


        /// <summary>
        /// Создать клиента
        /// </summary>
        /// <param name="request">Сущность задания нового клиента</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Клиент</returns>
        /// <response code="201">Создание клиента успешно</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
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

        /// <summary>
        /// Изменить существующего клиента по id
        /// </summary>
        /// <param name="id">Id клиента</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <param name="request">Сущность запросма на изменение клиента</param>
        /// <returns></returns>
        /// <response code="204">Изменение клиента прошло успешно</response>
        /// <response code="404">Такого клиента не существует</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
            customer.Preferences.Clear();
            customer.Preferences = preferences.Select(x => new CustomerPreference()
            {
                Customer = customer,
                Preference = x
            }).ToList();

            _customerRepository.Update(customer);
            //_customerRepository.SaveChanges();

            await _customerRepository.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Удалить клиента по id
        /// </summary>
        /// <param name="id">Id клиента</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        /// <response code="204">Удаление клиента прошло успешно</response>
        /// <response code="404">Такого клиента не существует</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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