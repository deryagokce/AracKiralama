namespace AracKiralama.Models
{
    public class Car:BaseEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
