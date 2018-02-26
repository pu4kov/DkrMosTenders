using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DkrMosTenders.Web.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DkrMosTenders.Model;

namespace DkrMosTenders.Web.Controllers
{
    public class DistrictsController : Controller
    {
        private readonly TendersContext DbContext;
        private readonly IMapper Mapper;

        public DistrictsController(TendersContext context, IMapper mapper)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
            Mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var districts = await DbContext.Districts.AsNoTracking().ToArrayAsync();
            return View(districts);
        }

        public IActionResult VerifyShortName(string shortName)
        {
            var district = DbContext.Districts.Where(d => d.ShortName.ToUpper() == shortName.ToUpper()).FirstOrDefault();
            if (district != null)
            {
                return Json($"District \"{shortName}\" already exists");
            }

            return Json(true);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShortName", "FullName")]DistrictViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var newDistrict = Mapper.Map<DistrictViewModel, District>(vm);
                DbContext.Add(newDistrict);
                await DbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }
    }
}