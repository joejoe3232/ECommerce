using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        [Required]
        public string OrderNumber { get; set; } = string.Empty;
        
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;
        
        [Required]
        public string CustomerPhone { get; set; } = string.Empty;
        
        [Required]
        public string ShippingAddress { get; set; } = string.Empty;
        
        public string? Notes { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        public DateTime? ShippedDate { get; set; }
        
        public DateTime? DeliveredDate { get; set; }
        
        // Navigation property
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
    
    public enum OrderStatus
    {
        Pending,        // 待處理
        Processing,     // 處理中
        Shipped,        // 已出貨
        Delivered,      // 已送達
        Cancelled       // 已取消
    }
}
