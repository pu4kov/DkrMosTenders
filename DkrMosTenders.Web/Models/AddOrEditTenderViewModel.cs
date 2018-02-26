using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DkrMosTenders.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DkrMosTenders.Web.Models
{
    public class AddOrEditTenderViewModel
    {
        [BindRequired]
        public Tender Tender { get; set; }
    }
}
