﻿using System;
using System.Linq;
using System.Collections.Generic;
using BO;
using BlApi;

namespace BL
{
    sealed partial class BL : IBL
    {
        public void AddParcel(Parcel parcel)
        {
            
            try
            {
                DalObject.AddParcel(parcel.Sender.Id, parcel.Target.Id, (DO.WeightCategories)parcel.Weight, (DO.Priorities)parcel.Prioritie, DateTime.Now, null, null, null);
            }
            catch (DO.IdAlreadyExistsException ex)
            {
                throw new IdAlreadyExistsException(ex.Message, ex.Id);
            }
            catch(DO.IdNotFoundException ex)
            {
                throw new IdNotFoundException(ex.Message, ex.Id);
            }
        }
        public Parcel GetParcel(int parcelId)
        {
            try
            {
                DO.Parcel tmpParcel = DalObject.GetParcerl(parcelId);
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
            catch (DO.IdNotFoundException ex)
            {

                throw new IdNotFoundException(ex.Message, ex.Id);
            }
        }

        public IEnumerable<ParcelToList> GetAllParcels()
        {
            List<ParcelToList> parcels = new();
            foreach (DO.Parcel oldParcel in DalObject.GetAllParcels())
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
            List<ParcelToList> filterdParcel = new();
            foreach (DO.Parcel oldParcel in DalObject.GetFilterdParcels(parcel => parcel.Scheduled == null))
            {
                ParcelToList newParcel = new();
                newParcel.Id = oldParcel.Id;
                newParcel.Priority = (Priorities)oldParcel.Priority;
                newParcel.Weight = (WeightCategories)oldParcel.Weight;
                newParcel.SenderName = DalObject.GetCustomer(oldParcel.SenderId).Name;
                newParcel.TargetName = DalObject.GetCustomer(oldParcel.TargetId).Name;
                newParcel.Status = ParcelStatus.Created;
                if (oldParcel.Scheduled != null)
                    newParcel.Status = ParcelStatus.Scheduled;
                if (oldParcel.PickedUp != null)
                    newParcel.Status = ParcelStatus.PickUp;
                if (oldParcel.Delivered != null)
                    newParcel.Status = ParcelStatus.Deliverd;
                filterdParcel.Add(newParcel);
            }
            return filterdParcel;

            
        }
        IEnumerable<DroneToList> IBL.GetFilterdParcels(DateTime startDate, DateTime endDate, ParcelStatus status)
        {
            throw new NotImplementedException();
        }

        public int linkParcel(int droneId)
        {
            int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
            if(droneIndex==-1)
                throw new IdNotFoundException($"Can't find drone with ID #{droneId}!", droneId);

            DroneToList myDrone = Drones[droneIndex];//copy by ref
            if (myDrone.Status != DroneStatus.Available)
                throw new CantLinkParcelException("Drone is not available!");
            
            IEnumerable<ParcelToList> myParcels = GetUnassignedParcels();
            myParcels= myParcels.Where(parcel=>
            batteryNeedForTrip(GetCustomer(GetParcel(parcel.Id).Sender.Id).Location,myDrone.CurrentLocation)+//baterry for currnt location->sender
            batteryNeedForTrip(GetCustomer(GetParcel(parcel.Id).Target.Id).Location, GetCustomer(GetParcel(parcel.Id).Sender.Id).Location,false,parcel.Weight) +//baterry for sender->target (with weight)
            batteryNeedForTrip(getClosestStation(GetCustomer(GetParcel(parcel.Id).Sender.Id).Location).Location, GetCustomer(GetParcel(parcel.Id).Sender.Id).Location)//baterry for target->closer station to target
            <=myDrone.Battery);//remove too far parcels            
            if (myParcels.Count() == 0)
                throw new CantLinkParcelException("Can't find parcel to link too!");


            myParcels = myParcels.Where(parcel => parcel.Weight <= myDrone.MaxWeight);//remove too heavy parcels
            if (myParcels.Count() == 0)
                throw new CantLinkParcelException("Can't find parcel to link too!");

            
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
                throw new IdNotFoundException($"Can't find drone with ID #{droneId}!", droneId);
            DroneToList myDrone = Drones[droneIndex];
            if (myDrone.ParcelId == 0) 
                throw new CantPickUpParcelException("Drone is not link to any parcel!");
           
            Parcel myParcel = GetParcel(myDrone.ParcelId);
            if (myParcel.DatePickup != null) 
                throw new CantPickUpParcelException("Parcel alredy picked up!");

            myDrone.Battery -= batteryNeedForTrip(GetCustomer(myParcel.Sender.Id).Location, myDrone.CurrentLocation);
            myDrone.CurrentLocation = GetCustomer(myParcel.Sender.Id).Location;

            DalObject.PickParcel(myParcel.Id);

            
        }

        public void ParcelToCustomer(int droneId)
        {
            int droneIndex = Drones.FindIndex(drone => drone.Id == droneId);
            if(droneIndex==-1)
                throw new IdNotFoundException($"Can't find drone with ID #{droneId}!", droneId);

            DroneToList myDrone = Drones[droneIndex];//copy by ref

            if (myDrone.ParcelId == 0)
                throw new CantDeliverParcelException("Drone is not link to any parcel!");
            
            Parcel myParcel = GetParcel(myDrone.ParcelId);
            if (myParcel.DatePickup == null)
                throw new CantDeliverParcelException($"The drone didn't picked up the parcel #{myParcel.Id}!");
            if(myParcel.DateDeliverd!=null)
                throw new CantDeliverParcelException($"The drone deliverd parcel #{myParcel.Id} already!");

            int batteryNeededForCustomer = batteryNeedForTrip(GetCustomer(myParcel.Target.Id).Location, myDrone.CurrentLocation, false, myParcel.Weight);
            if (batteryNeededForCustomer > myDrone.Battery)
                throw new CantDeliverParcelException($"Not enough battery to deliver parcel! (battry needed: {batteryNeededForCustomer}%)");

            myDrone.Battery -= batteryNeededForCustomer;
            myDrone.CurrentLocation = GetCustomer(myParcel.Target.Id).Location;
            myDrone.Status = DroneStatus.Available;
            myDrone.ParcelId = 0;
            DalObject.ParcelToCustomer(myParcel.Id);
        }

        
        
    }
}
