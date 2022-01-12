using System;
using BO;
using BlApi;
using System.Threading;
using static BL.BL;
using System.Linq;


namespace BL
{


    class Simulator
    {
        const int EMPTY = 0;
        const int LIGHT = 1;
        const int MIDDLE = 2;
        const int HEAVY = 3;
        const double SPPED = 5000;// M/S
        const double DELAY = 500;// MS
        public Simulator(BL db, int droneId, Action updteDrone, Func<bool> checkStop)
        {
            Drone myDrone;
            double tripReminning = 0; //meters
            bool droneIsWaitnig = false;
            while (!checkStop())
            {
                myDrone = db.GetDrone(droneId);
                switch (myDrone.Status)
                {
                    case DroneStatus.Available:
                        if (droneIsWaitnig)
                            try
                            {
                                int stationId = db.DroneToStation(droneId);
                                droneIsWaitnig = false;
                                tripReminning = db.calculateDist(myDrone.CurrentLocation, db.GetStation(stationId).Location);
                            }
                            catch (CantSendDroneToChargeException)
                            {
                                droneIsWaitnig = true;
                            }
                        else
                            try
                            {
                                int parcelId = db.linkParcel(droneId);
                                tripReminning = db.calculateDist(myDrone.CurrentLocation, myDrone.Parcel.PickupLocation);
                            }
                            catch (CantLinkParcelException)
                            {
                                //GOTO CLOOSETS STATION//
                                droneIsWaitnig = true;
                            }
                        break;
                    case DroneStatus.UnderMaintenance:
                        if (tripReminning > 0)
                        {
                            double newBattery = myDrone.Battery - (DELAY / 1000) * SPPED * db.DalObject.GetPowerUse()[EMPTY];
                            db.UpdateDrone(droneId, myDrone.Model, newBattery);
                            tripReminning -= (DELAY / 1000) * SPPED;
                            tripReminning = tripReminning < 0 ? 0 : tripReminning;// dont get underflow
                        }
                        else
                        {
                            if (myDrone.Battery < 100)
                            {
                                double newBattery = myDrone.Battery + DELAY / 1000 * db.DalObject.GetChargingRate();
                                db.UpdateDrone(droneId, myDrone.Model, newBattery);
                            }
                            else
                                db.FreeDrone(droneId);
                        }
                        break;
                    case DroneStatus.Delivery:
                        {
                            if (!myDrone.Parcel.IsInTransfer) //drone is on the way to sender
                            {
                                if (tripReminning > 0)
                                {
                                    double newBattery = myDrone.Battery - (DELAY / 1000) * SPPED * db.DalObject.GetPowerUse()[EMPTY];
                                    db.UpdateDrone(droneId, myDrone.Model, newBattery);
                                    tripReminning -= (DELAY / 1000) * SPPED;
                                    tripReminning = tripReminning < 0 ? 0 : tripReminning;// dont get underflow
                                }
                                else
                                {
                                    db.PickParcel(droneId);
                                    tripReminning = db.calculateDist(myDrone.CurrentLocation, myDrone.Parcel.TargetLocation);
                                }

                            }
                            else//drone is on the way to reciver
                            {
                                if (tripReminning > 0)
                                {
                                    double newBattery = myDrone.Battery - (DELAY / 1000) * SPPED * db.DalObject.GetPowerUse()[(int)myDrone.Parcel.Weight + 1];
                                    db.UpdateDrone(droneId, myDrone.Model, newBattery);
                                    tripReminning -= (DELAY / 1000) * SPPED;
                                    tripReminning = tripReminning < 0 ? 0 : tripReminning;// dont get underflow
                                }
                                else
                                {
                                    db.ParcelToCustomer(droneId);
                                    updteDrone();
                                }
                            }

                            break;
                        }
                    default:
                        break;
                }
                
            }
        }
    }
}
