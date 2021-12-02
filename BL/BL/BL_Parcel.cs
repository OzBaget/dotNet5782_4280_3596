using System;
using System.Linq;
using System.Collections.Generic;
using IBL.BO;

namespace BL
{
    public partial class BL
    {
        public void AddParcel(Parcel parcel)
        {
            
            try
            {
                DalObject.AddParcel(parcel.Sender.Id, parcel.Target.Id, (IDAL.DO.WeightCategories)parcel.Weight, (IDAL.DO.Priorities)parcel.Prioritie, DateTime.Now, null, null, null);
            }
            catch (IDAL.DO.IdAlreadyExistsException ex)
            {
                throw new IBL.BL.IdAlreadyExistsException(ex.Message, ex.Id);
            }
            catch(IDAL.DO.IdNotFoundException ex)
            {
                throw new IBL.BL.IdNotFoundException(ex.Message, ex.Id);
            }
        }
        public Parcel GetParcel(int parcelId)
        {
            try
            {
                IDAL.DO.Parcel tmpParcel = DalObject.GetParcerl(parcelId);
                Parcel newParcel = new();
                newParcel.Id = tmpParcel.Id;
                newParcel.Prioritie = (Priorities)tmpParcel.Priority;
                newParcel.Weight = (WeightCategories)tmpParcel.Weight;
                newParcel.DateCreated = tmpParcel.Requsted;
                newParcel.DateScheduled = tmpParcel.Scheduled;
                newParcel.DatePickup = tmpParcel.PickedUp;
                newParcel.DateDeliverd = tmpParcel.Delivered;

                newParcel.Target = new();
                newParcel.Target.Id = tmpParcel.TargetId;
                newParcel.Target.Name = DalObject.GetCustomer(tmpParcel.TargetId).Name;

                newParcel.Sender = new();
                newParcel.Sender.Id = tmpParcel.SenderId;
                newParcel.Sender.Name = DalObject.GetCustomer(tmpParcel.SenderId).Name;

                DroneInParcel drone=new();
                if (tmpParcel.DroneId != 0)
                {
                    drone.Id = tmpParcel.DroneId;
                    drone.Battery = Drones.Find(drone => drone.Id == tmpParcel.DroneId).Battery;
                    drone.CurrentLocation = Drones.Find(drone => drone.Id == tmpParcel.DroneId).CurrentLocation;
                }
                newParcel.Drone = drone;
                return newParcel;

            }
            catch (IDAL.DO.IdNotFoundException ex)
            {

                throw new IBL.BL.IdNotFoundException(ex.Message, ex.Id);
            }
        }

        public IEnumerable<ParcelToList> GetAllParcels()
        {
            List<ParcelToList> parcels = new();
            foreach (IDAL.DO.Parcel oldParcel in DalObject.GetAllParcels())
            {
                ParcelToList parcel = new();
                parcel.Id = oldParcel.Id;
                parcel.SenderName = GetCustomer(oldParcel.SenderId).Name;
                parcel.TargetName = GetCustomer(oldParcel.TargetId).Name;
                parcel.Priority = (Priorities)oldParcel.Priority;
                parcel.Weight = (WeightCategories)oldParcel.Weight;

                if (oldParcel.Requsted!=null)
                    parcel.Status = ParcelStatus.Created;

                if (oldParcel.Scheduled!=null)
                    parcel.Status = ParcelStatus.Scheduled;

                if (oldParcel.PickedUp!=null)
                    parcel.Status = ParcelStatus.PickUp;

                if (oldParcel.Delivered!=null)
                    parcel.Status = ParcelStatus.Deliverd;

                parcels.Add(parcel);
            }
            return parcels;
        }

        public IEnumerable<ParcelToList> GetUnassignedParcels()
        {
            return GetAllParcels().Where(parcel => parcel.Status == ParcelStatus.Created);
        }

