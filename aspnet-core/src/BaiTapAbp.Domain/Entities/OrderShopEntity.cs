using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaiTapAbp.Entities.Enum;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("OrderShop")]
public class OrderShopEntity : FullAuditedEntity<int>
{
    [Required]
    [MaxLength(50)]
    public int OrderId { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountShopAmount { get; set; }
    public int? VoucherShopId { get; set; }
    public string? VoucherShopCode { get; set; }
    public decimal TotalShopAmount { get; set; }
    public OrderShopStatus Status {get; set; }
    public string? Notes { get; set; }
    public int ShopId { get; set;  }
    public decimal SubTotalShop { get; set; } //tong tien ban dau chua voucher
    public decimal ShippingFee { get; set; }
    public DateTime? DeliveredDate { get; set; }
    public string? TrackingCode { get; set; }
}