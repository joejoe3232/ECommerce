using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "產品名稱是必填的")]
        [StringLength(100, ErrorMessage = "產品名稱不能超過100個字元")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "產品描述是必填的")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "價格是必填的")]
        [Range(0.01, double.MaxValue, ErrorMessage = "價格必須大於0")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "庫存數量是必填的")]
        [Range(0, int.MaxValue, ErrorMessage = "庫存數量不能為負數")]
        public int StockQuantity { get; set; }
        
        public string? ImageUrl { get; set; }
        
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
