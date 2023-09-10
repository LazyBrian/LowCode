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
    public class EntitiesController : Controller
    {
        private readonly LowCodeDbContext _context;

        public EntitiesController(LowCodeDbContext context)
        {
            _context = context;
        }

        // GET: Entities
        public async Task<IActionResult> Index()
        {
              return _context.Entities != null ? 
                          View(await _context.Entities.ToListAsync()) :
                          Problem("Entity set 'LowCodeDbContext.Entities'  is null.");
        }

        // GET: Entities/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Entities == null)
            {
                return NotFound();
            }

            var internalEntity = await _context.Entities
                .FirstOrDefaultAsync(m => m.EntityId == id);
            if (internalEntity == null)
            {
                return NotFound();
            }

            return View(internalEntity);
        }

        // GET: Entities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntityId,LogicalName,DisplayName,IsCustomEntity,EntityMask,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] InternalEntity internalEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(internalEntity);
                DatabaseHelper.CreateTable(internalEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(internalEntity);
        }

        // GET: Entities/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Entities == null)
            {
                return NotFound();
            }

            var internalEntity = await _context.Entities.FindAsync(id);
            if (internalEntity == null)
            {
                return NotFound();
            }
            return View(internalEntity);
        }

        // POST: Entities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("EntityId,LogicalName,DisplayName,IsCustomEntity,EntityMask,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] InternalEntity internalEntity)
        {
            if (id != internalEntity.EntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(internalEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternalEntityExists(internalEntity.EntityId))
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
            return View(internalEntity);
        }

        // GET: Entities/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Entities == null)
            {
                return NotFound();
            }

            var internalEntity = await _context.Entities
                .FirstOrDefaultAsync(m => m.EntityId == id);
            if (internalEntity == null)
            {
                return NotFound();
            }

            return View(internalEntity);
        }

        // POST: Entities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            if (_context.Entities == null)
            {
                return Problem("Entity set 'LowCodeDbContext.Entities'  is null.");
            }
            var internalEntity = await _context.Entities.FindAsync(id);
            if (internalEntity != null)
            {
                _context.Entities.Remove(internalEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InternalEntityExists(Guid? id)
        {
          return (_context.Entities?.Any(e => e.EntityId == id)).GetValueOrDefault();
        }
    }
}
