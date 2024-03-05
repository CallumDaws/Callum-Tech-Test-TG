using System.ComponentModel.DataAnnotations;

namespace Callum_Tech_Test_TG.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name must be less than or equal to 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(0, 110, ErrorMessage = "Age must be between 0 and 110")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Post code is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d\s]+$", ErrorMessage = "Post code must contain numbers and characters")]
        public string PostCode { get; set; }

        [Range(0, 2.50, ErrorMessage = "Height must be between 0 and 2.50 meters")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Height must only have up to 2 decimal places")]
        public double Height { get; set; }
    }
}
