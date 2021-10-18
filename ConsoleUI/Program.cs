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
                exit = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.WriteLine("Choose one of the following:");
            Console.WriteLine("1. Add\n2. Update\n3. Get\n4. List\n5. Exit");
            int userChoose = getUserSelection(5);
            bool back = false;
            switch (userChoose)
            {
                case 1:
                    while (!back)
                        back = AddMenu();
                    break;
                case 2:
                    UpdateMenu();
                    break;
                case 3:
                    GetMenu();
                    break;
                case 4:
                    ListMenu();
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

        private static bool AddMenu()
        {
            Console.WriteLine("What do you want to add?");
            Console.WriteLine("1. Base station\n2. Drone\n3. Costumer\n4. Parcel\n5. Back");
            int userChoose = getUserSelection(5);
            switch (userChoose)
            {
                case 1:
                    AddBaseStationMenu();
                    break;
                case 2:
                    AddDroneMenu();
                    break;
                case 3:
                    AddCostumerMenu();
                    break;
                case 4:
                    AddParcelMenu();
                    break;
                case 5:
                    return true;
                default:
                    Console.WriteLine("An Error Accurd!");
                    break;
            }
            return false;
        }

        private static void AddBaseStationMenu()
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
            db.AddBase(new IDAL.DO.Station(name, lat, lng, slots));
        }
        private static void AddDroneMenu()
        {
            Console.WriteLine("Enter drone model: ");
            string model = Console.ReadLine();

            Console.WriteLine("Choose MaxWeight of drone:");
            Console.WriteLine("1. Light\n2. Middle\n3. Heavy");
            IDAL.DO.WeightCategories maxWeight= (IDAL.DO.WeightCategories)getUserSelection(3) - 1;
            db.AddDrone(new IDAL.DO.Drone(model,maxWeight));
        }
        private static void AddCostumerMenu()
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

            db.AddCustomer(new IDAL.DO.Customer(name, phone, lat, lng));
        }
        private static void AddParcelMenu()
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
            IDAL.DO.WeightCategories weight = (IDAL.DO.WeightCategories)getUserSelection(3) - 1;

            Console.WriteLine("Choose parcel priority:");
            Console.WriteLine("1. Normal\n2. Fast\n3. Urgent");
            IDAL.DO.Priorities priority = (IDAL.DO.Priorities)getUserSelection(3) - 1;

            db.AddParcel(new IDAL.DO.Parcel(senderId, targetId, weight, priority));
        }
        private static void UpdateMenu()
        {

        }
        private static void GetMenu()
        {

        }
        private static void ListMenu()
        {

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
