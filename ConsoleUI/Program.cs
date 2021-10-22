using System;
using System.Collections.Generic;
namespace ConsoleUI
{
    class Program
    {
        static DalObject.DalObject db = new DalObject.DalObject();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To DeliverManger!");
            bool exit = false;
            while (!exit)
                exit = mainMenu();
        }

        /// <summary>
        /// The main menu
        /// </summary>
        /// <returns>true if the user choose 'Exit', otherwise, false</returns>
        private static bool mainMenu()
        {
            Console.WriteLine("Choose one of the following:");
            Console.WriteLine("1. Add\n2. Update\n3. View\n4. View list\n5. Exit");
            int userChoose = getUserSelection(5);
            bool back = false;
            switch (userChoose)
            {
                case 1:
                    while (!back)
                        back = addMenu();
                    break;
                case 2:
                    while (!back)
                        back = updateMenu();
                    break;
                case 3:
                    while (!back)
                        back = viewMenu();
                    break;
                case 4:
                    while (!back)
                        back = viewListMenu();
                    break;
                case 5:
                    Console.WriteLine("Exiting...");
                    return true;
                default:
                    Console.WriteLine("An Error Accurd!");
                    return false;
            }
            return false;
        }

        /// <summary>
        /// The add-object menu
        /// </summary>
        /// <returns>true if the user choose 'back', otherwise, false</returns>
        private static bool addMenu()
        {
            Console.WriteLine("What do you want to add?");
            Console.WriteLine("1. Base station\n2. Drone\n3. Customer\n4. Parcel\n5. Back");
            int userChoose = getUserSelection(5);
            if (userChoose == 5)
            {
                return true;
            }
            switch (userChoose)
            {
                case 1:
                    addBaseStationMenu();
                    break;
                case 2:
                    addDroneMenu();
                    break;
                case 3:
                    addCustomerMenu();
                    break;
                case 4:
                    addParcelMenu();
                    break;
                case 5:
                    return true;
                default:
                    Console.WriteLine("An Error Accurd!");
                    break;
            }
            return false;
        }

        /// <summary>
        /// get base station info from the user, and add the base station.
        /// </summary>
        private static void addBaseStationMenu()
        {
            Console.WriteLine("Enter station name:");
            string name = Console.ReadLine();

            Tuple<double, double> location = getCoordsFromUser();

            Console.WriteLine("How many charge slots are in the station?");
            int slots = getIntFromUser();
            db.AddBase(name, location.Item1, location.Item2, slots);
        }
        /// <summary>
        /// get drone info from the user, and add the drone.
        /// </summary>
        private static void addDroneMenu()
        {
            Console.WriteLine("Enter drone model: ");
            string model = Console.ReadLine();

            Console.WriteLine("Choose MaxWeight of drone:");
            Console.WriteLine("1. Light\n2. Middle\n3. Heavy");
            int maxWeightInt = getUserSelection(3) - 1;
            db.AddDrone(model, maxWeightInt);
        }
        /// <summary>
        /// get customer info from the user, and add the customer.
        /// </summary>
        private static void addCustomerMenu()
        {
            Console.WriteLine("Enter customer name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter customer phone:");
            string phone = Console.ReadLine();

            Tuple<double, double> position = getCoordsFromUser();
            db.AddCustomer(name, phone, position.Item1, position.Item2);
        }
        /// <summary>
        /// get parcel info from the user, and add the parcel.
        /// </summary>
        private static void addParcelMenu()
        {
            Console.WriteLine("Enter sender ID:");
            int senderId = getIntFromUser();

            Console.WriteLine("Enter target ID:");
            int targetId = getIntFromUser();

            Console.WriteLine("Choose parcel weight:");
            Console.WriteLine("1. Light\n2. Middle\n3. Heavy");
            int weightInt = getUserSelection(3) - 1;

            Console.WriteLine("Choose parcel priority:");
            Console.WriteLine("1. Normal\n2. Fast\n3. Urgent");
            int priorityInt = getUserSelection(3) - 1;

            db.AddParcel(senderId, targetId, weightInt, priorityInt);
        }
        /// <summary>
        /// The update menu
        /// </summary>
        /// <returns>true if the user choose 'back', otherwise, false</returns>
        private static bool updateMenu()
        {
            Console.WriteLine("What action do you want to do?");
            Console.WriteLine("1. Link parcel to drone\n2. Pick up parcel by drone\n3. Deliver parcel to customer\n4. Send drone to charge\n5. Release drone from charger\n6. Back");
            int userChoose = getUserSelection(6);
            switch (userChoose)
            {
                case 1:
                    linkParcelMenu();
                    break;
                case 2:
                    pickUpParcelMenu();
                    break;
                case 3:
                    deliverParcel();
                    break;
                case 4:
                    sendDroneToCharge();
                    break;
                case 5:
                    releaseDroneFromCharger();
                    break;
                case 6:
                    return true;
                default:
                    Console.WriteLine("An Error Accurd!");
                    break;
            }
            return false;
        }

