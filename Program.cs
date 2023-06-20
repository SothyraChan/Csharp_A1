using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestDealership();
        }

        static void TestDealership()
        {

            Dealership dealership1 = new Dealership("D1_22_T501", "The Six Cars", "1 Main Street, Toronto");
            Console.WriteLine(dealership1.ToString());

            Dealership dealership2 = new Dealership("D2_22_B321", "Car Street", "5th avenue, Brampton");
            Console.WriteLine(dealership2.ToString());

            Console.WriteLine("\nToyota Cars available in Dealership 1");
            dealership1.ShowCars("Toyota");

            Console.WriteLine("\n\nToyota Cars available in Dealership 2");
            dealership2.ShowCars("Toyota");

            Car favCar = new Car("Hyundai", 2020, "Elantra", 30000.00, CarType.Sedan);
            Console.WriteLine($"\n\nCar to match : {favCar.ToString()}");

            Console.WriteLine("\nMatching car(s) from Dealership 1 : ");
            dealership1.ShowCars(favCar);

            Console.WriteLine("\nMatching car(s) from Dealership 2 : ");
            dealership2.ShowCars(favCar);

            //favCar = new Car("Honda", 2018, "Civic", 20000.00, CarType.SUV, CarSpecifications.FogLights | CarSpecifications.TintendGlasses);
            favCar = new Car("Honda", 2018, "Civic", 20000.00, CarType.SUV);

            Console.WriteLine($"\n\nCar to match : {favCar.ToString()}");

            Console.WriteLine("\nMatching car(s) from Dealership 1 : ");
            dealership1.ShowCars(favCar);

            Console.WriteLine("\nMatching car(s) from Dealership 2 : ");
            dealership2.ShowCars(favCar);

            Console.WriteLine("\nList of similiar car models available in both dealership : ");

            foreach (Car firstCar in dealership1.CarList)
            {
                foreach (Car secondsCar in dealership2.CarList)
                {
                    if (firstCar == secondsCar)
                    {
                        Console.WriteLine($"Dealership 1 : {firstCar.ToString()}");
                        Console.WriteLine($"Dealership 2 : {secondsCar.ToString()}");
                    }
                }
            }
        }

        public enum CarType
        {
            SUV, Hatchback, Sedan, Truck
        }

        public class Car
        {
            //static property
            public static int VI_NUMBER = 1021;
            public string Manufacturer { get; }
            public int Make { get; }
            public string Model { get; }
            public int VIN { get; }
            public double BasePrice { get; }
            public CarType Type { get; }


            /* A constructor for Car */
            public Car(string manufacturer, int make, string model, double basePrice, CarType type)
            {
                this.Manufacturer = manufacturer;
                this.Make = make;
                this.Model = model;
                VIN = VI_NUMBER + 100;
                VI_NUMBER += 100;
                this.BasePrice = basePrice;
                this.Type = type;
            }


            /* Methods for comparing the equal of 2 cars instances */
            public static bool operator ==(Car first, Car second)
            {
                bool result = false;
                if (first.Manufacturer == second.Manufacturer
                    && first.Model == second.Model && first.Type == second.Type)
                {
                    result = true;
                }
                return result;

            }

            /* Methods for comparing the unequal of 2 cars instances */
            public static bool operator !=(Car first, Car second)
            {
                bool result = false;
                if (first.Manufacturer != second.Manufacturer
                    && first.Model != second.Model && first.Type != second.Type)
                {
                    result = true;
                }
                return result;
            }

            /* override string */
            public override string ToString()
            {
                return $"\n{VIN} : {this.Manufacturer}, {this.Make}, {this.Model}, {this.BasePrice}, {this.Type}";
            }
        }


        /* Dealership method */
        public class Dealership
        {
            public const string FILENAME = "Dealership_Cars.txt";
            public string ID { get; }
            public string Name { get; }
            public string Address { get; }
            public List<Car> CarList { get; }


            /* Dealership constructor */
            public Dealership(string ID, string name, string address)
            {
                this.ID = ID;
                this.Name = name;
                this.Address = address;
                this.CarList = new List<Car>(); //allocating the memory

                try
                {
                    using (StreamReader reader = new StreamReader(FILENAME))
                    {
                        string dataLine;

                        while ((dataLine = reader.ReadLine()) != null)
                        {
                            string[] values = dataLine.Split(',');

                            /* checking if the first value matches the Dealer Id */
                            if (values[0] == this.ID)
                            {
                                string Manufacturer = values[1];
                                //int Make = int.Parse(values[2]);
                                int Make = Convert.ToInt32(values[2]);
                                string Model = values[3];
                                //double BasePrice = double.Parse(values[4]);
                                double BasePrice = Convert.ToDouble(values[4]);
                                CarType carType = (CarType)Enum.Parse(typeof(CarType), values[5]);
                                Car addedCar = new Car(Manufacturer, Make, Model, BasePrice, carType);
                                //this.CarList.Add(addedCar);
                                CarList.Add(addedCar);

                            }
                        }

                        reader.Close();
                    }

                }//try ends
                catch (FileLoadException fne)
                {
                    Console.WriteLine($"Please check the filename {FILENAME} and location");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong while reading " +
                        $"the file {FILENAME}");
                    Console.WriteLine($"{ex}");
                }
            }


            /* Method for showing all the cars which the manufacturer is same as the
            given parameter in the output */
            public void ShowCars(string manufacturer)
            {
                Boolean found = false;
                foreach (Car car in this.CarList)
                {
                    //if (car.Manufacturer.ToLower() == manufacturer.ToLower())
                    if (car.Manufacturer == manufacturer)
                    {
                        Console.WriteLine(car);
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine($"None");
                }
            }

            /* Method for the matched object given in carToBeSearched property */
            public void ShowCars(Car carToBeSearched)
            {
                Boolean found = false;
                foreach (Car car in this.CarList)
                {
                    if (car == carToBeSearched)
                    {
                        Console.WriteLine(car);
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine($"None");
                }
            }

            /* Override string */
            public override string ToString()
            {
                string result = $"\n{this.ID},{this.Name},{this.Address}\n\n";
                foreach (Car car in this.CarList)
                {
                    result += car.ToString();
                }
                return result;
            }
        }
    }
}
