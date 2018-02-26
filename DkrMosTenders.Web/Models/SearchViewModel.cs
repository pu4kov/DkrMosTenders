using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DkrMosTenders.Web.Models
{
    public class SearchViewModel
    {
        public string SearchQuery { get; set; }

        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }

        public DateTime? ApplicationDateFrom { get; set; }
        public DateTime? ApplicationDateTo { get; set; }
    }
}
