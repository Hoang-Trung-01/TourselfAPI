using TourSefl.Repository.Repository;

namespace TourSefl.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        //CategoryRepository CategoryRepository { get; }
        PlaceRepository PlaceRepository { get; }
        PaymentRepository PaymentRepository { get; }
        BookingRepository BookingRepository { get; }
        UserRepository UserRepository { get; }
        PlanRepository PlanRepository { get; }
        RoleRepository RoleRepository { get; }
        TripRepository TripRepository { get; }
        DestinationRepository DestinationRepository { get; }
        void Save();
    }
}
