using AracKiralama.Models;
using AracKiralama.Repositories;
using AracKiralama.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using AracKiralama.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AracKiralama.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly CarRepository _carRepository;
        private readonly INotyfService _notyf;
        private readonly IMapper _mapper;
        private readonly IHubContext<GeneralHub> _generalHub;

        public CategoryController(CategoryRepository categoryRepository, INotyfService notyf, CarRepository productRepository, IMapper mapper, IHubContext<GeneralHub> generalHub)
        {
            _categoryRepository = categoryRepository;
            _notyf = notyf;
            _carRepository = productRepository;
            _mapper = mapper;
            _generalHub = generalHub;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryModels = _mapper.Map<List<CategoryModel>>(categories);

            int activeCategoryCount = categories.Count(c => c.IsActive);
            await _generalHub.Clients.All.SendAsync("onCategoryAdd", activeCategoryCount);

            return View(categoryModels);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = _mapper.Map<Category>(model);
            category.Created = DateTime.Now;
            category.Updated = DateTime.Now;
            await _categoryRepository.AddAsync(category);

            int catCount = _categoryRepository.Where(c => c.IsActive == true).Count();
            await _generalHub.Clients.All.SendAsync("onCategoryAdd", catCount);
            _notyf.Success("Kategori Eklendi...");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddAjax([FromBody] CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Form verileri geçerli değil!" });
            }

            try
            {
                var category = new Category
                {
                    Name = model.Name,
                    IsActive = model.IsActive,
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                };

                await _categoryRepository.AddAsync(category);

                int catCount = _categoryRepository.Where(c => c.IsActive == true).Count();
                await _generalHub.Clients.All.SendAsync("onCategoryAdd", catCount);

                return Json(new
                {
                    success = true,
                    message = "Kategori başarıyla eklendi.",
                    category = new
                    {
                        id = category.Id,
                        name = category.Name,
                        isActive = category.IsActive,
                        created = category.Created,
                        updated = category.Updated
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Kategori eklenirken bir hata oluştu: " + ex.Message });
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var categoryModel = _mapper.Map<CategoryModel>(category);
            return View(categoryModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = await _categoryRepository.GetByIdAsync(model.Id);
            category.Name = model.Name;
            category.IsActive = model.IsActive;
            category.Updated = DateTime.Now;
            await _categoryRepository.UpdateAsync(category);

            int catCount = _categoryRepository.Where(c => c.IsActive == true).Count();
            await _generalHub.Clients.All.SendAsync("onCategoryUpdate", catCount);
            _notyf.Success("Kategori Güncellendi...");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var categoryModel = _mapper.Map<CategoryModel>(category);
            return View(categoryModel);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(CategoryModel model)
        {

            var products = await _carRepository.GetAllAsync();
            if (products.Count(c => c.CategoryId == model.Id) > 0)
            {
                _notyf.Error("Üzerinde Araba Kayıtlı Olan Kategori Silinemez!");
                return RedirectToAction("Index");
            }

            await _categoryRepository.DeleteAsync(model.Id);


            _notyf.Success("Kategori Silindi...");
            return RedirectToAction("Index");

        }
    }
}