        /// <summary>
        /// get from the user parcel and drone IDs to link
        /// </summary>
        private static void linkParcelMenu()
        {
            Console.WriteLine("Enter parcel ID:");
            int parcelId = getIntFromUser();

            Console.WriteLine("Enter drone  ID:");
            int dronelId = getIntFromUser();
            db.linkParcel(parcelId, dronelId);
        }
        /// <summary>
        /// get parcel ID form user and pick-up the parcel
        /// </summary>
        private static void pickUpParcelMenu()
        {
            Console.WriteLine("Enter parcel ID:");
            int parcelId = getIntFromUser();
            db.PickParcel(parcelId);
        }
        /// <summary>
        /// get parcel ID form user and deliver the parcel
        /// </summary>
        private static void deliverParcel()
        {
            Console.WriteLine("Enter parcel ID:");
            int parcelId = getIntFromUser();
            db.ParcelToCustomer(parcelId);
        }
        /// <summary>
        /// get drone ID form user and pick-up the parcel
        /// </summary>
        private static void sendDroneToCharge()
        {
            Console.WriteLine("Enter drone ID:");
            int droneId = getIntFromUser();

            printListOf(printType.AvailableStation);
            Console.WriteLine("Enter station ID:");
            int stationId = getIntFromUser();
            db.DroneToBase(stationId, droneId);
        }
        /// <summary>
        /// get drone ID form user, and release the drone
        /// </summary>
        private static void releaseDroneFromCharger()
        {
            Console.WriteLine("Enter drone ID:");
            int droneId = getIntFromUser();
            db.FreeDrone(droneId);
        }

        /// <summary>
        /// View one object menu
        /// </summary>
        /// <returns>true if the user choose 'back', otherwise, false</returns>
        private static bool viewMenu()
        {
            Console.WriteLine("What do you want to view?");
            Console.WriteLine("1. Base station\n2. Drone\n3. Customer\n4. Parcel\n5. Back");
            int userChoose = getUserSelection(5);
            switch (userChoose)
            {
                case 1:
                    GetBaseStationMenu();
                    break;
                case 2:
                    GetDroneMenu();
                    break;
                case 3:
                    GetCustomerMenu();
                    break;
                case 4:
                    GetParcelMenu();
                    break;
                case 5:
                    return true;
                default:
                    Console.WriteLine("An Error Accurd!");
                    break;
            }
            return false;

        }
        /// <summary>
        /// get station ID and print the station
        /// </summary>
        private static void GetBaseStationMenu()
        {
            Console.WriteLine("Enter station ID:");
            int stationId = getIntFromUser();
            Console.WriteLine(db.GetBaseStation(stationId));
        }
        /// <summary>
        /// get drone ID and print the drone
        /// </summary>
        private static void GetDroneMenu()
        {
            Console.WriteLine("Enter drone ID:");
            int droneId = getIntFromUser();
            Console.WriteLine(db.GetDrone(droneId));
        }
        /// <summary>
        /// get customer ID and print the customer
        /// </summary>
        private static void GetCustomerMenu()
        {
            Console.WriteLine("Enter customer ID:");
            int customerId = getIntFromUser();
            Console.WriteLine(db.GetCustomer(customerId));
        }
        /// <summary>
        /// get parcel ID and print the parcel
        /// </summary>
        private static void GetParcelMenu()
        {
            Console.WriteLine("Enter parcel ID:");
            int parcelId = getIntFromUser();
            Console.WriteLine(db.GetParcerl(parcelId));
        }

