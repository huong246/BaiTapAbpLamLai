using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("Category")]
public class CategoryEntity :  FullAuditedAggregateRoot<int>
{
    [MaxLength(100)]
    [Required]
    public string Name { get; set; } = string.Empty;
    
}