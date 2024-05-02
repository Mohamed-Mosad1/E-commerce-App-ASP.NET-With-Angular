using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_commerce.Core.Dtos
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class ListingCategoryDto : CategoryDto
    {
        public int Id { get; set; }
    }
    public class UpdateCategoryDto : CategoryDto
    {
        public int Id { get; set; }
    }
}
