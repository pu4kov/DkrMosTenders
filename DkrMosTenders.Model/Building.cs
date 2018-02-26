using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DkrMosTenders.Model
{
    public class Building
    {
        public int Id { get; set; }

        [Required]
        public virtual District District { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Address { get; set; }
    }
}
