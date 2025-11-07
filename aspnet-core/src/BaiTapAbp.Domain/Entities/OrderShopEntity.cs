using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;
[Table("OrderShop")]
public class OrderShopEntity : FullAuditedEntity<int>
{
    public int OrderId { get; set; }
    [MaxLength(256)] public string Note { get; set; } = string.Empty;
    public int ShopId { get; set; }
    public DateTime DeliveredDate { get; set; }
    public decimal ToTalShop { get; set; }
    
}