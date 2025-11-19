using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("ProductImage")]
public class ProductImageEntity : FullAuditedEntity<int>
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    public string? ImageUrl { get; set; }
    
}