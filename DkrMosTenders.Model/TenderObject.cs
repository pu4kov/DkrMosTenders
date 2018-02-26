using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DkrMosTenders.Model
{
    public class TenderObject
    {
        [Required]
        public int TenderId { get; set; }
        public Tender Tender { get; set; }

        [Required]
        public int BuildingId { get; set; }
        public Building Building { get; set; }
    }
}
