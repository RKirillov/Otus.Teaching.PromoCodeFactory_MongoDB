using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Otus.Teaching.PromoCodeFactory.DataAccess.MongoDB;
using Otus.Teaching.PromoCodeFactory.DataAccess.Repositories;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController
        : ControllerBase
    {
        private readonly IMongoEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeesController(IMongoEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Коллекция всех сотрудников</returns>
        /// <response code="200">Получение всех сотрудников успешно</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult> GetEmployeesAsync(CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.Collection.Find(_ => true).ToListAsync();

            var employeesModelList = employees.Select(x => 
                new EmployeeShortResponse()
                    {
                        Id = x.Id,
                        Email = x.Email,
                        FullName = x.FullName,
                    }).ToList();

            return Ok(employeesModelList);
        }


        /// <summary>
        /// Получение сотрудника по id
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Сотрудник</returns>
        /// <response code="200">Получение сотрудника успешно</response>
        /// <response code="404">Такого сотрудника не существует</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (employee == null)
                return NotFound();

            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Role = new RoleItemResponse()
                {
                    Id = employee.Id,
                    Name = employee.Role.Name,
                    Description = employee.Role.Description
                },
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return Ok(employeeModel);
        }
    }
}