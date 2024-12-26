using AracKiralama.Models;
using AracKiralama.ViewModels;
using AutoMapper;

namespace AracKiralama.Repositories
{
        public class CarRepository
        {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;

        public CarRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<CarModel> GetList()
            {
            var cars = _context.Cars.ToList();
            var carModels = _mapper.Map<List<CarModel>>(cars);
            return carModels;
        }
            public CarModel GetById(int id)
            {
            var car = _context.Cars.Where(s => s.Id == id).FirstOrDefault();
            var carModel = _mapper.Map<CarModel>(car);
            return carModel;
        }
            public void Add(CarModel model)
            {
            var car = _mapper.Map<Car>(model);
            _context.Cars.Add(car);
            _context.SaveChanges();
        }
        public void Update(CarModel model)
            {
                var car = _context.Cars.Where(s => s.Id == model.Id).FirstOrDefault();
            if (car != null)
                {
                    car.Id = model.Id;
                    car.Name = model.Name;
                    car.Price = model.Price;
                    car.IsActive = model.IsActive;
                _context.Cars.Update(car);
                _context.SaveChanges();
                }
            }
            public void Delete(int id)
            {
                var car = _context.Cars.Where(s => s.Id == id).FirstOrDefault();
            if (car != null)
                {
                    _context.Cars.Remove(car);
                    _context.SaveChanges();
                }
            }
        }
    }