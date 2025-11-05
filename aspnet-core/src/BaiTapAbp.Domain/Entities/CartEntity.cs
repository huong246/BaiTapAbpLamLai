using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("Cart")]
public class CartEntity : FullAuditedEntity<int>
{
     [Required]
     public int CustomerId {get; set; }
     [Required]
     public int ShopId {get; set; }
     [Required]
     public int ProductId {get; set; }
     [Required]
     public int Quantity {get; set; }
     
}