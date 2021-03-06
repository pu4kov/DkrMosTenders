﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DkrMosTenders.Model
{
    public class District
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FullName { get; set; }
        [Required(AllowEmptyStrings = false), MaxLength(10)]
        public string ShortName { get; set; }
    }
}
