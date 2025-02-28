﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Plan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationDate { get; set; }
        public double TotalCost { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TripId { get; set; }
        public int PlaceId { get; set; }

        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Trip> Trips { get; set; }
    }
}
