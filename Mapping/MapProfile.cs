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
        }
    }
}