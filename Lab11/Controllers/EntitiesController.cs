using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab11.Models;

namespace Lab11.Controllers
{
    public class EntitiesController : Controller
    {
        private readonly Dictionary<int, Entity> _context;

        public EntitiesController(Dictionary<int, Entity> context)
        {
            _context = context;
        }

        // GET: Entities
        public IActionResult Index()
        {
            return View(_context.Values.ToList());
        }

        // GET: Entities/Details/5
        public IActionResult Details(int id)
        {
            var entity = _context.ContainsKey(id) ? _context[id] : null;

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Entities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Key,Email,Name,Postcode,EnumValue")] Entity entity)
        {
            if (ModelState.IsValid)
            {
                int index = _context.Count > 0 ? _context.Keys.Max() + 1 : 0;
                entity.Key = index;
                _context.Add(entity.Key, entity);
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        // GET: Entities/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _context.ContainsKey(id.Value) ? _context[id.Value] : null;
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        // POST: Entities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Key,Email,Name,Postcode,EnumValue")] Entity entity)
        {
            if (id != entity.Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_context.ContainsKey(id))
                {
                    _context[id] = entity;
                }
                else
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        // GET: Entities/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _context.ContainsKey(id.Value) ? _context[id.Value] : null;
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: Entities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var entity = _context.ContainsKey(id) ? _context[id] : null;
            if (entity != null)
            {
                _context.Remove(id);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
