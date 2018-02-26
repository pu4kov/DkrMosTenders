using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DkrMosTenders.Model;
using DkrMosTenders.Web.Models;

namespace DkrMosTenders.Web.Controllers
{
    public class ObjectsController : Controller
    {
        private readonly TendersContext _context;

        public ObjectsController(TendersContext context)
        {
            _context = context;
        }

        // GET: Objects
        public async Task<IActionResult> Index()
        {
            var tendersContext = _context.TendersObjects.Include(t => t.Building).Include(t => t.Tender);
            return View(await tendersContext.ToListAsync());
        }

        // GET: Objects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenderObject = await _context.TendersObjects
                .Include(t => t.Building)
                .Include(t => t.Tender)
                .SingleOrDefaultAsync(m => m.TenderId == id);
            if (tenderObject == null)
            {
                return NotFound();
            }

            return View(tenderObject);
        }

        // GET: Objects/Create
        public IActionResult Create()
        {
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "Id", "Address");
            ViewData["TenderId"] = new SelectList(_context.Tenders, "Id", "DkrNumber");
            return View();
        }

        // POST: Objects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenderId,BuildingId")] TenderObject tenderObject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tenderObject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "Id", "Address", tenderObject.BuildingId);
            ViewData["TenderId"] = new SelectList(_context.Tenders, "Id", "DkrNumber", tenderObject.TenderId);
            return View(tenderObject);
        }

        // GET: Objects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenderObject = await _context.TendersObjects.SingleOrDefaultAsync(m => m.TenderId == id);
            if (tenderObject == null)
            {
                return NotFound();
            }
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "Id", "Address", tenderObject.BuildingId);
            ViewData["TenderId"] = new SelectList(_context.Tenders, "Id", "DkrNumber", tenderObject.TenderId);
            return View(tenderObject);
        }

        // POST: Objects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TenderId,BuildingId")] TenderObject tenderObject)
        {
            if (id != tenderObject.TenderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenderObject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenderObjectExists(tenderObject.TenderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "Id", "Address", tenderObject.BuildingId);
            ViewData["TenderId"] = new SelectList(_context.Tenders, "Id", "DkrNumber", tenderObject.TenderId);
            return View(tenderObject);
        }

        // GET: Objects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenderObject = await _context.TendersObjects
                .Include(t => t.Building)
                .Include(t => t.Tender)
                .SingleOrDefaultAsync(m => m.TenderId == id);
            if (tenderObject == null)
            {
                return NotFound();
            }

            return View(tenderObject);
        }

        // POST: Objects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenderObject = await _context.TendersObjects.SingleOrDefaultAsync(m => m.TenderId == id);
            _context.TendersObjects.Remove(tenderObject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenderObjectExists(int id)
        {
            return _context.TendersObjects.Any(e => e.TenderId == id);
        }
    }
}
