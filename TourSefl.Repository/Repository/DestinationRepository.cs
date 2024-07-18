using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSefl.Repository.Repository
{
    public class DestinationRepository : GenericRepository<Destination>
    {
        public DestinationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
