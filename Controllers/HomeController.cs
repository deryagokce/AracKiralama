﻿using AracKiralama.Models;
using AracKiralama.Repositories;
using AracKiralama.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using NETCore.Encrypt.Extensions;
using System.Diagnostics;
using System.Security.Claims;

namespace AracKiralama.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CarRepository _carRepository;
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly INotyfService _notyf;
        private readonly IFileProvider _fileProvider;

        public HomeController(ILogger<HomeController> logger, CarRepository carRepository, IMapper mapper, UserRepository userRepository, IConfiguration config, INotyfService notyf, IFileProvider fileProvider)
        {
            _logger = logger;
            _carRepository = carRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _config = config;
            _notyf = notyf;
            _fileProvider = fileProvider;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carRepository.GetAllAsync();
            cars = cars.Where(s => s.IsActive == true).ToList();
            var carModels = _mapper.Map<List<CarModel>>(cars);
            return View(carModels);
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var hashedpass = MD5Hash(model.Password);
            var user = _userRepository.Where(s => s.UserName == model.UserName && s.Password == hashedpass).SingleOrDefault();

            if (user == null)
            {
                _notyf.Error("Kullanıcı Adı veya Parola Geçersizdir!");
                return View();
            }

            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim("UserName",user.UserName),
                new Claim("PhotoUrl",user.PhotoUrl),
                new Claim("Email",user.Email),
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = model.KeepMe
            };
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (_userRepository.Where(s => s.UserName == model.UserName).Count() > 0)
            {
                _notyf.Error("Girilen Kullanıcı Adı Kayıtlıdır!");
                return View(model);
            }
            if (_userRepository.Where(s => s.Email == model.Email).Count() > 0)
            {
                _notyf.Error("Girilen E-Posta Adresi Kayıtlıdır!");
                return View(model);
            }

            var rootFolder = _fileProvider.GetDirectoryContents("wwwroot");
            var photoUrl = "no-img.png";
            if (model.PhotoFile != null)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(model.PhotoFile.FileName);
                var photoPath = Path.Combine(rootFolder.First(x => x.Name == "userPhotos").PhysicalPath, filename);
                using var stream = new FileStream(photoPath, FileMode.Create);
                model.PhotoFile.CopyTo(stream);
                photoUrl = filename;
            }

            var hashedpass = MD5Hash(model.Password);
            var user = new User();
            user.FullName = model.FullName;
            user.UserName = model.UserName;
            user.Password = hashedpass;
            user.Email = model.Email;
            user.PhotoUrl = photoUrl;
            user.Role = "Uye";
            await _userRepository.AddAsync(user);

            _notyf.Success("Üye Kaydı Yapılmıştır. Oturum Açınız");

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public string MD5Hash(string pass)
        {
            var salt = _config.GetValue<string>("AppSettings:MD5Salt");
            var password = pass + salt;
            var hashed = password.MD5();
            return hashed;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}