using Microsoft.AspNetCore.Mvc;
using System;

namespace Lab10.Controllers
{
    public class GameController : Controller
    {
        private static int bound = -1;
        private static int drawn = -1;

        [Route("Set,{n}")]
        public IActionResult Set([FromRoute] int n)
        {
            if (n < 1)
            {
                ViewBag.Message = "Nieprawidłowa wartość n: " + n;
                ViewBag.Color = "color:red";
                return View();
            }

            bound = n;
            
            ViewBag.Message = "Wartość graniczna ustawiona na " + n;
            ViewBag.Color = "color:green";
            
            return View();
        }

        [Route("Draw")]
        public IActionResult Draw()
        {
            if (bound == -1)
            {
                ViewBag.Message = "Wartość graniczna nie została jeszcze ustawiona\nUżyj opcji '/set,{n}'";
                ViewBag.Color = "color:red";
                return View();
            }

            var rand = new Random();
            drawn = rand.Next(bound);

            ViewBag.Message = "Wartość została wylosowana: ???";
            ViewBag.Color = "color:black";

            return View();
        }

        [Route("Guess,{n}")]
        public IActionResult Guess([FromRoute] int n)
        {
            if (drawn == -1)
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
