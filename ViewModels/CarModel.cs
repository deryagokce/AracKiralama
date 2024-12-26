using System.ComponentModel.DataAnnotations;

namespace AracKiralama.ViewModels
{
    public class CarModel
    {
        public int Id { get; set; }
        [Display(Name = "Araba Adı")]
        [Required(ErrorMessage = "Araba Adı Giriniz!")]
        public string Name { get; set; }
        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "Fiyat Giriniz!")]
        public int Price { get; set; }
        [Display(Name = "Mevcut")]
        public bool IsActive { get; set; }
        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Kategori Giriniz!")]
        public int CategoryId { get; set; }
    }
}
