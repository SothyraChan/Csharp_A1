using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sothyra_A1.Sothyra_A1;

namespace Sothyra_A1
{
    internal class Sothyra_A1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Student name : Sothyra Chan");
            Console.WriteLine("Student ID : 301289779");
            Console.WriteLine("-----------------------------");

            Purchase purchase1 = new Purchase(ProductCategory.Electronics);
            purchase1.CalculateCost();
            Console.WriteLine($"{purchase1}");

            Purchase purchase2 = new Purchase(ProductCategory.CleaningSupplies, 8);
            purchase2.CalculateCost();
            Console.WriteLine($"{purchase2}");

            Purchase purchase3 = new Purchase(ProductCategory.Beverages, 12);
            purchase3.CalculateCost();
            Console.WriteLine($"{purchase3}");

            Purchase purchase4 = new Purchase(ProductCategory.Miscellaneous, 14);
            purchase4.CalculateCost();
            Console.WriteLine($"{purchase4}");
        }

        public enum ProductCategory
        {
            None, Grocery, CleaningSupplies, Beverages, Electronics, Miscellaneous
        }

        public class Purchase
        {
            //static property
            public static int Purchase_ID = 1;

            //field variables
            public ProductCategory category;
            public int quantities;
            public double cost;
            public int purchaseCounter;
           
            //constructor
            //setting the default value for quantity to 1
            public Purchase(ProductCategory category, int quantities = 1)
            {
                cost = 0;
                this.category = category;
                this.quantities = quantities;
                this.purchaseCounter = Purchase_ID++;    
            }

            //method for calculating total cost
            public void CalculateCost()
            {
                double unitCost = ProductPrice(category);
                cost = unitCost * quantities;
                double discountAmount = cost * DiscountPrice(category);
                double discountedPrice = cost - discountAmount;
                cost = discountedPrice + (discountedPrice * 0.13);
            }

            //method for showing the product prices
            public double ProductPrice(ProductCategory category)
            {
                if (category == ProductCategory.Grocery)
                {
                    return 1;
                }
                else if (category == ProductCategory.CleaningSupplies)
                {
                    return 5;
                }
                else if (category == ProductCategory.Beverages)
                {
                    return 10;
                }
                else if (category == ProductCategory.Electronics)
                {
                    return 15;
                }
                else if (category == ProductCategory.Miscellaneous)
                {
                    return 20;
                }
                else
                {
                    return 0;
                }
            }

            //method for discount products
            public double DiscountPrice(ProductCategory category)
            {
                if (category == ProductCategory.Grocery)
                {
                    return 0.2;
                }
                else if (category == ProductCategory.CleaningSupplies)
                {
                    return 0.15;
                }
                else if (category == ProductCategory.Beverages)
                {
                    return 0.05;
                }
                else if (category == ProductCategory.Electronics)
                {
                    return 0.1;
                }
                else if (category == ProductCategory.Miscellaneous)
                {
                    return 0.0;
                }
                else
                {
                    return 0;
                }
            }

            //override string method
            public override string ToString()
            {
                return $"\nPurchase_ID : {purchaseCounter} \n" +
                       $"Product Type: {category} \n" +
                       $"Product Quantity : {quantities} \n" +
                       $"Total Product Cost : ${cost:f} \n";
            }
        }
    }
}//namespace ends
