using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaiTapAbp.Entities.Enum;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("Order")]
public class OrderEntity : FullAuditedAggregateRoot<int>
{
    [Column(TypeName = "decimal(18,2)")]
    public decimal ToTalOrder{ get; set; }
    public OrderStatus Status {get;set;}
    [Required]
    public Guid UserId { get; set; }
    public int InvoiceId { get; set; }
    [MaxLength(200)]
    public string UserAddress { get; set; } = string.Empty;
}