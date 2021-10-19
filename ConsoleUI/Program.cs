using System;
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
            {
                exit = mainMenu();
            }
        }

        private static bool mainMenu()
        {
            Console.WriteLine("Choose one of the following:");
            Console.WriteLine("1. Add\n2. Update\n3. Get\n4. List\n5. Exit");
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
                        back = GetMenu();
                    break;
                case 4:
                    while (!back)
                        back = ListMenu();
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

        private static bool addMenu()
        {
            Console.WriteLine("What do you want to add?");
            Console.WriteLine("1. Base station\n2. Drone\n3. Customer\n4. Parcel\n5. Back");
            int userChoose = getUserSelection(5);
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

        private static void addBaseStationMenu()
        {
            Console.WriteLine("Enter station name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter lattitude (as decimal): ");
            double lat;
            string input = Console.ReadLine();
            while (!double.TryParse(input, out lat))
            {
                Console.WriteLine("Not Valid Lattitude");
                input = Console.ReadLine();
            }

            Console.WriteLine("Enter longitude (as decimal): ");
            double lng;
            input = Console.ReadLine();
            while (!double.TryParse(input, out lng))
            {
                Console.WriteLine("Not Valid Longitude");
                input = Console.ReadLine();
            }

            Console.WriteLine("How many charge slots are in the station?");
            int slots;
            input = Console.ReadLine();
            while (!int.TryParse(input, out slots))
            {
                Console.WriteLine("Not Valid Input!");
                input = Console.ReadLine();
            }
            db.AddBase(name, lat, lng, slots);
        }
        private static void addDroneMenu()
        {
            Console.WriteLine("Enter drone model: ");
            string model = Console.ReadLine();

            Console.WriteLine("Choose MaxWeight of drone:");
            Console.WriteLine("1. Light\n2. Middle\n3. Heavy");
            int maxWeightInt= getUserSelection(3) - 1;
            db.AddDrone(model, maxWeightInt);
        }
        private static void addCustomerMenu()
        {
            Console.WriteLine("Enter customer name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter customer phone:");
            string phone = Console.ReadLine();

            

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

            db.AddCustomer(name, phone, lat, lng);
        }
        private static void addParcelMenu()
        {
            Console.WriteLine("Enter sender ID:");
            int senderId;
            string input = Console.ReadLine();
            while (!int.TryParse(input, out senderId))
            {
                Console.WriteLine("Not Valid Input!");
                input = Console.ReadLine();
            }

            Console.WriteLine("Enter target ID:");
            int targetId;
            input = Console.ReadLine();
            while (!int.TryParse(input, out targetId))
            {
                Console.WriteLine("Not Valid Input!");
                input = Console.ReadLine();
            }

            Console.WriteLine("Choose parcel weight:");
            Console.WriteLine("1. Light\n2. Middle\n3. Heavy");
            int weightInt = getUserSelection(3) - 1;

            Console.WriteLine("Choose parcel priority:");
            Console.WriteLine("1. Normal\n2. Fast\n3. Urgent");
            int priorityInt = getUserSelection(3) - 1;

            db.AddParcel(senderId, targetId, weightInt, priorityInt);
        }
        private static bool updateMenu()
        {
            Console.WriteLine("What action do you want to do?");
            Console.WriteLine("1. Link parcel to drone\n2. Pick up parcel by drone\n3. Deliver parcel to customer\n4. Back");
            int userChoose = getUserSelection(4);
            switch (userChoose)
            {
                case 1:
                    linkParcelMenu();
                    break;
                case 2:
                    pickUpParcelMenu();
                    break;
                case 3:
                    DeliverParcel();
                    break;
                case 4:
                    return true;
                default:
                    Console.WriteLine("An Error Accurd!");
                    break;
            }
            return false;

        }

        private static void linkParcelMenu()
        {
            Console.WriteLine("Enter parcel ID:");
            int parcelId;
            string input = Console.ReadLine();
            while (!int.TryParse(input, out parcelId))
            {
                Console.WriteLine("Not Valid Input!");
                input = Console.ReadLine();
            }

            Console.WriteLine("Enter drone  ID:");
            int dronelId;
            input = Console.ReadLine();
            while (!int.TryParse(input, out dronelId))
            {
                Console.WriteLine("Not Valid Input!");
                input = Console.ReadLine();
            }
            db.linkParcel(parcelId, dronelId);
        }
        private static void pickUpParcelMenu()
        {
            Console.WriteLine("Enter parcel ID:");
            int parcelId;
            string input = Console.ReadLine();
            while (!int.TryParse(input, out parcelId))
            {
                Console.WriteLine("Not Valid Input!");
                input = Console.ReadLine();
            }
            db.PickParcel(parcelId);
        }
        private static void DeliverParcel()
        {
            Console.WriteLine("Enter parcel ID:");
            int parcelId;
            string input = Console.ReadLine();
            while (!int.TryParse(input, out parcelId))
            {
                Console.WriteLine("Not Valid Input!");
                input = Console.ReadLine();
            }
            db.ParcelToCustomer(parcelId);
        }


        private static bool GetMenu()
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
        private static void GetBaseStationMenu()
        {
            Console.WriteLine("Enter station ID:");
            int stationId;
            string input = Console.ReadLine();
            while (!int.TryParse(input, out stationId))
            {
                Console.WriteLine("Not Valid Input!");
                input = Console.ReadLine();
            }
            Console.WriteLine(db.GetStation(stationId));
        }
        private static void GetDroneMenu()
        {
            Console.WriteLine("Enter drone ID:");
            int droneId;
            string input = Console.ReadLine();
            while (!int.TryParse(input, out droneId))
            {
                Console.WriteLine("Not Valid Input!");
                input = Console.ReadLine();
            }
            Console.WriteLine(db.GetDrone(droneId));
        }

        private static void GetCustomerMenu()
        {
            Console.WriteLine("Enter customer ID:");
            int customerId;
            string input = Console.ReadLine();
            while (!int.TryParse(input, out customerId))
            {
                Console.WriteLine("Not Valid Input!");
                input = Console.ReadLine();
            }
            Console.WriteLine(db.GetCustomer(customerId));
        }

        private static void GetParcelMenu()
        {
            Console.WriteLine("Enter parcel ID:");
            int parcelId;
            string input = Console.ReadLine();
            while (!int.TryParse(input, out parcelId))
            {
                Console.WriteLine("Not Valid Input!");
                input = Console.ReadLine();
            }
            Console.WriteLine(db.GetParcerl(parcelId));
        }


        private static bool ListMenu()
        {
            Console.WriteLine("What  do you want to view?");
            Console.WriteLine("1. Base stations\n2. Drones\n3. Customers\n4. Parcels\n5. Back");
            int userChoose = getUserSelection(5);
            switch (userChoose)
            {
                case 1:
                    PrintAllStations();
                    break;
                case 2:
                    PrintAllDrones();
                    break;
                case 3:
                    PrintAllCustomers();
                    break;
                case 4:
                    printAllParcels();
                    break;
                case 5:
                    return true;
                default:
                    Console.WriteLine("An Error Accurd!");
                    break;
            }
            return false;

        }

        private static void PrintAllStations()
        {
            foreach (var station in db.GetAllStations())
            {
                Console.WriteLine(station);
                Console.WriteLine("======================");
            }
        }

        private static void PrintAllDrones()
        {
            foreach (var drone in db.GetAllDrones())
            {
                Console.WriteLine(drone);
                Console.WriteLine("======================");
            }
        }

        private static void PrintAllCustomers()
        {
            foreach (var customer in db.GetAllCustomers())
            {
                Console.WriteLine(customer);
                Console.WriteLine("======================");
            }
        }

        private static void printAllParcels()
        {
            foreach (var parcel in db.GetAllParcels())
            {
                Console.WriteLine(parcel);
                Console.WriteLine("======================");
            }
        }

        private static int getUserSelection(int numOptions)
        {
            string input = Console.ReadLine();
            int result;
            while (!int.TryParse(input,out result)||!isValidOption(result,numOptions))
            {
                Console.WriteLine("Not valid option!");
                input = Console.ReadLine();
            }
            return result;
        }
        private static bool isValidOption(int choosen, int numOptions)
        {
            for (int i = 1; i < numOptions + 1; i++)
            {
                if (choosen == i)
                    return true;
            }
            return false;
        }
    }
}
