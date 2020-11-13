using System;
using System.Collections.Generic;
using System.Linq;
using Lab6.Task1;
using Lab6.Task2;

namespace Lab6
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("TASK1");
            TestTask1();
            Console.WriteLine("\n\nTASK2");
            TestTask2();
        }

        static void TestTask1()
        {
            List<Vehicle> vehicleAuctionList = new List<Vehicle>();
            vehicleAuctionList.Add(new Bike
            {
                Brand = "MTB", 
                Price = 2100, 
                FrameSizeInInches = 17, 
                WheelSizeInInches = 27.5
            });

            vehicleAuctionList.Add(new PassengerCar
            {
                Brand = "Nissan",
                Model = "Murano",
                EnginePower = 234,
                BodyType = "SUV",
                NumberOfSeats = 5,
                Price = 10900
            });

            vehicleAuctionList.Add(new PassengerCar
            {
                Brand = "Kia",
                Model = "Ceed",
                EnginePower = 90,
                BodyType = "Hatchback",
                NumberOfSeats = 5,
                Price = 21300
            });

            vehicleAuctionList.Add(new Truck
            {
                Brand = "Iveco",
                Model = "Eurocargo",
                EnginePower = 180,
                LoadCapacityInKilograms = 7000,
                LoadType = "Box",
                Price = 9000
            });

            vehicleAuctionList.Add(new Truck
            {
                Brand = "DAF",
                Model = "CF GINAF",
                EnginePower = 380,
                LoadCapacityInKilograms = 26000,
                LoadType = "Tipper",
                Price = 85000
            });

            foreach (Vehicle v in vehicleAuctionList)
            {
                Console.WriteLine(v + "\n");
            }

            Console.WriteLine("Truck load sum with 'is' operator: " + SumLoadIs(vehicleAuctionList));
            Console.WriteLine("Truck load sum with 'as' operator: " + SumLoadAs(vehicleAuctionList));
        }

        static double SumLoadIs(IEnumerable<Vehicle> vehicles)
        {
            return vehicles
                .Where(v => v is Truck)
                .Sum(v => ((Truck) v).LoadCapacityInKilograms);
        }

        static double SumLoadAs(IEnumerable<Vehicle> vehicles)
        {
            return vehicles
                .Select(v => v as Truck)
                .Sum(v => v?.LoadCapacityInKilograms ?? 0);
        }

        static void TestTask2()
        {
            List<object> collection = new List<object>();
            collection.Add(new ExampleA());
            collection.Add(new ExampleB());
            ((IHasInterior) collection[^1]).Color = "Blue";
            collection.Add(new ExampleB());
            ((IHasInterior)collection[^1]).Color = "Green";
            collection.Add(new ExampleA());
            collection.Add(new ExampleB());
            ((IHasInterior)collection[^1]).Color = "Red";
            collection.Add(new ExampleB());
            ((IHasInterior)collection[^1]).Color = "Yellow";

            PrintColors(collection);
        }

        static void PrintColors(IEnumerable<object> values)
        {
            foreach (var value in values)
            {
                var coloredValue = value as IHasInterior;
                Console.WriteLine(coloredValue == null ? "no color" : coloredValue.Color);
            }
        }
    }
}
