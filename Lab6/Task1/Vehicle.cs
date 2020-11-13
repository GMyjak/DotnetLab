using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6.Task1
{
    abstract class Vehicle
    {
        protected double price;
        public double Price
        {
            get { return price;}
            set { price = Math.Max(0, value); }
        }
        public string Brand { get; set; }

        public bool MakeBid(double amountOfMoney)
        {
            if (amountOfMoney > Price)
            {
                Price = amountOfMoney;
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"Vehicle to be sold: {Brand}, initial price {Price}";
        }
    }
}
