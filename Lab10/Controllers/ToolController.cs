using Microsoft.AspNetCore.Mvc;

namespace Lab10.Controllers
{
    public class ToolController : Controller
    {
        public IActionResult Index(int a, int b, int c)
        {
            string equation = a != 0 ? $"{a}x²" : string.Empty;
            if (equation != string.Empty && b > 0)
            {
                equation += "+";
            }
            equation += b != 0 ? $"{b}x" : string.Empty;
            if (equation != string.Empty && c > 0)
            {
                equation += "+";
            }
            equation += c != 0 ? $"{c}" : string.Empty;
            ViewBag.Equation = equation;
            string solution = Lab2.Program.SolveEquation(a, b, c, out int numOfSolutions);
            ViewBag.NumSolutions = numOfSolutions;
            ViewBag.Solution = solution;

            switch (numOfSolutions)
            {
                case 0:
                    ViewBag.Color = "color:red";
                    break;
                case 1:
                    ViewBag.Color = "color:yellow";
                    break;
                case 2:
                    ViewBag.Color = "color:green";
                    break;
                case -1:
                    ViewBag.Color = "color:purple";
                    break;
                default:
                    ViewBag.Color = "color:black";
                    break;
            }

            return View();
        }
    }
}
