using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6.Task2
{
    class ExampleA : IFigure
    {
        public void MoveTo(double x, double y)
        {
            Console.WriteLine($"Moved ExampleA instance to ({x}, {y})");
        }
    }

    class ExampleB : IFigure, IHasInterior
    {
        public void MoveTo(double x, double y)
        {
            Console.WriteLine($"Moved ExampleB instance to ({x}, {y})");
        }

        string IHasInterior.Color { get; set; }
    }
}
