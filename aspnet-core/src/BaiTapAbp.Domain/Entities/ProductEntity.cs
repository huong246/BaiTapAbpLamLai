using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("Product")]
public class ProductEntity : FullAuditedEntity<int>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int ShopId {get; set;}
}