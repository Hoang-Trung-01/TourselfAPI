using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSefl.Repository.Repository
{
    public class BookingRepository : GenericRepository<Booking>
    {
        public BookingRepository(AppDbContext context) : base(context)
        {
        }
    }
}