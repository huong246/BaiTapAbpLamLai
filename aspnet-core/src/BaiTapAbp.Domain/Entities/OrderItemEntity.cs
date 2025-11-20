using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("OrderItem")]
public class OrderItemEntity : Entity<int>
{
    //dai dien cho 1 mat hang trong 1 don hang thuoc 1 shop
    [Required]
    [MaxLength(50)]
    public int ProductId { get; set; }
 /*   [MaxLength(50)]
    public int OrderId { get; set; }*/
    [MaxLength(50)]
    public int OrderShopId { get; set; }
    public int Quantity { get; set; }
    public int CustomerId { get; set; }
    public decimal Price { get; set; }
    public decimal TotalAmount { get; set; }
    
}