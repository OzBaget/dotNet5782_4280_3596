using BlApi;
using BO;
using System;
using System.Threading;


namespace BL
{
    internal class Simulator
    {
        private const int EMPTY = 0;
        private const double SPPED = 5000;// M/S
        private const double DELAY = 1000;// MS
        public Simulator(BL db, int droneId, Action updteDrone, Func<bool> checkStop)
        {
            Drone myDrone;
            bool droneIsWaitnigForCharging = false;
            while (!checkStop())
            {
                myDrone = db.GetDrone(droneId);
                switch (myDrone.Status)
                {
                    case DroneStatus.Available:
                        if (droneIsWaitnigForCharging)
                            try
                            {
                                db.DroneToStation(droneId);
                            }
                            catch (CantSendDroneToChargeException)
                            {
                                droneIsWaitnigForCharging = true;
                            }
                        else
                            try
                            {
                                db.linkParcel(droneId);
                            }
                            catch (CantLinkParcelException)
                            {
                                //GOTO CLOOSETS STATION//
                                droneIsWaitnigForCharging = true;
                            }
                        break;
                    case DroneStatus.UnderMaintenance:
                        if (myDrone.Battery < 100)
                        {
                            double newBattery = myDrone.Battery + (DELAY / 1000) * db.DalObject.GetChargingRate();
                            newBattery = newBattery < 100 ? newBattery : 100;
                            db.UpdateDrone(droneId, myDrone.Model, newBattery);
                        }
                        else
                        {
                            db.FreeDrone(droneId);
                            droneIsWaitnigForCharging = false;
                        }
                        break;
                    case DroneStatus.Delivery:
                        {
                            if (!myDrone.Parcel.IsInTransfer) //drone is on the way to sender
                            {
                                if (myDrone.Parcel.Distance > (DELAY / 1000) * SPPED)
                                {
                                    Location newLocation = calculateCurrnetLocation(myDrone.CurrentLocation, myDrone.Parcel.PickupLocation, (DELAY / 1000) * SPPED);
                                    double newBattery = myDrone.Battery - (DELAY / 1000) * SPPED * db.DalObject.GetPowerUse()[EMPTY];
                                    db.UpdateDrone(droneId, myDrone.Model, newBattery, newLocation);

                                }
                                else
                                {
                                    db.PickParcel(droneId);
                                }
                            }
                            else//drone is on the way to reciver
                            {
                                if (myDrone.Parcel.Distance > (DELAY / 1000) * SPPED)
                                {
                                    Location newLocation = calculateCurrnetLocation(myDrone.CurrentLocation, myDrone.Parcel.TargetLocation, (DELAY / 1000) * SPPED);
                                    double newBattery = myDrone.Battery - (DELAY / 1000) * SPPED * db.DalObject.GetPowerUse()[(int)myDrone.Parcel.Weight + 1];
                                    db.UpdateDrone(droneId, myDrone.Model, newBattery, newLocation);
                                }
                                else
                                {
                                    db.ParcelToCustomer(droneId);
                                }
                            }
                            break;
                        }
                    default:
                        break;
                }
                updteDrone();
                Thread.Sleep((int)DELAY);
            }
        }
        private Location calculateCurrnetLocation(Location startLoc, Location endLoc, double step)
        {
            double ratio = step / calculateDist(startLoc, endLoc);
            double latDelta = endLoc.Latitude - startLoc.Latitude;
            double lngDelta = endLoc.Longitude - startLoc.Longitude;
            Location newLoc = new();
            newLoc.Latitude = startLoc.Latitude + ratio * latDelta;
            newLoc.Longitude = startLoc.Longitude + ratio * lngDelta;
            return newLoc;

        }
        private double calculateDist(Location loc1, Location loc2)
        {
            const double Radios = 6371000;//meters
            //deg to radians
            double lat1 = loc1.Latitude * Math.PI / 180;
            double lat2 = loc2.Latitude * Math.PI / 180;
            double lng1 = loc1.Longitude * Math.PI / 180;
            double lng2 = loc2.Longitude * Math.PI / 180;

            //Haversine formula
            double a = Math.Pow(Math.Sin((lat2 - lat1) / 2), 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Pow(Math.Sin((lng2 - lng1) / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return Radios * c;
        }
    }
}
