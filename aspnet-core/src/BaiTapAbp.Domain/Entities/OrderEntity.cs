using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaiTapAbp.Entities.Enum;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("Order")]
public class OrderEntity : FullAuditedAggregateRoot<int>
{
    [Required]
    public Guid UserId { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalShippingFee { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalSubtotal { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountProductAmount { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountShippingAmount { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime OrderDate { get; set; } 
    [MaxLength(50)]
    public int VoucherProductId { get; set; }
    [MaxLength(50)]
    public int VoucherShippingId { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverPhone { get; set; }
    public string ShippingAddress { get; set; } 
}