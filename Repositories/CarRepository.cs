using AracKiralama.Models;
using AracKiralama.ViewModels;

namespace AracKiralama.Repositories
{
        public class CarRepository
        {
            private readonly AppDbContext _context;
            public CarRepository(AppDbContext context)
            {
                _context = context;
            }
            public List<CarModel> GetList()
            {
                var cars = _context.Cars.Select(x => new CarModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    IsActive = x.IsActive
                }).ToList();
            return cars;
            }
            public CarModel GetById(int id)
            {
            var car = _context.Cars.Where(s => s.Id == id).Select(x => new CarModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                IsActive = x.IsActive
            }).FirstOrDefault(); 
            return car;
        }
            public void Add(CarModel model)
            {
            var car = new Car()
            {
                Name = model.Name,
                Price = model.Price,
                IsActive = model.IsActive,
            };
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