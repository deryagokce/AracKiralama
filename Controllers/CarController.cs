using AracKiralama.Models;
using AracKiralama.Repositories;
using Microsoft.AspNetCore.Mvc;
using AracKiralama.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AracKiralama.Controllers
{
    public class CarController : Controller
       {
        private readonly CarRepository _carRepository;
        private readonly CategoryRepository _categoryRepository;
        public CarController(CarRepository carRepository, CategoryRepository categoryRepository)
        {
            _carRepository = carRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            var cars = _carRepository.GetList();
            return View(cars);
        }
        public IActionResult Add()
        {
            var categories = _categoryRepository.GetList().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Categories = categories;
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
            var categories = _categoryRepository.GetList().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Categories = categories;
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
