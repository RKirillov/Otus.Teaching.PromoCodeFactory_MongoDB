using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using AutoMapper;
using Otus.Teaching.PromoCodeFactory.DataAccess.MongoDB;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Роли сотрудников
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IMongoRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RolesController(ILogger<RolesController> logger,
            IMongoRoleRepository roleRepository, IMapper mapper)
        {
            _logger = logger;
            _roleRepository = roleRepository;
            _mapper= mapper;
        }

        /// <summary>
        /// Получить все доступные роли сотрудников
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Все роли</returns>
        /// <response code="200">Получение всех ролей успешно</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult> GetRolesAsync(CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAsync();

            var rolesModelList = roles.Select(x => 
                new RoleItemResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList();
            return Ok(rolesModelList);
        }

        /// <summary>
        /// Добавить роль сотрудника
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult> PostRoleAsync(CreateRoleItemRequest role)
        {
            var _role = _mapper.Map<Role>(role);
            await _roleRepository.InsertOneAsync(_role);

            return CreatedAtAction(nameof(PostRoleAsync), null, _mapper.Map<RoleItemResponse>(_role));
        }
    }
}