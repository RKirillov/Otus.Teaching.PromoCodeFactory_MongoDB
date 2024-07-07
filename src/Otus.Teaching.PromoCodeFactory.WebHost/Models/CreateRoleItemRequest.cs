using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    public class CreateRoleItemRequest
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        [Required]
        [StringLength(120)]
        public string Description { get; set; }
    }
}