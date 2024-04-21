using System.ComponentModel.DataAnnotations;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    public class GivePromoCodeRequest
    {
        [Required]
        [StringLength(120)]
        public string ServiceInfo { get; set; }
        [Required]
        [StringLength(120)]
        public string PartnerName { get; set; }
        [Required]
        [StringLength(120)]
        public string PromoCode { get; set; }
        [Required]
        [StringLength(120)]
        public string PreferenceName { get; set; }
    }
}