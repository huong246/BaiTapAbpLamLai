using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaiTapAbp.Entities.Enum;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("Order")]
public class OrderEntity : FullAuditedAggregateRoot<int>
{
    public decimal ToTalOrder{ get; set; }
    public OrderStatus Status {get;set;}
    public Guid UserId { get; set; }
    public int InvoiceId { get; set; }
    [MaxLength(200)]
    public string UserAddress { get; set; } = string.Empty;
}