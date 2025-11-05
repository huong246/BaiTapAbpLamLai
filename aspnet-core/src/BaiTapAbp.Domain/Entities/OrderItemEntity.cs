using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("OrderItem")]
public class OrderItemEntity : FullAuditedEntity<int>
{
    //dai dien cho 1 mat hang trong 1 don hang thuoc 1 shop
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int OrderShopId { get; set; }
    
    public int Quantity { get; set; }
    public int CustomerId { get; set; }
    public decimal Price { get; set; }
    public decimal TotalAmount { get; set; }
    
}