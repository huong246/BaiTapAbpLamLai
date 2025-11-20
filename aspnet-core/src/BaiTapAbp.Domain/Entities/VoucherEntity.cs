using System;
using BaiTapAbp.Entities.Enum;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

public class VoucherEntity : FullAuditedAggregateRoot<int>
{
    public string? Code { get; set; }
    public decimal? MaxValue { get; set; }
    public decimal? MinSpend { get; set; }
    public int Quantity { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public decimal Value { get; set; }
    public int? ShopId { get; set; }
    public int? ItemId { get; set; }
    public Target VoucherTarget { get; set; }
    public Method VoucherMethod { get; set; }
    
}