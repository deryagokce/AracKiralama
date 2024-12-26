using AracKiralama.Repositories;
using AracKiralama.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AracKiralama.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly CarRepository _carRepository;
        private readonly INotyfService _notyf;
        public CategoryController(CategoryRepository categoryRepository, INotyfService notyf, CarRepository carRepository)
        {
            _categoryRepository = categoryRepository;
            _notyf = notyf;
            _carRepository = carRepository;
        }
        public IActionResult Index()
        {
            var categories = _categoryRepository.GetList();
            return View(categories);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _categoryRepository.Add(model);
            _notyf.Success("Kategori Eklendi...");
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var category = _categoryRepository.GetById(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _categoryRepository.Update(model);
            _notyf.Success("Kategori Güncellendi...");
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.GetById(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Delete(CategoryModel model)
        {
            var products = _carRepository.GetList();
            if (products.Count(c => c.CategoryId == model.Id) > 0)
            {
                _notyf.Error("Üzerinde Ürün Kayıtlı Olan Kategori Silinemez!");
                return RedirectToAction("Index");
            }
            _categoryRepository.Delete(model.Id);
            _notyf.Success("Kategori Silindi...");
            return RedirectToAction("Index");
        }
    }
}