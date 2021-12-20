﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Parcel
    {
        public Parcel(int id, int senderId, int targetId, WeightCategories weight, Priorities priority) : this()
        {
            Id = id;
            SenderId = senderId;
            TargetId = targetId;
            Weight = weight;
            Priority = priority;

            Requsted = DateTime.Now;
            DroneId = 0;
        }

        public int Id { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DateTime? Requsted { get; set; }
        public int DroneId { get; set; }
        public DateTime? Scheduled { get; set; }
        public DateTime? PickedUp { get; set; }
        public DateTime? Delivered { get; set; }

        public override string ToString()
        {
            string details =
                $"Parcel ID:         {Id}\n" +
                $"Sender ID:         {SenderId}\n" +
                $"Target ID:         {TargetId}\n" +
                $"Weight:            {Weight}\n" +
                $"Priority:          {Priority}\n" +
                $"Assigned Drone ID: {DroneId}\n" +
                $"Requsted Time:     {Requsted}";
            if (Scheduled != null) //Scheduled !=null
                details += $"\nScheduled Time:    {Scheduled}";

            if (PickedUp != null) //PickedUp !=null
                details += $"\nPickedUp Time:     {PickedUp}";

            if (Delivered != null) //Delivered !=null
                details += $"\nDelivered Time:    {Delivered}";

            return details;
        }
    }
}

