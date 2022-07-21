using System.ComponentModel.DataAnnotations;

namespace Taller.CarModel.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        public int Year { get; set; }
        public int Doors { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
    }
}
