using AracKiralama.Models;

namespace AracKiralama.Repositories
{
        public class CarRepository
        {
            private readonly AppDbContext _context;
            public CarRepository(AppDbContext context)
            {
                _context = context;
            }
            public List<Car> GetList()
            {
                var cars = _context.Cars.ToList();
                return cars;
            }
            public Car GetById(int id)
            {
                var car = _context.Cars.Where(s => s.Id == id).FirstOrDefault();
                return car;
            }
            public void Add(Car model)
            {
                _context.Cars.Add(model);
                _context.SaveChanges();
            }
            public void Update(Car model)
            {
                var car = GetById(model.Id);
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
                var car = GetById(id);
                if (car != null)
                {
                    _context.Cars.Remove(car);
                    _context.SaveChanges();
                }
            }
        }
    }