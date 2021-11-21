using System;
using System.Collections.Generic;
using IBL.BO;

namespace BL
{
    public partial class BL
    {
        public void AddParcel(Parcel parcel)
        {
            parcel.DateCreated = DateTime.Now;
            parcel.DateScheduled = DateTime.MinValue;
            parcel.DatePickup = DateTime.MinValue;
            parcel.DateDeliverd = DateTime.MinValue;
            try
            {
                DalObject.AddParcel(parcel.Sender.Id, parcel.Target.Id, (IDAL.DO.WeightCategories)parcel.Weight, (IDAL.DO.Priorities)parcel.Prioritie, parcel.DateCreated, parcel.DateScheduled, parcel.DatePickup, parcel.DateDeliverd);
            }
            catch (IDAL.DO.IdAlreadyExistsException ex)
            {
                throw new IBL.BL.IdAlreadyExistsException(ex.Message, ex.Id);
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

                if (oldParcel.Requsted!=DateTime.MinValue)
                    parcel.Status = ParcelStatus.Created;

                if (oldParcel.Scheduled!=DateTime.MinValue)
                    parcel.Status = ParcelStatus.Scheduled;

                if (oldParcel.PickedUp!=DateTime.MinValue)
                    parcel.Status = ParcelStatus.PickUp;

                if (oldParcel.Delivered!=DateTime.MinValue)
                    parcel.Status = ParcelStatus.Deliverd;

                parcels.Add(parcel);
            }
            return parcels;
        }

        public IEnumerable<ParcelToList> GetUnassignedParcels()
        {
            List<ParcelToList> parcels = new();
            foreach (ParcelToList parcel in GetAllParcels())
                if (parcel.Status == ParcelStatus.Created)
                    parcels.Add(parcel);
            return parcels;
        }

        public void linkParcel(int droneId)
        {
            int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
            if(droneIndex==-1)
                throw new IBL.BL.IdNotFoundException($"Can't find drone with ID #{droneId}!", droneId);

            DroneToList myDrone = Drones[droneIndex];//copy by ref
            if (myDrone.Status != DroneStatus.Available)
                throw new IBL.BL.CantLinkParcelException("Drone is not available!");

            List<ParcelToList> myParcels = (List<ParcelToList>)GetUnassignedParcels();
            myParcels.RemoveAll(parcel=>
            batteryNeedForDest(GetCustomer(GetParcel(parcel.Id).Sender.Id).Location,myDrone.CurrentLocation)+//baterry for currnt location->sender
            batteryNeedForDest(GetCustomer(GetParcel(parcel.Id).Target.Id).Location, GetCustomer(GetParcel(parcel.Id).Sender.Id).Location,false,parcel.Weight) +//baterry for sender->target (with weight)
            batteryNeedForDest(getClosestStation(GetCustomer(GetParcel(parcel.Id).Sender.Id).Location).Location, GetCustomer(GetParcel(parcel.Id).Sender.Id).Location)//baterry for target->closer station to target
            >myDrone.Battery);//remove too far parcels            

            myParcels.RemoveAll(parcel => parcel.Weight > myDrone.MaxWeight);//remove too heavy parcels

            myParcels.Sort(delegate (ParcelToList parcel1, ParcelToList parcel2)//sort by Priority
            {
                    if (parcel1.Priority > parcel2.Priority) return 1;
                    else return -1;
            });
            myParcels.Reverse();
            myParcels.RemoveAll(parcel => parcel.Priority < myParcels[0].Priority);//remove all parcel that less pariority
            
            myParcels.Sort(delegate (ParcelToList parcel1, ParcelToList parcel2)//sort by Weight
            {
                if (parcel1.Weight > parcel2.Weight) return 1;
                else return -1;
            });
            myParcels.Reverse();
            myParcels.RemoveAll(parcel => parcel.Weight < myParcels[0].Weight);//remove all parcel that less weight

            myParcels.Sort(delegate (ParcelToList parcel1, ParcelToList parcel2)//sort by distance
            {
                if (calculateDist(GetCustomer(GetParcel(parcel1.Id).Sender.Id).Location,myDrone.CurrentLocation) >
                 calculateDist(GetCustomer(GetParcel(parcel2.Id).Sender.Id).Location, myDrone.CurrentLocation)) return 1;
                else return -1;
            });

            if (myParcels.Count == 0)
                throw new IBL.BL.CantLinkParcelException("Can't find parcel to link too!");

            
            myDrone.Status = DroneStatus.Delivery;

            DalObject.linkParcel(myParcels[0].Id, myDrone.Id);
        }

        public void PickParcel(int droneId)
        {
            int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
            if (droneIndex == -1)
                throw new IBL.BL.IdNotFoundException($"Can't find drone with ID #{droneId}!", droneId);
            DroneToList myDrone = Drones[droneIndex];
            if (myDrone.PacelId == 0) 
                throw new IBL.BL.CantPickUpParcelException("Drone is not link to any parcel!");
           
            Parcel myParcel = GetParcel(myDrone.PacelId);
            if (myParcel.DatePickup == DateTime.MinValue) 
                throw new IBL.BL.CantPickUpParcelException("Parcel alredy picked up!");

            myDrone.Battery -= batteryNeedForDest(GetCustomer(myParcel.Sender.Id).Location, myDrone.CurrentLocation);
            myDrone.CurrentLocation = GetCustomer(myParcel.Sender.Id).Location;

            DalObject.PickParcel(myParcel.Id);

            
        }

        public void ParcelToCustomer(int droneId)
        {
            int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
            if(droneIndex==-1)
                throw new IBL.BL.IdNotFoundException($"Can't find drone with ID #{droneId}!", droneId);

            DroneToList myDrone = Drones[droneIndex];//copy by ref

            if (myDrone.PacelId == 0)
                throw new IBL.BL.CantDeliverParcelException("Drone is not link to any parcel!");
            
            Parcel myParcel = GetParcel(myDrone.PacelId);
            if (myParcel.DatePickup == DateTime.MinValue)
                throw new IBL.BL.CantDeliverParcelException($"The drone didn't picked up the parcel #{myParcel.Id}!");
            if(myParcel.DateDeliverd!=DateTime.MinValue)
                throw new IBL.BL.CantDeliverParcelException($"The drone deliverd parcel #{myParcel.Id} already!");

            int batteryNeededForCustomer = batteryNeedForDest(GetCustomer(myParcel.Target.Id).Location, myDrone.CurrentLocation, false, myParcel.Weight);
            if (batteryNeededForCustomer > myDrone.Battery)
                throw new IBL.BL.CantDeliverParcelException($"Not enough battery to deliver parcel! (battry needed: {batteryNeededForCustomer}%)");

            myDrone.Battery -= batteryNeededForCustomer;
            myDrone.CurrentLocation = GetCustomer(myParcel.Target.Id).Location;
            myDrone.Status = DroneStatus.Available;
            DalObject.ParcelToCustomer(myParcel.Id);
        }
    }
}
