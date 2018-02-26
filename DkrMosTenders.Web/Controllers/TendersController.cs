using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DkrMosTenders.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using AutoMapper;
using DkrMosTenders.Model;

namespace DkrMosTenders.Web.Controllers
{
    public class TendersController : Controller
    {
        private const int DefaultPageSize = 25;

        private readonly TendersContext DbContext;
        private readonly IMapper Mapper;

        public TendersController(TendersContext context, IMapper mapper)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
            
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IActionResult> Index()
        {
            var tenders = await DbContext
                .Tenders.AsNoTracking()
                .Include(t => t.Objects)
                    .ThenInclude(obj => obj.Building)
                        .ThenInclude(b => b.District)
                .Select(t => Mapper.Map<Tender, TenderSummaryViewModel>(t))
                .ToArrayAsync();
            return View(tenders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AddOrEditTenderViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromBody]AddOrEditTenderViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var tender = Mapper.Map<AddOrEditTenderViewModel, Tender>(vm);
                DbContext.Add<Tender>(tender);
                DbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromQuery]int id)
        {
            var tender = await DbContext.Tenders.FindAsync(id);
            if (tender == null)
            {
                return NotFound();
            }

            var vm = new AddOrEditTenderViewModel
            {
                Tender = tender
            };

            return View(vm);
        }

        [HttpPost]
        public /*async*/ Task<IActionResult> Edit([FromBody] AddOrEditTenderViewModel vm)
        {
            throw new NotImplementedException();
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
