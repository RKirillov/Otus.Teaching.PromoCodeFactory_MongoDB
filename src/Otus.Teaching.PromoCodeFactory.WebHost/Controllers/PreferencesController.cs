
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
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
        
        [HttpGet]
        public async Task<ActionResult<List<PreferenceResponse>>> GetPreferencesAsync(CancellationToken cancellationToken)
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