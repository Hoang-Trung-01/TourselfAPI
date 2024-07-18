using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSefl.Repository.Repository
{
    public class PlanRepository : GenericRepository<Plan>
    {
        public PlanRepository(AppDbContext context) : base(context)
        {
        }
    }
}
