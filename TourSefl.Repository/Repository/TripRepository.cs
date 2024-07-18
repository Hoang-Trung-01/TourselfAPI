using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSefl.Repository.Repository
{
    public class TripRepository : GenericRepository<Trip>
    {
        public TripRepository(AppDbContext context) : base(context)
        {
        }
    }
}
