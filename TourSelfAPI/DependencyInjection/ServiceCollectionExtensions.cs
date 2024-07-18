
using Microsoft.EntityFrameworkCore;
using TourSefl.Repository;
using TourSefl.Repository.Interface;
using TourSefl.Repository.Repository;
using TourSelfAPI.Mapper;


namespace TourSelfAPI.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigRepository(this IServiceCollection services)
            => services.AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient(typeof(PlaceRepository))
                .AddTransient(typeof(PaymentRepository))
                .AddTransient(typeof(BookingRepository))
                .AddTransient(typeof(UserRepository))
                .AddTransient(typeof(PlanRepository))
                .AddTransient(typeof(RoleRepository))
                .AddTransient(typeof(DestinationRepository))
                .AddTransient(typeof(TripRepository))
            ;

        public static IServiceCollection AddConfigDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options
                => options.UseSqlServer(configuration.GetConnectionString("ConnectionStrings")));
            return services;
        }

        public static IServiceCollection AddConfigurationMapper(this IServiceCollection services)
        => services.AddAutoMapper(typeof(ServiceProfile));
    }
}
