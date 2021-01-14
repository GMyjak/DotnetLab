using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab12.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Lab12.Controllers
{
    public class ShopController : Controller
    {
        private readonly DotNetL12Context _context;

        public ShopController(DotNetL12Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["CategoryId"] = await _context.Category.ToListAsync();
            var articles = await _context.Article
                .Include(art => art.Category)
                .ToListAsync();
            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> SortById(int id)
        {
            ViewData["CategoryId"] = await _context.Category.ToListAsync();
            var articles = await _context.Article
                .Where(art => art.CategoryId == id)
                .Include(art => art.Category)
                .ToListAsync();
            return View("Index", articles);
        }
    }
}
