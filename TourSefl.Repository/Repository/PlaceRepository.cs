using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSefl.Repository.Repository
{
    public class PlaceRepository : GenericRepository<Place>
    {
        public PlaceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
