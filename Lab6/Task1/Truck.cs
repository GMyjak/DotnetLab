using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6.Task1
{
    class Truck : Car
    {
        private double loadCapacityInKilograms;
        public double LoadCapacityInKilograms
        {
            get { return loadCapacityInKilograms; }
            set { loadCapacityInKilograms = Math.Max(0, value); }
        }
        public string LoadType { get; set; }

        public bool CanTransportAdultElephant()
        {
            return loadCapacityInKilograms >= 8500;
        }

        public override string ToString()
        {
            string result = base.ToString() + $"\nIt can load {loadCapacityInKilograms}kg of {LoadType}";
            return CanTransportAdultElephant() ? result + "\nTruck can transport elephants!" : result;
        }
    }
}
