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
            var lowCodeDbContext = _context.Attributes.Include(i => i.AttributeType).Include(i => i.Entity);
            return View(await lowCodeDbContext.ToListAsync());
        }

        // GET: Attributes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Attributes == null)
            {
                return NotFound();
            }

            var internalAttribute = await _context.Attributes
                .Include(i => i.AttributeType)
                .Include(i => i.Entity)
                .FirstOrDefaultAsync(m => m.AttributeId == id);
            if (internalAttribute == null)
            {
                return NotFound();
            }

            return View(internalAttribute);
        }

        // GET: Attributes/Create
        public IActionResult Create()
        {
            ViewData["AttributeTypeId"] = new SelectList(_context.AttributeTypes, "AttributeTypeId", "Name");
            ViewData["EntityId"] = new SelectList(_context.Entities, "EntityId", "DisplayName");
            return View();
        }

        // POST: Attributes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttributeId,LogicalName,DisplayName,AttributeMask,DefaultValue,IsCustomField,IsPKAttribute,MaxLength,MinValue,MaxValue,IsActive,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,EntityId,AttributeTypeId")] InternalAttribute internalAttribute)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(internalAttribute);
                var entity = _context.Entities.Find(internalAttribute.EntityId);
                if (entity != null)
                {
                    DatabaseHelper.AddAttribute(entity.LogicalName, internalAttribute);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["AttributeTypeId"] = new SelectList(_context.AttributeTypes, "AttributeTypeId", "Name", internalAttribute.AttributeTypeId);
            ViewData["EntityId"] = new SelectList(_context.Entities, "EntityId", "DisplayName", internalAttribute.EntityId);
            return View(internalAttribute);
        }

        // GET: Attributes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Attributes == null)
            {
                return NotFound();
            }

            var internalAttribute = await _context.Attributes.FindAsync(id);
            if (internalAttribute == null)
            {
                return NotFound();
            }
            ViewData["AttributeTypeId"] = new SelectList(_context.AttributeTypes, "AttributeTypeId", "Name", internalAttribute.AttributeTypeId);
            ViewData["EntityId"] = new SelectList(_context.Entities, "EntityId", "DisplayName", internalAttribute.EntityId);
            return View(internalAttribute);
        }

        // POST: Attributes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("AttributeId,LogicalName,DisplayName,AttributeMask,DefaultValue,IsCustomField,IsPKAttribute,MaxLength,MinValue,MaxValue,IsActive,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,EntityId,AttributeTypeId")] InternalAttribute internalAttribute)
        {
            if (id != internalAttribute.AttributeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(internalAttribute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternalAttributeExists(internalAttribute.AttributeId))
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
            ViewData["AttributeTypeId"] = new SelectList(_context.AttributeTypes, "AttributeTypeId", "Name", internalAttribute.AttributeTypeId);
            ViewData["EntityId"] = new SelectList(_context.Entities, "EntityId", "DisplayName", internalAttribute.EntityId);
            return View(internalAttribute);
        }

        // GET: Attributes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Attributes == null)
            {
                return NotFound();
            }

            var internalAttribute = await _context.Attributes
                .Include(i => i.AttributeType)
                .Include(i => i.Entity)
                .FirstOrDefaultAsync(m => m.AttributeId == id);
            if (internalAttribute == null)
            {
                return NotFound();
            }

            return View(internalAttribute);
        }

        // POST: Attributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            if (_context.Attributes == null)
            {
                return Problem("Entity set 'LowCodeDbContext.Attributes'  is null.");
            }
            var internalAttribute = await _context.Attributes.FindAsync(id);
            if (internalAttribute != null)
            {
                _context.Attributes.Remove(internalAttribute);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InternalAttributeExists(Guid? id)
        {
          return (_context.Attributes?.Any(e => e.AttributeId == id)).GetValueOrDefault();
        }
    }
}
