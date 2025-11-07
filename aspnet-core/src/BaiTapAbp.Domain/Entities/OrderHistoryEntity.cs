using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaiTapAbp.Entities.Enum;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("OrderHistory")]
public class OrderHistoryEntity : FullAuditedEntity<int>
{
    [MaxLength(256)]
    public string Name { get; set; } = string.Empty;
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}