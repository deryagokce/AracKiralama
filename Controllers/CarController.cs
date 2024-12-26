using AracKiralama.Models;
using AracKiralama.Repositories;
using Microsoft.AspNetCore.Mvc;
using AracKiralama.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace AracKiralama.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarController : Controller
    {
        private readonly CarRepository _carRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;

        public CarController(CarRepository carRepository, CategoryRepository categoryRepository, IMapper mapper, INotyfService notyf)
        {
            _carRepository = carRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carRepository.GetAllAsync();
            var carModels = _mapper.Map<List<CarModel>>(cars);
            return View(carModels);
        }

        public async Task<IActionResult> Add()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoriesSelectList = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Categories = categoriesSelectList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var car = _mapper.Map<Car>(model);
            car.Created = DateTime.Now;
            car.Updated = DateTime.Now;
            await _carRepository.AddAsync(car);
            _notyf.Success("Araba Eklendi...");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoriesSelectList = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Categories = categoriesSelectList;
            var car = await _carRepository.GetByIdAsync(id);
            var carModel = _mapper.Map<CarModel>(car);
            return View(carModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var car = await _carRepository.GetByIdAsync(model.Id);
            car.Name = model.Name;
            car.IsActive = model.IsActive;
            car.CategoryId = model.CategoryId;
            car.Updated = DateTime.Now;
            await _carRepository.UpdateAsync(car);
            _notyf.Success("Araba Güncellendi...");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            var carModel = _mapper.Map<CarModel>(car);
            return View(carModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CarModel model)
        {
            await _carRepository.DeleteAsync(model.Id);
            _notyf.Success("Araba Silindi...");
            return RedirectToAction("Index");
        }
    }
}