using DO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dal
{
    internal sealed partial class DalXml : DalApi.IDal
    {

        public Parcel GetParcerl(int parcelId)
        {
            bool parcelExists = false;
            List<Parcel> myList = loadXmlToList<Parcel>();
            foreach (Parcel parcel in myList)
                if (parcel.Id == parcelId && parcel.IsActived)
                    parcelExists = true;

            if (!parcelExists)
                throw new IdNotFoundException($"Can't find parcel with ID #{parcelId}", parcelId);

            return myList.Find(parcel => parcel.Id == parcelId);
        }


        public void AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority, DateTime? requsted, DateTime? scheduled, DateTime? pickedUp, DateTime? delivered)
        {
            bool customerExists = false;
            List<Customer> myList = loadXmlToList<Customer>();
            foreach (Customer customer in myList)
                if (customer.Id == senderId)
                    customerExists = true;
            if (!customerExists)
                throw new IdNotFoundException($"Can't find sender (#{senderId}", senderId);

            customerExists = false;
            foreach (Customer customer in myList)
                if (customer.Id == targetId)
                    customerExists = true;
            if (!customerExists)
                throw new IdNotFoundException($"Can't find target (#{targetId}", targetId);


            Parcel myParcel = new();
            myParcel.Id = Config.ParcelId;
            myParcel.SenderId = senderId;
            myParcel.TargetId = targetId;
            myParcel.Weight = weight;
            myParcel.Priority = priority;
            myParcel.Requsted = requsted;
            myParcel.Scheduled = scheduled;
            myParcel.PickedUp = pickedUp;
            myParcel.Delivered = delivered;
            myParcel.IsActived = true;

            List<Parcel> myParcelsList = loadXmlToList<Parcel>();
            myParcelsList.Add(myParcel);
            saveListToXml(myParcelsList);
        }

        public void DeleteParcel(int parcelId)
        {
            Parcel myParcel = GetParcerl(parcelId);
            List<Parcel> myList = loadXmlToList<Parcel>();
            int parcelIndex = myList.IndexOf(myParcel);
            myParcel.IsActived = false;
            myList[parcelIndex] = myParcel;
            saveListToXml(myList);
        }


        public void linkParcel(int parcelId, int droneId)
        {
            List<Parcel> myList = loadXmlToList<Parcel>();
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = myList.IndexOf(parcelTmp);
            parcelTmp.DroneId = droneId;
            parcelTmp.Scheduled = DateTime.Now;
            myList[index] = parcelTmp;
            saveListToXml(myList);
        }


        public void PickParcel(int parcelId)
        {
            List<Parcel> myList = loadXmlToList<Parcel>();

            Parcel parcelTmp = GetParcerl(parcelId);
            int index = myList.IndexOf(parcelTmp);
            parcelTmp.PickedUp = DateTime.Now;
            myList[index] = parcelTmp;
            saveListToXml(myList);
        }


        public void ParcelToCustomer(int parcelId)
        {
            List<Parcel> myParcelList = loadXmlToList<Parcel>();
            Parcel parcelTmp = GetParcerl(parcelId);
            int index = myParcelList.IndexOf(parcelTmp);
            parcelTmp.Delivered = DateTime.Now;
            int droneId = parcelTmp.DroneId;
            parcelTmp.DroneId = 0;
            myParcelList[index] = parcelTmp;
            saveListToXml(myParcelList);

            List<Drone> myDroneList = loadXmlToList<Drone>();
            Drone droneTmp = GetDrone(droneId);
            index = myDroneList.IndexOf(droneTmp);
            myDroneList[index] = droneTmp;
            saveListToXml(myDroneList);
        }


        public IEnumerable<Parcel> GetAllParcels()
        {
            return loadXmlToList<Parcel>().Where(p => p.IsActived);
        }

        public IEnumerable<Parcel> GetFilterdParcels(Predicate<Parcel> filter)
        {
            return loadXmlToList<Parcel>().Where(p => filter(p) && p.IsActived);
        }
    }
}
