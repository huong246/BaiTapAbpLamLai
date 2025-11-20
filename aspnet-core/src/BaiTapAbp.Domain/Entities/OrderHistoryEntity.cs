using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("OrderHistory")]
public class OrderHistoryEntity : FullAuditedEntity<int>
{
    public DateTime CreationAt { get; set; }
    [MaxLength(256)]
    public string? Note { get; set; }
    [MaxLength(50)]
    public int? OrderId { get; set; }
    [MaxLength(50)]
    public int? OrderShopId { get; set; }
    [MaxLength(50)]
    public int? OrderItemId { get; set; }
    public string? Status { get; set; }
}