        /// <summary>
        /// View list of objects menu
        /// </summary>
        /// <returns>true if the user choose 'back', otherwise, false</returns>
        private static bool viewListMenu()
        {
            Console.WriteLine("What list do you want to view?");
            Console.WriteLine("1. Base stations\n2. Drones\n3. Customers\n4. Parcels\n5. Unassigned parcels\n6. Available stations\n7. Back");
            int userChoose = getUserSelection(7);
            if (userChoose==7)//back..
            {
                return true;
            }
            printListOf((printType)userChoose - 1);
            return false;
        }
        /// <summary>
        /// print list of objects by time
        /// </summary>
        /// <param name="type">the type of object to print</param>
        private static void printListOf(printType type)
        {
            switch (type)
            {
                case printType.BaseStation:
                    Console.WriteLine("=======Stations=======");
                    foreach (var station in db.GetAllStations())
                    {
                        Console.WriteLine(station);
                        Console.WriteLine("======================");
                    }
                    break;
                case printType.Drone:
                    Console.WriteLine("========Drones========");
                    foreach (var drone in db.GetAllDrones())
                    {
                        Console.WriteLine(drone);
                        Console.WriteLine("======================");
                    }
                    break;
                case printType.Customer:
                    Console.WriteLine("======Customers=======");
                    foreach (var customer in db.GetAllCustomers())
                    {
                        Console.WriteLine(customer);
                        Console.WriteLine("======================");
                    }
                    break;
                case printType.Parcel:
                    Console.WriteLine("=======Parcels========");
                    foreach (var parcel in db.GetAllParcels())
                    {
                        Console.WriteLine(parcel);
                        Console.WriteLine("======================");
                    }
                    break;
                case printType.UnassignedParcel:
                    Console.WriteLine("==Unassigned Parcels==");
                    foreach (var parcel in db.GetUnassignedParcels())
                    {
                        Console.WriteLine(parcel);
                        Console.WriteLine("======================");
                    }
                    break;
                case printType.AvailableStation:
                    Console.WriteLine("==Available Stations==");
                    foreach (var parcel in db.GetStationsWithFreeSlots())
                    {
                        Console.WriteLine(parcel);
                        Console.WriteLine("======================");
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// get input from user until it valid option
        /// </summary>
        /// <param name="numOptions">the num of options to choose from</param>
        /// <returns>return the  the user enter</returns>
        private static int getUserSelection(int numOptions)
        {
            string input = Console.ReadLine();
            int result;
            while (!int.TryParse(input, out result) || !isValidOption(result, numOptions)) 
            {
                Console.WriteLine("Not valid option!");
                input = Console.ReadLine();
            }
            return result;
        }
        /// <summary>
        /// check if the option is valid.
        /// </summary>
        /// <param name="choosen">the choosen option</param>
        /// <param name="numOptions">the num of options to choose of</param>
        /// <returns>true if valid option, otherwise false</returns>
        private static bool isValidOption(int choosen, int numOptions)
        {
            return choosen > 0 && choosen <= numOptions;
        }

        /// <summary>
        /// get int from user until 
        /// </summary>
        /// <returns>the int the user enter</returns>
        private static int getIntFromUser()
        {
            int myInt;
            string input = Console.ReadLine();
            while (!int.TryParse(input, out myInt))
            {
                Console.WriteLine("Not Valid Input!");
                input = Console.ReadLine();
            }
            return myInt;
        }
        /// <summary>
        /// get coords from user until its valid input
        /// </summary>
        /// <returns>the coords (lat,lng)</returns>
        private static Tuple<double, double> getCoordsFromUser()
        {
            Console.WriteLine("Enter Lattitude (as decimal): ");
            double lat;
            string input = Console.ReadLine();
            while (!double.TryParse(input, out lat)) 
            {
                Console.WriteLine("Not Valid Lattitude");
                input = Console.ReadLine();
            }

            Console.WriteLine("Enter Longitude (as decimal): ");
            double lng;
            input = Console.ReadLine();
            while (!double.TryParse(input, out lng))
            {
                Console.WriteLine("Not Valid Longitude");
                input = Console.ReadLine();
            }
            return Tuple.Create(lat, lng);
        }
        enum printType { BaseStation, Drone, Customer, Parcel, UnassignedParcel, AvailableStation};
    }
}
