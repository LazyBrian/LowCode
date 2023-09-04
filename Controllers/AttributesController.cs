using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LowCode.Models;
using Attribute = LowCode.Models.Attribute;

namespace LowCode.Controllers
{
    public class AttributesController : Controller
    {
        private readonly LowCodeDbContext _context;

        public AttributesController(LowCodeDbContext context)
        {
            _context = context;
        }

        // GET: Attributes
        public async Task<IActionResult> Index()
        {
            var lowCodeDbContext = _context.Attributes.Include(a => a.Entity);
            return View(await lowCodeDbContext.ToListAsync());
        }

        // GET: Attributes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Attributes == null)
            {
                return NotFound();
            }

            var attribute = await _context.Attributes
                .Include(a => a.Entity)
                .FirstOrDefaultAsync(m => m.AttributeId == id);
            if (attribute == null)
            {
                return NotFound();
            }

            return View(attribute);
        }

        // GET: Attributes/Create
        public IActionResult Create()
        {
            ViewData["EntityId"] = new SelectList(_context.Entities, "EntityId", "DisplayName");
            return View();
        }

        // POST: Attributes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttributeId,LogicalName,DisplayName,Description,EntityId")] Attribute attribute)
        {
            
            //if (ModelState.IsValid)
            //{
                Entity entity = _context.Entities.Find(attribute.EntityId);
                
                if (!_context.Attributes.Any(c => c.LogicalName == entity.LogicalName && c.LogicalName == attribute.LogicalName))
                {
                    _context.Add(attribute);
                    DatabaseHelper.AddAttribute(entity.LogicalName, attribute);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            //}
            ViewData["EntityId"] = new SelectList(_context.Entities, "EntityId", "DisplayName", attribute.EntityId);
            return View(attribute);
        }

        // GET: Attributes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Attributes == null)
            {
                return NotFound();
            }

            var attribute = await _context.Attributes.FindAsync(id);
            if (attribute == null)
            {
                return NotFound();
            }
            ViewData["EntityId"] = new SelectList(_context.Entities, "EntityId", "DisplayName", attribute.EntityId);
            return View(attribute);
        }

        // POST: Attributes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttributeId,LogicalName,DisplayName,Description,EntityId")] Attribute attribute)
        {
            if (id != attribute.AttributeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attribute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributeExists(attribute.AttributeId))
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
            ViewData["EntityId"] = new SelectList(_context.Entities, "EntityId", "DisplayName", attribute.EntityId);
            return View(attribute);
        }

        // GET: Attributes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attributes == null)
            {
                return NotFound();
            }

            var attribute = await _context.Attributes
                .Include(a => a.Entity)
                .FirstOrDefaultAsync(m => m.AttributeId == id);
            if (attribute == null)
            {
                return NotFound();
            }

            return View(attribute);
        }

        // POST: Attributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attributes == null)
            {
                return Problem("Entity set 'LowCodeDbContext.Attributes'  is null.");
            }
            var attribute = await _context.Attributes.FindAsync(id);
            if (attribute != null)
            {
                _context.Attributes.Remove(attribute);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttributeExists(int id)
        {
            return (_context.Attributes?.Any(e => e.AttributeId == id)).GetValueOrDefault();
        }
    }
}
