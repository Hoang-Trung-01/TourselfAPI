using AutoMapper;
using Domain.Entities;
using TourSelfAPI.Services.Place;
using TourSelfAPI.Services.Payment;
using TourSelfAPI.Services.Booking;
using TourSelfAPI.Services.User;
using TourSelfAPI.Services.Plan;

namespace TourSelfAPI.Mapper
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            //Product
            //CreateMap<Product, Responses.ProductResponse>()
            //.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
            //.ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            //.ForMember(dest => dest.UnitsInStock, opt => opt.MapFrom(src => src.UnitsInStock))
            //.ReverseMap();
            CreateMap<Place , Services.Place.Response.PlaceResponse>().ReverseMap();
            CreateMap<Place , Services.Place.Response.GetAllPlaceResponse>().ReverseMap();

            CreateMap<Payment, Services.Payment.Response.PaymentResponse>().ReverseMap();
            CreateMap<Payment, Services.Payment.Response.GetAllPaymentResponse>().ReverseMap();

            CreateMap<Booking, Services.Booking.Response.BookingResponse>().ReverseMap();
            CreateMap<Booking, Services.Booking.Response.GetAllBookingResponse>().ReverseMap();

            CreateMap<User, Services.User.Response.UserResponse>().ReverseMap();
            CreateMap<User, Services.User.Response.GetAllUserResponse>().ReverseMap();

            CreateMap<Plan, Services.Plan.Response.PlanResponse>().ReverseMap();
            CreateMap<Plan, Services.Plan.Response.GetAllPlanResponse>().ReverseMap();

            CreateMap<Role, Services.Role.Response.RoleResponse>().ReverseMap();
            CreateMap<Role, Services.Role.Response.GetAllRoleResponse>().ReverseMap();

            CreateMap<Destination, Services.Destination.Response.DestinationResponse>().ReverseMap();
            CreateMap<Destination, Services.Destination.Response.GetAllDestinationResponse>().ReverseMap();

            CreateMap<Trip, Services.Trip.Response.TripResponse>().ReverseMap();
            CreateMap<Trip, Services.Trip.Response.GetAllTripResponse>().ReverseMap();

        }
    }
}
