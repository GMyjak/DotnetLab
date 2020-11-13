using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6.Task2
{
    interface IFigure
    {
        void MoveTo(double x, double y);
    }

    interface IHasInterior
    {
        string Color { get; set; }
    }
}
