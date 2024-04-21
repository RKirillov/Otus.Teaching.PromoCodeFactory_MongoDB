
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.DataAccess.Repositories;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Предпочтения клиентов
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PreferencesController
        : ControllerBase
    {
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly IMapper _mapper;
        public PreferencesController(IPreferenceRepository preferenceRepository, IMapper mapper)
        {
            _preferenceRepository = preferenceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все предпочтения клиентов
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Коллекция предпочтений клиента</returns>
        /// <response code="200">Получение всех предпочтений успешно</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult> GetPreferencesAsync(CancellationToken cancellationToken)
        {
            var preferences = await _preferenceRepository.GetAllAsync(cancellationToken,true);
            var preferencesShortsList = _mapper.Map<List<PreferenceResponse>>(preferences);
/*            var response = preferences.Select(x => new PreferenceResponse()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();*/

            return Ok(preferencesShortsList);
        }
    }
}