        public int linkParcel(int droneId)
        {
            int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
            if(droneIndex==-1)
                throw new IBL.BL.IdNotFoundException($"Can't find drone with ID #{droneId}!", droneId);

            DroneToList myDrone = Drones[droneIndex];//copy by ref
            if (myDrone.Status != DroneStatus.Available)
                throw new IBL.BL.CantLinkParcelException("Drone is not available!");
            
            IEnumerable<ParcelToList> myParcels = GetUnassignedParcels();
            myParcels= myParcels.Where(parcel=>
            batteryNeedForTrip(GetCustomer(GetParcel(parcel.Id).Sender.Id).Location,myDrone.CurrentLocation)+//baterry for currnt location->sender
            batteryNeedForTrip(GetCustomer(GetParcel(parcel.Id).Target.Id).Location, GetCustomer(GetParcel(parcel.Id).Sender.Id).Location,false,parcel.Weight) +//baterry for sender->target (with weight)
            batteryNeedForTrip(getClosestStation(GetCustomer(GetParcel(parcel.Id).Sender.Id).Location).Location, GetCustomer(GetParcel(parcel.Id).Sender.Id).Location)//baterry for target->closer station to target
            <=myDrone.Battery);//remove too far parcels            
            if (myParcels.Count() == 0)
                throw new IBL.BL.CantLinkParcelException("Can't find parcel to link too!");


            myParcels = myParcels.Where(parcel => parcel.Weight <= myDrone.MaxWeight);//remove too heavy parcels
            if (myParcels.Count() == 0)
                throw new IBL.BL.CantLinkParcelException("Can't find parcel to link too!");

            
            myParcels.OrderBy(parcel => parcel.Priority); //sort by Priority
            Priorities topPriority = myParcels.Last().Priority;
            myParcels = myParcels.Where(parcel => parcel.Priority == topPriority);//remove all parcel that less pariority

            myParcels.OrderBy(parcel => parcel.Weight);//sort by Weight
            WeightCategories topWeight = myParcels.Last().Weight;
            myParcels = myParcels.Where(parcel => parcel.Weight ==topWeight);//remove all parcel that less weight

            myParcels.OrderBy(parcel => calculateDist(GetCustomer(GetParcel(parcel.Id).Sender.Id).Location, myDrone.CurrentLocation));//sort by distance
            

            myDrone.Status = DroneStatus.Delivery;
            myDrone.ParcelId = myParcels.First().Id;
            DalObject.linkParcel(myParcels.First().Id, myDrone.Id);
            return myParcels.First().Id;
        }

        public void PickParcel(int droneId)
        {
            int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
            if (droneIndex == -1)
                throw new IBL.BL.IdNotFoundException($"Can't find drone with ID #{droneId}!", droneId);
            DroneToList myDrone = Drones[droneIndex];
            if (myDrone.ParcelId == 0) 
                throw new IBL.BL.CantPickUpParcelException("Drone is not link to any parcel!");
           
            Parcel myParcel = GetParcel(myDrone.ParcelId);
            if (myParcel.DatePickup != null) 
                throw new IBL.BL.CantPickUpParcelException("Parcel alredy picked up!");

            myDrone.Battery -= batteryNeedForTrip(GetCustomer(myParcel.Sender.Id).Location, myDrone.CurrentLocation);
            myDrone.CurrentLocation = GetCustomer(myParcel.Sender.Id).Location;

            DalObject.PickParcel(myParcel.Id);

            
        }

        public void ParcelToCustomer(int droneId)
        {
            int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
            if(droneIndex==-1)
                throw new IBL.BL.IdNotFoundException($"Can't find drone with ID #{droneId}!", droneId);

            DroneToList myDrone = Drones[droneIndex];//copy by ref

            if (myDrone.ParcelId == 0)
                throw new IBL.BL.CantDeliverParcelException("Drone is not link to any parcel!");
            
            Parcel myParcel = GetParcel(myDrone.ParcelId);
            if (myParcel.DatePickup == null)
                throw new IBL.BL.CantDeliverParcelException($"The drone didn't picked up the parcel #{myParcel.Id}!");
            if(myParcel.DateDeliverd!=null)
                throw new IBL.BL.CantDeliverParcelException($"The drone deliverd parcel #{myParcel.Id} already!");

            int batteryNeededForCustomer = batteryNeedForTrip(GetCustomer(myParcel.Target.Id).Location, myDrone.CurrentLocation, false, myParcel.Weight);
            if (batteryNeededForCustomer > myDrone.Battery)
                throw new IBL.BL.CantDeliverParcelException($"Not enough battery to deliver parcel! (battry needed: {batteryNeededForCustomer}%)");

            myDrone.Battery -= batteryNeededForCustomer;
            myDrone.CurrentLocation = GetCustomer(myParcel.Target.Id).Location;
            myDrone.Status = DroneStatus.Available;
            myDrone.ParcelId = 0;
            DalObject.ParcelToCustomer(myParcel.Id);
        }
    }
}
