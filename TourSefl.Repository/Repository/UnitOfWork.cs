using TourSefl.Repository.Interface;

namespace TourSefl.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext context = new AppDbContext();
        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        private PlaceRepository placeRepository;
        private PaymentRepository paymentRepository;
        private BookingRepository bookingRepository;
        private UserRepository userRepository;
        private PlanRepository planRepository;
        private RoleRepository roleRepository;
        private DestinationRepository destinationRepository;
        private TripRepository tripRepository;

        public PlaceRepository PlaceRepository
        {
            get
            {
                if(placeRepository == null)
                {
                    placeRepository = new PlaceRepository(context);
                }
                return placeRepository;
            }
        }

        public PaymentRepository PaymentRepository
        {
            get
            {
                if (paymentRepository == null)
                {
                    paymentRepository = new PaymentRepository(context);
                }
                return paymentRepository;
            }
        }

        public BookingRepository BookingRepository
        {
            get
            {
                if (bookingRepository == null)
                {
                    bookingRepository = new BookingRepository(context);
                }
                return bookingRepository;
            }
        }

        public UserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public PlanRepository PlanRepository
        {
            get
            {
                if (planRepository == null)
                {
                    planRepository = new PlanRepository(context);
                }
                return planRepository;
            }
        }

        public RoleRepository RoleRepository
        {
            get
            {
                if (roleRepository == null)
                {
                    roleRepository = new RoleRepository(context);
                }
                return roleRepository;
            }
        }

        public DestinationRepository DestinationRepository
        {
            get
            {
                if (destinationRepository == null)
                {
                    destinationRepository = new DestinationRepository(context);
                }
                return destinationRepository;
            }
        }

        public TripRepository TripRepository
        {
            get
            {
                if (tripRepository == null)
                {
                    tripRepository = new TripRepository(context);
                }
                return tripRepository;
            }
        }

        //public CategoryRepository CategoryRepository
        //{
        //    get
        //    {

        //        if (categoryRepository == null)
        //        {
        //            categoryRepository = new CategoryRepository(context);
        //        }
        //        return categoryRepository;
        //    }
        //}


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}