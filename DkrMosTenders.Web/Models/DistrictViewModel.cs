using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DkrMosTenders.Web.Models
{
    public class DistrictViewModel
    {
        public int? Id { get; set; }
        [Remote(action: "VerifyShortName", controller: "Districts")]
        [Required]
        [MinLength(3), MaxLength(10)]
        public string ShortName { get; set; }
        [Required]
        [MinLength(3)]
        public string FullName { get; set; }
    }
}
