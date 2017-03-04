using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class Fruit
    {
        [Display(Name = "Fruit Id")]
        public int Id { get; set; }
        [Display(Name = "Fruit Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Image")]
        public string Image { get; set; }
    }
}