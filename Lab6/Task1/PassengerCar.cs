using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6.Task1
{
    class PassengerCar : Car
    {
        public int NumberOfSeats { get; set; }
        public string BodyType { get; set; }

        public bool IsSportsCar()
        {
            return EnginePower > 360;
        }

        public override string ToString()
        {
            string result = IsSportsCar() ? "This is sports car!!!\n" : string.Empty;
            return result + $"{base.ToString()}\nCar has {NumberOfSeats} seats and {BodyType} body type";
        }
    }
}
