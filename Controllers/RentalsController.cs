using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab_3.Models;

namespace lab_3.Controllers
{
    public class RentalsController : Controller
    {
        private readonly RentalDbContext _context;

        public RentalsController(RentalDbContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var rentalDbContext = _context.Rentals.Include(r => r.Client).Include(r => r.Product);
            return View(await rentalDbContext.ToListAsync());
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Client)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            var clientsList = _context.Clients
                .Select(c => new { Id = c.Id, FullName = c.LastName + " " + c.FirstName })
                .ToList();

            ViewData["ClientId"] = new SelectList(clientsList, "Id", "FullName");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title");
            return View();
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,ProductId,IssueDate,ReturnDate")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var clientsList = _context.Clients
                .Select(c => new { Id = c.Id, FullName = c.LastName + " " + c.FirstName })
                .ToList();

            ViewData["ClientId"] = new SelectList(clientsList, "Id", "FullName", rental.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title", rental.ProductId);
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            var clientsList = _context.Clients
                .Select(c => new { Id = c.Id, FullName = c.LastName + " " + c.FirstName })
                .ToList();

            ViewData["ClientId"] = new SelectList(clientsList, "Id", "FullName", rental.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title", rental.ProductId);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,ProductId,IssueDate,ReturnDate")] Rental rental)
        {
            if (id != rental.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.Id))
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

            var clientsList = _context.Clients
                .Select(c => new { Id = c.Id, FullName = c.LastName + " " + c.FirstName })
                .ToList();

            ViewData["ClientId"] = new SelectList(clientsList, "Id", "FullName", rental.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title", rental.ProductId);
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Client)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.Id == id);
        }
    }
}