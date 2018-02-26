using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DkrMosTenders.Web.Models
{
    public class TenderSummaryViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string FullName { get; set; }
        public double Price { get; set; }
        public ICollection<string> Districts { get; set; }
        public ICollection<string> Addresses { get; set; }
    }
}
