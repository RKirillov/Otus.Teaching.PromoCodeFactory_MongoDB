using System;
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
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController
        : ControllerBase
    {
        private readonly IPromoCodeRepository _promoCodeRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public PromocodesController(IPromoCodeRepository promoCodeRepository, IMapper mapper, ICustomerRepository customerRepository)
        {
            _promoCodeRepository = promoCodeRepository;
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Получить все промокоды
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Все промокоды</returns>
        [HttpGet]
        public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromocodesAsync(CancellationToken cancellationToken)
        {
            //TODO: Получить все промокоды 
            var preferences = await _promoCodeRepository.GetAllAsync(cancellationToken);

            var response = preferences.Select(x => new PromoCodeShortResponse()
            {
                Id = x.Id,
                Code = x.Code1,
                BeginDate = x.BeginDate.ToString(),
                EndDate = x.EndDate.ToString(),
                PartnerName = x.PartnerName,
                ServiceInfo = x.ServiceInfo
            }).ToList();

            return Ok(response);
        }

        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        /// <param name="request">Запрос на создание промокода</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Возвращается промокод</returns>
        [HttpPost]
        public async Task<ActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request, CancellationToken cancellationToken)
        {
            //TODO: Создать промокод и выдать его клиентам с указанным предпочтением
            //TODO перенести в сервис
            var newPromoCode = _mapper.Map<PromoCode>(request);

            /*            var customers = (await _customerRepository.GetAllAsync(cancellationToken, false))
                            .Where(x => x.Preferences
                            .Select(x => x.Preference.Name)
                            .Contains(request.PreferenceName));*/
            var customers = await _customerRepository.GetByPreferences(cancellationToken, request.PreferenceName);
            if (customers.Any())
            {
                newPromoCode.PreferenceId = customers.Select(x => x.Preferences).FirstOrDefault().Select(x => x.PreferenceId).FirstOrDefault();
            }
            foreach (var customer in customers)
            {
                newPromoCode.CustomerId = customer.Id;
                customer.PromoCodes.Add(newPromoCode);
            }
            await _promoCodeRepository.AddAsync(newPromoCode, cancellationToken);
            //_promoCodeRepository.SaveChanges();

            _customerRepository.SaveChanges();

            return Ok(_mapper.Map<PromoCodeShortResponse>(newPromoCode));
        }
    }
}