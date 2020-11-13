using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6.Task1
{
    abstract class Car : Vehicle
    {
        public string Model { get; set; }
        private int enginePower;
        public int EnginePower
        {
            get { return enginePower; }
            set { enginePower = Math.Max(0, value); }
        }
        private string FuelType { get; set; } // Gasoline, diesel, ...

        public string BrandWithModel()
        {
            if (!string.IsNullOrWhiteSpace(Brand) && !string.IsNullOrWhiteSpace(Model))
            {
                return $"Car: {Brand} {Model}";
            }
            else
            {
                return "Invalid data format";
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}\nCar model: {Model}\nCar has {enginePower}HP engine fueled by {FuelType}";
        }
    }
}
