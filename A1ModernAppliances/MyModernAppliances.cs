using ModernAppliances.Entities;
using ModernAppliances.Entities.Abstract;
using ModernAppliances.Helpers;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace ModernAppliances
{
    /// <summary>
    /// Manager class for Modern Appliances
    /// </summary>
    /// <remarks>Author: Tyler Galea</remarks>
    /// <remarks>Date: June 11, 2023</remarks>
    /// This program allows the user to select one of 5 options to manage th Appliances.
    /// If the user picks the check out option, they are asked to enter the item number of the appliance
    /// and tells the user that they checked out the product, the product is not avalible, or does not exist.
    /// If the user picks the find appliances option, they are asked to enter a brand and then a list of
    /// every appliance with that brand is printed out.
    /// If the user picks the display option, they are asked to pick a type of appliance and enter an 
    /// attribiute of that appliance then prints out a list of appliance of that type with the attribiute.
    /// If the user picks the random list option, they are asked to enter the number of appliances to print
    /// and then prints a random list of appliace with the amount entered.
    internal class MyModernAppliances : ModernAppliances
    {
        /// <summary>
        /// Option 1: Performs a checkout
        /// </summary>
        public override void Checkout()
        {
            Console.Write("Enter the item number of an appliance:\n    ");
            long itemNumber;
            String input = Console.ReadLine();
            itemNumber = long.Parse(input);

            Appliance? foundAppliance = null;

            foreach (Appliance appliance in Appliances)
            {
                if (itemNumber == appliance.ItemNumber)
                {
                    foundAppliance = appliance;
                    break;
                }
            }

            if (foundAppliance == null) 
            {
                Console.WriteLine("No appliances found with that item number.");
            } 
            else
            {
                if (foundAppliance.IsAvailable)
                {
                    foundAppliance.Checkout();
                    Console.WriteLine("Appliance \"" + itemNumber + "\" has been checked out.");
                }
                else
                {
                    Console.WriteLine("The appliance is not available to be checked out.");
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Option 2: Finds appliances
        /// </summary>
        public override void Find()
        {
            Console.Write("Enter brand to search for:\n    ");
            string brand = Console.ReadLine();

            List<Appliance> foundAppliancesList = new List<Appliance>();
            
            foreach (Appliance appliance in Appliances) 
            {
                if (brand.ToLower() == appliance.Brand.ToLower())
                {
                    foundAppliancesList.Add(appliance);
                }
            }
            Console.WriteLine("Matching Appliances:");
            DisplayAppliancesFromList(foundAppliancesList, 0);
        }

        /// <summary>
        /// Displays Refridgerators
        /// </summary>
        public override void DisplayRefrigerators()
        {
            Console.Write("Enter number of doors: 2 (double door), 3 (three doors) or 4 (four doors):\n    ");
            int numOfDoors;
            string input = Console.ReadLine();
            numOfDoors = int.Parse(input);

            if (numOfDoors == 2 || numOfDoors == 3 || numOfDoors == 4)
            {
                List<Appliance> foundAppliancesList = new List<Appliance>();

                foreach (Appliance appliance in Appliances)
                {
                    if (appliance is Refrigerator)
                    {
                        Refrigerator refrigerator = (Refrigerator)appliance;
                        if (refrigerator.Doors == numOfDoors)
                        {
                            foundAppliancesList.Add(appliance);
                        }
                    }
                }
                Console.WriteLine("Matching refrigerators:");
                DisplayAppliancesFromList(foundAppliancesList, 0);
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }

        /// <summary>
        /// Displays Vacuums
        /// </summary>
        /// <param name="grade">Grade of vacuum to find (or null for any grade)</param>
        /// <param name="voltage">Vacuum voltage (or 0 for any voltage)</param>
        public override void DisplayVacuums()
        {
            Console.Write("Enter battery voltage value. 18 V (low) or 24 V (high)\n    ");
            String input = Console.ReadLine();
            int voltage = int.Parse(input);

            if (voltage == 18 || voltage == 24)
            {
                List<Appliance> foundAppliancesList = new List<Appliance>();

                foreach (Appliance appliance in Appliances)
                {
                    if (appliance is Vacuum)
                    {
                        Vacuum vacuum = (Vacuum)appliance;
                        if (vacuum.BatteryVoltage == voltage)
                        {
                            foundAppliancesList.Add(vacuum);
                        }
                    }
                }
                Console.WriteLine("Matching vacuums:");
                DisplayAppliancesFromList(foundAppliancesList, 0);
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }

        /// <summary>
        /// Displays microwaves
        /// </summary>
        public override void DisplayMicrowaves()
        {
            Console.Write("Room where the microwave will be installed: K (kitchen) or W (work site):\n    ");
            String input = Console.ReadLine();
            char roomType = Char.Parse(input);

            if (roomType == 'K' || roomType == 'W')
            {

                List<Appliance> foundAppliancesList = new List<Appliance>();

                foreach (Appliance appliance in Appliances)
                {
                    if (appliance is Microwave)
                    {
                        Microwave microwave = (Microwave)appliance;
                        if (microwave.RoomType == roomType)
                        {
                            foundAppliancesList.Add(microwave);
                        }
                    }
                }
                Console.WriteLine("Matching microwaves:");
                DisplayAppliancesFromList(foundAppliancesList, 0);
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }

        /// <summary>
        /// Displays dishwashers
        /// </summary>
        public override void DisplayDishwashers()
        {
            Console.Write("Enter the sound rating of the dishwasher: Qt (Quietest), Qr (Quieter), Qu(Quiet) or M (Moderate):\n    ");
            string soundRating = Console.ReadLine();

            if (soundRating == "Qt" || soundRating == "Qr" || soundRating == "Qu" || soundRating == "M")
            {
                List<Appliance> foundAppliancesList = new List<Appliance>();

                foreach (Appliance appliance in Appliances)
                {
                    if (appliance is Dishwasher)
                    {
                        Dishwasher dishwasher = (Dishwasher)appliance;
                        if (soundRating == "Any" || dishwasher.SoundRating == soundRating)
                        {
                            foundAppliancesList.Add(dishwasher);
                        }
                    }
                }
                Console.WriteLine("Matching dishwashers:");
                DisplayAppliancesFromList(foundAppliancesList, 0);
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }

        /// <summary>
        /// Generates random list of appliances
        /// </summary>
        public override void RandomList()
        {
            Console.Write("Enter number of appliances:\n    ");
            String input = Console.ReadLine();
            int num = int.Parse(input);

            List<Appliance> foundAppliancesList = Appliances;
            foundAppliancesList.Sort(new RandomComparer());
            Console.WriteLine("Random appliances:");
            DisplayAppliancesFromList(foundAppliancesList, num);
        }
    }
}
