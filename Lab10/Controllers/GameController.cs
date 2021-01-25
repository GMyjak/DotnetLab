using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;

namespace Lab10.Controllers
{
    public class GameController : Controller
    {
        //private static int bound = -1;
        //private static int drawn = -1;
        private static readonly string boundCookieTag = "bound";
        private static readonly string drawnCookieTag = "drawn";

        [Route("Set,{n}")]
        public IActionResult Set([FromRoute] int n)
        {
            if (n < 1)
            {
                ViewBag.Message = "Nieprawidłowa wartość n: " + n;
                ViewBag.Color = "color:red";
                return View();
            }

            HttpContext.Session.SetInt32(boundCookieTag, n);
            //bound = n;
            
            ViewBag.Message = "Wartość graniczna ustawiona na " + n;
            ViewBag.Color = "color:green";
            
            return View();
        }

        [Route("Draw")]
        public IActionResult Draw()
        {
            int? bound = HttpContext.Session.GetInt32(boundCookieTag);
            if (bound == null)
            {
                ViewBag.Message = "Wartość graniczna nie została jeszcze ustawiona\nUżyj opcji '/set,{n}'";
                ViewBag.Color = "color:red";
                return View();
            }

            var rand = new Random();
            int drawn = rand.Next(bound.Value);
            HttpContext.Session.SetInt32(drawnCookieTag, drawn);

            ViewBag.Message = "Wartość została wylosowana: ???";
            ViewBag.Color = "color:black";

            return View();
        }

        [Route("Guess,{n}")]
        public IActionResult Guess([FromRoute] int n)
        {
            int? drawn = HttpContext.Session.GetInt32(drawnCookieTag);
            if (drawn == null)
            {
                ViewBag.Message = "Liczba nie została jeszcze wylosowana\nUżyj opcji '/draw'";
                ViewBag.Color = "color:red";
                return View();
            }
            else if (drawn == n)
            {
                ViewBag.Message = "Zgadłeś, liczba to " + n;
                ViewBag.Color = "color:green";
                return View();
            }
            else if (drawn > n)
            {
                ViewBag.Message = "Podana liczba jest za mała. Próbuj dalej";
                ViewBag.Color = "color:black";
                return View();
            }
            else
            {
                ViewBag.Message = "Podana liczba jest za duża. Próbuj dalej";
                ViewBag.Color = "color:black";
                return View();
            }
        }
    }
}
