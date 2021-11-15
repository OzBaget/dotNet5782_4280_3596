using System;
using IBL.BL;

namespace ConsoleUI_BL
{
    class Program
    {
        static IBL.IBL db = new BL.BL();
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
            int userChoose = getUserSelection(6);
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
            Console.WriteLine("Enter station id:");
            int id = getIntFromUser();
            Tuple<double, double> location = getCoordsFromUser();

            Console.WriteLine("How many charge slots are in the station?");
            int slots = getIntFromUser();
            IBL.BO.BaseStation s = new();
            s.Id = id;
            s.Name = name;
            s.Location.Latitude = location.Item1;
            s.Location.Longitude = location.Item2;
            try
            {
                db.AddStation(s);
            }
            catch (IdAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// get drone info from the user, and add the drone.
        /// </summary>
        private static void addDroneMenu()
        {
            Console.WriteLine("Enter drone model: ");
            string model = Console.ReadLine();
            Console.WriteLine("Enter drone id:");
            int id = getIntFromUser();
            Console.WriteLine("Choose MaxWeight of drone:");
            Console.WriteLine("1. Light\n2. Middle\n3. Heavy");
            IBL.BO.WeightCategories maxWeight = (IBL.BO.WeightCategories)(getUserSelection(3) - 1);
            Console.WriteLine("Enter station id:");
            int idStation = getIntFromUser();

            IBL.BO.Drone d = new();
            d.Id = id;
            d.Modle = model;
            d.MaxWeight = maxWeight;
            try
            {
                db.AddDrone(d, idStation);
            }
            catch (IdAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// get customer info from the user, and add the customer.
        /// </summary>
        private static void addCustomerMenu()
        {
            Console.WriteLine("Enter customer name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter customer id:");
            int id = getIntFromUser();
            Console.WriteLine("Enter customer phone:");
            string phone = Console.ReadLine();

            Tuple<double, double> position = getCoordsFromUser();
            IBL.BO.Customer c = new IBL.BO.Customer();
            c.Id = id;
            c.Name = name;
            c.Phone = phone;
            c.Location.Latitude = position.Item1;
            c.Location.Longitude = position.Item2;
            try

            {
                db.AddCustomer(c);
            }
            catch (IdAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message);
            }
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
            IBL.BO.WeightCategories weight =(IBL.BO.WeightCategories) getUserSelection(3) - 1;

            Console.WriteLine("Choose parcel priority:");
            Console.WriteLine("1. Normal\n2. Fast\n3. Urgent");
            IBL.BO.Priorities priority = (IBL.BO.Priorities)getUserSelection(3) - 1;
            IBL.BO.Parcel p = new IBL.BO.Parcel();
            IBL.BO.CustomerInParcel sender = new IBL.BO.CustomerInParcel();
            sender.Id = senderId;
            IBL.BO.CustomerInParcel target = new IBL.BO.CustomerInParcel();
            target.Id = targetId;
            p.Sender = sender;
            p.Receiver = target;
            p.Prioritie = priority;
            p.Weight = weight;
            try
            {
                db.AddParcel(p);
            }
            catch (IdAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message);
            }

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

        private static void updateDroneModelMenu()
        {
            Console.WriteLine("Enter drone id:");
            int id = getIntFromUser();
            Console.WriteLine("Enter drone new model: ");
            string model = Console.ReadLine();
            try
            {
                db.UpdateDroneModel(id, model);
            }
            catch (IdNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private static void updateStationMenu()
        {
            Console.WriteLine("Enter station id:");
            int id = getIntFromUser();
            Console.WriteLine("Enter station new name: ");
            string name = Console.ReadLine();
            if (name == "")
                name = db.GetStation(id).Name;
            Console.WriteLine("Enter station new number of chargers: ");
            int numChargers;
            string input = Console.ReadLine();
            while (!int.TryParse(input, out numChargers) || input != "")
            {
                Console.WriteLine("Not Valid Input!");
                input = Console.ReadLine();
            }
            if (input == "")
                numChargers = db.GetStation(id).NumFreeChargers + db.GetStation(id).DronesInCharging.Count;
            try
            {
                db.UpdateStation(id, name, numChargers);
            }
            catch (IdNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// get drone ID form user and pick-up the parcel
        /// </summary>
        private static void sendDroneToCharge()
        {
            Console.WriteLine("Enter drone ID:");
            int droneId = getIntFromUser();
            try
            {
                db.DroneToBase(droneId);

            }
            catch (IdNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// get drone ID form user, and release the drone
        /// </summary>
        private static void releaseDroneFromCharger()
        {
            Console.WriteLine("Enter drone ID:");
            int droneId = getIntFromUser();
            Console.WriteLine("Enter how much hours the drone charged:");
            double droneTime = getDoubleFromUser();

            try
            {
                db.FreeDrone(droneId, droneTime);

            }
            catch (IdNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        /// <summary>
        /// get from the user parcel and drone IDs to link
        /// </summary>
        /// 
        private static void linkParcelMenu()
        {
            Console.WriteLine("Enter drone ID:");
            int droneId = getIntFromUser();
            try
            {
                db.linkParcel(droneId);
            }
            catch (IdNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// get parcel ID form user and pick-up the parcel
        /// </summary>
        private static void pickUpParcelMenu()
        {
            Console.WriteLine("Enter parcel ID:");
            int parcelId = getIntFromUser();
            try
            {
                db.PickParcel(parcelId);
            }
            catch (IdNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// get parcel ID form user and deliver the parcel
        /// </summary>
        private static void deliverParcel()
        {
            Console.WriteLine("Enter drone ID:");
            int droneId = getIntFromUser();
            try
            {
                db.ParcelToCustomer(droneId);
            }
            catch (IdNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
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
            try
            {
                Console.WriteLine(db.GetStation(stationId));
            }
            catch (IdNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// get drone ID and print the drone
        /// </summary>
        private static void GetDroneMenu()
        {
            Console.WriteLine("Enter drone ID:");
            int droneId = getIntFromUser();
            try
            {
                Console.WriteLine(db.GetDrone(droneId));
            }
            catch (IdNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// get customer ID and print the customer
        /// </summary>
        private static void GetCustomerMenu()
        {
            Console.WriteLine("Enter customer ID:");
            int customerId = getIntFromUser();
            try
            {
                Console.WriteLine(db.GetCustomer(customerId));
            }
            catch (IdNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// get parcel ID and print the parcel
        /// </summary>
        private static void GetParcelMenu()
        {
            Console.WriteLine("Enter parcel ID:");
            int parcelId = getIntFromUser();
            try
            {
                Console.WriteLine(db.GetParcerl(parcelId));
            }
            catch (IdNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
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
            if (userChoose == 7)//back..
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
        /// get input from user until its valid int
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
        private static double getDoubleFromUser()
        {
            double myDouble;
            string input = Console.ReadLine();
            while (!double.TryParse(input, out myDouble))
            {
                Console.WriteLine("Not Valid Input!");
                input = Console.ReadLine();
            }
            return myDouble;
        }


        /// <summary>
        /// get coords from user until its valid input
        /// </summary>
        /// <returns>the coords (lat,lng)</returns>
        private static Tuple<double, double> getCoordsFromUser()
        {
            Console.WriteLine("Enter Lattitude (as decimal): ");
            double lat = getDoubleFromUser();

            Console.WriteLine("Enter Longitude (as decimal): ");
            double lng = getDoubleFromUser();

            return Tuple.Create(lat, lng);
        }

        /// <summary>
        /// calculate Haversine dist between two coords
        /// </summary>
        /// <param name="lat1">latituse of point 1</param>
        /// <param name="lng1">longtude of point 1</param>
        /// <param name="lat2">latituse of point 2</param>
        /// <param name="lng2">longtude of point 2</param>
        /// <returns>the distance bitween the two coords in meters</returns>
        public static double CalculateDist(double lat1, double lng1, double lat2, double lng2)
        {
            const double Radios = 6371000;//meters
            //deg to radians
            lat1 = lat1 * Math.PI / 180;
            lat2 = lat2 * Math.PI / 180;
            lng1 = lng1 * Math.PI / 180;
            lng2 = lng2 * Math.PI / 180;

            //Haversine formula
            double a = Math.Pow(Math.Sin((lat2 - lat1) / 2), 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Pow(Math.Sin((lng2 - lng1) / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return Radios * c;
        }

        enum printType { BaseStation, Drone, Customer, Parcel, UnassignedParcel, AvailableStation };
    }
}
