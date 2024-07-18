using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TourSefl.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewDetail> ReviewDetails { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1 user has 1 role, 1 role has many users
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // 1 user has many bookings
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);

            // 1 booking has 1 payment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BookingId);

            // 1 booking has 1 plan
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Plan)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PlanId);

            // 1 plan has many trips
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Plan)
                .WithMany(p => p.Trips)
                .HasForeignKey(t => t.Id);

            // 1 trip has many places
            modelBuilder.Entity<Place>()
                .HasOne(pl => pl.Trip)
                .WithMany(t => t.Places)
                .HasForeignKey(pl => pl.TripId);

            // 1 place has many destinations
            modelBuilder.Entity<Destination>()
                .HasOne(d => d.Place)
                .WithMany(pl => pl.Destinations)
                .HasForeignKey(d => d.PlaceId);
        }
    }

}
