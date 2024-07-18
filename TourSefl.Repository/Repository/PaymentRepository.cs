using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSefl.Repository.Repository
{
    public class PaymentRepository : GenericRepository<Payment>
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
