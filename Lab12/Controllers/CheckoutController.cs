using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab12.Data;
using Lab12.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Lab12.Controllers
{
    public class CheckoutController : Controller
    {
        private static readonly string checkoutDataTag = "checkout";

        private readonly DotNetL12Context _context;

        public CheckoutController(DotNetL12Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string cookie = Request.Cookies[checkoutDataTag] ?? "{}"; // Empty dict
            Dictionary<int, int> itemsInCart =
                JsonConvert.DeserializeObject<Dictionary<int, int>>(cookie) ?? new Dictionary<int, int>();

            var articles = await _context.Article.Include(art => art.Category).ToListAsync();
            articles = articles.Where(art => itemsInCart.ContainsKey(art.Id)).ToList();

            Dictionary<Article, int> model = new Dictionary<Article, int>();
            articles.ForEach(art => model.Add(art, itemsInCart[art.Id]));

            return View(model);
        }

        public async Task<IActionResult> AddItem(int id)
        {
            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound("XD??");
            }

            string cookie = Request.Cookies[checkoutDataTag] ?? "{}"; // Empty dict
            Dictionary<int, int> itemsInCart = 
                JsonConvert.DeserializeObject<Dictionary<int, int>>(cookie) ?? new Dictionary<int, int>();

            if (!itemsInCart.ContainsKey(id))
            {
                itemsInCart.Add(id, 1);
            }
            else
            {
                int count = itemsInCart[id]; // todo idk if necessary
                count++;
                itemsInCart[id] = count;
            }

            string result = JsonConvert.SerializeObject(itemsInCart);
            Response.Cookies.Append(checkoutDataTag, result, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            });


            var articles = await _context.Article.Include(art => art.Category).ToListAsync();
            articles = articles.Where(art => itemsInCart.ContainsKey(art.Id)).ToList();

            Dictionary<Article, int> model = new Dictionary<Article, int>();
            articles.ForEach(art => model.Add(art, itemsInCart[art.Id]));

            //return RedirectToAction("Index", "Shop");
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> AddOne(int id)
        {
            return await Add(id, 1, false);
        }

        public async Task<IActionResult> RemoveOne(int id)
        {
            return await Add(id, -1, false);
        }

        public async Task<IActionResult> RemoveAll(int id)
        {
            return await Add(id, 0, true);
        }

        private async Task<IActionResult> Add(int id, int amount, bool removeAll)
        {
            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound("XD??");
            }

            string cookie = Request.Cookies[checkoutDataTag] ?? "{}"; // Empty dict
            Dictionary<int, int> itemsInCart =
                JsonConvert.DeserializeObject<Dictionary<int, int>>(cookie) ?? new Dictionary<int, int>();

            if (!itemsInCart.ContainsKey(id))
            {
                return NotFound();
            }
            else
            {
                int count = itemsInCart[id];
                count = removeAll ? 0 : count + amount;
                if (count <= 0)
                {
                    itemsInCart.Remove(id);
                }
                else
                {
                    itemsInCart[id] = count;
                }
            }

            string result = JsonConvert.SerializeObject(itemsInCart);
            Response.Cookies.Append(checkoutDataTag, result, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            });

            var articles = await _context.Article.Include(art => art.Category).ToListAsync();
            articles = articles.Where(art => itemsInCart.ContainsKey(art.Id)).ToList();

            Dictionary<Article, int> model = new Dictionary<Article, int>();
            articles.ForEach(art => model.Add(art, itemsInCart[art.Id]));

            return Redirect(Request.Headers["Referer"].ToString());
            //return Ok();
        }
    }
}
