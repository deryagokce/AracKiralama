using AutoMapper;
using AracKiralama.Models;
using AracKiralama.ViewModels;

namespace AracKiralama.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Car, CarModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, RegisterModel>().ReverseMap();

        }
    }
}