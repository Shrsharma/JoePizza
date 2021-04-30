using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using ModelLayer;

namespace JoePizza.Controllers
{
    public class PizzaController : Controller
    {
        private readonly AppDbContext _context;

        public PizzaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Pizza
        public async Task<IActionResult> Index()
        {
            return View(await _context.PizzaModel.ToListAsync());
        }

        // GET: Pizza/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaModel = await _context.PizzaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaModel == null)
            {
                return NotFound();
            }

            return View(pizzaModel);
        }

        // GET: Pizza/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pizza/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Pizza,Description,Price")] PizzaModel pizzaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pizzaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pizzaModel);
        }

        // GET: Pizza/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaModel = await _context.PizzaModel.FindAsync(id);
            if (pizzaModel == null)
            {
                return NotFound();
            }
            return View(pizzaModel);
        }

        // POST: Pizza/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Pizza,Description,Price")] PizzaModel pizzaModel)
        {
            if (id != pizzaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizzaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaModelExists(pizzaModel.Id))
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
            return View(pizzaModel);
        }

        // GET: Pizza/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaModel = await _context.PizzaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaModel == null)
            {
                return NotFound();
            }

            return View(pizzaModel);
        }

        // POST: Pizza/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var pizzaModel = await _context.PizzaModel.FindAsync(id);
            _context.PizzaModel.Remove(pizzaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaModelExists(string id)
        {
            return _context.PizzaModel.Any(e => e.Id == id);
        }
    }
}
