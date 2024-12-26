using AracKiralama.Models;
using AracKiralama.Repositories;
using Microsoft.AspNetCore.Mvc;
using AracKiralama.ViewModels;

namespace AracKiralama.Controllers
{
    public class CarController : Controller
       {
        private readonly CarRepository _carRepository;
        public CarController(CarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public IActionResult Index()
        {
            var cars = _carRepository.GetList();
            return View(cars);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CarModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _carRepository.Add(model);
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var car = _carRepository.GetById(id);
            return View(car);
        }
        [HttpPost]
        public IActionResult Update(CarModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _carRepository.Update(model);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var car = _carRepository.GetById(id);
            return View(car);
        }
        [HttpPost]
        public IActionResult Delete(CarModel model)
        {
            _carRepository.Delete(model.Id);
            return RedirectToAction("Index");
        }
    }
}
