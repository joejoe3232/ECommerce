using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "分類名稱是必填的")]
        [StringLength(50, ErrorMessage = "分類名稱不能超過50個字元")]
        public string Name { get; set; } = string.Empty;
        
        public string? Description { get; set; }
        
        public string? ImageUrl { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Navigation property
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
