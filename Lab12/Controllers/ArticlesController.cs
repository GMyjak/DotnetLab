using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab12.Data;
using Lab12.Models;
using Microsoft.AspNetCore.Hosting;

namespace Lab12.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly DotNetL12Context _context;
        private readonly IWebHostEnvironment _environment;

        public ArticlesController(DotNetL12Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var dotNetL12Context = _context.Article.Include(a => a.Category);
            return View(await dotNetL12Context.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleDto articleDto)
        {
            if (ModelState.IsValid)
            {
                Article article = articleDto.CastToArticle();
                
                // Save file into upload directory
                if (articleDto.Image != null)
                {
                    string filePath = Path.Combine(_environment.WebRootPath, "upload", article.ImagePath);
                    if (!System.IO.File.Exists(filePath))
                    {
                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        await articleDto.Image.CopyToAsync(fs);
                        fs.Close();
                    }
                }

                // Add info to db
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", articleDto.CategoryId);
            return View(articleDto);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Article article)
        {
            Article oldArticle = await _context.Article.FirstOrDefaultAsync(art => art.Id == article.Id);

            if (id != article.Id || oldArticle == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                oldArticle.Name = article.Name;
                oldArticle.CategoryId = article.CategoryId;
                oldArticle.Price = article.Price;
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Article.FindAsync(id);
            if (article?.ImagePath != null)
            {
                string associatedImagePath = Path.Combine(_environment.WebRootPath, "upload", article.ImagePath);
                if (System.IO.File.Exists(associatedImagePath))
                {
                    System.IO.File.Delete(associatedImagePath);
                }
            }
            _context.Article.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
