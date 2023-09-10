using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LowCode.Models;

namespace LowCode.Controllers
{
    public class AttributeTypesController : Controller
    {
        private readonly LowCodeDbContext _context;

        public AttributeTypesController(LowCodeDbContext context)
        {
            _context = context;
        }

        // GET: AttributeTypes
        public async Task<IActionResult> Index()
        {
            return _context.AttributeTypes != null ?
                        View(await _context.AttributeTypes.ToListAsync()) :
                        Problem("Entity set 'LowCodeDbContext.AttributeTypes'  is null.");
        }

        // GET: AttributeTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.AttributeTypes == null)
            {
                return NotFound();
            }

            var internalAttributeType = await _context.AttributeTypes
                .FirstOrDefaultAsync(m => m.AttributeTypeId == id);
            if (internalAttributeType == null)
            {
                return NotFound();
            }

            return View(internalAttributeType);
        }

        // GET: AttributeTypes/Create
        public IActionResult Create()
        {
            var items = Enum.GetValues<SqlType>().Select(e => new { Name = e, Value = (int)e }).ToList();
            ViewData["SqlType"] = new SelectList(items, "Value", "Name");
            return View();
        }

        // POST: AttributeTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttributeTypeId,Name,SqlType,CustomType,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] InternalAttributeType internalAttributeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(internalAttributeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var items = Enum.GetValues<SqlType>().Select(e => new { Name = e, Value = (int)e }).ToList();
            ViewData["SqlType"] = new SelectList(items, "Value", "Name", (int)internalAttributeType.SqlType);
            return View(internalAttributeType);
        }

        // GET: AttributeTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.AttributeTypes == null)
            {
                return NotFound();
            }

            var internalAttributeType = await _context.AttributeTypes.FindAsync(id);
            if (internalAttributeType == null)
            {
                return NotFound();
            }
            var items = Enum.GetValues<SqlType>().Select(e => new { Name = e, Value = (int)e }).ToList();
            ViewData["SqlType"] = new SelectList(items, "Value", "Name", (int)internalAttributeType.SqlType);
            return View(internalAttributeType);
        }

        // POST: AttributeTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("AttributeTypeId,Name,SqlType,CustomType,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] InternalAttributeType internalAttributeType)
        {
            if (id != internalAttributeType.AttributeTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(internalAttributeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternalAttributeTypeExists(internalAttributeType.AttributeTypeId))
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
            var items = Enum.GetValues<SqlType>().Select(e => new { Name = e, Value = (int)e }).ToList();
            ViewData["SqlType"] = new SelectList(items, "Value", "Name", (int)internalAttributeType.SqlType);
            return View(internalAttributeType);
        }

        // GET: AttributeTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.AttributeTypes == null)
            {
                return NotFound();
            }

            var internalAttributeType = await _context.AttributeTypes
                .FirstOrDefaultAsync(m => m.AttributeTypeId == id);
            if (internalAttributeType == null)
            {
                return NotFound();
            }

            return View(internalAttributeType);
        }

        // POST: AttributeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            if (_context.AttributeTypes == null)
            {
                return Problem("Entity set 'LowCodeDbContext.AttributeTypes'  is null.");
            }
            var internalAttributeType = await _context.AttributeTypes.FindAsync(id);
            if (internalAttributeType != null)
            {
                _context.AttributeTypes.Remove(internalAttributeType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InternalAttributeTypeExists(Guid? id)
        {
            return (_context.AttributeTypes?.Any(e => e.AttributeTypeId == id)).GetValueOrDefault();
        }
    }
}
