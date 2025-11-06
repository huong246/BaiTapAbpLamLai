using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("Shop")]
public class ShopEntity :  FullAuditedAggregateRoot<int>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Address { get; set; } = string.Empty;
    public Guid SellerId { get; set; }
}