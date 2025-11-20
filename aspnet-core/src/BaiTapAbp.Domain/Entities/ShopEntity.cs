using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaiTapAbp.Entities.Enum;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("Shop")]
public class ShopEntity : FullAuditedAggregateRoot<int>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; init; } = string.Empty;
    [MaxLength(100)]
    public string Address { get; init; } = string.Empty;
    [Required]
    public Guid SellerId { get; set; }
    public  int PrepareTime { get; init; }
    public double Rating { get; set; }
    [MaxLength(256)]
    public string? PhoneNumber { get; set; }
    
    [MaxLength(256)]
    public string? AvatarUrl { get; set; }
    
    [MaxLength(256)]
    public string? CoverUrl { get; set; }
    public ShopStatus Status { get; set; }

    public void Activate()
    {
        Status = ShopStatus.Active;
    }

    public void Ban(string reason)
    {
        Status = ShopStatus.Suspended;
    }

    public void SetVacationMode(bool isOnVacation)
    {
        if (Status == ShopStatus.Suspended)
        {
            throw new BusinessException("Shop:CannotChangeStatusWhileSuspended");
        }

        Status = isOnVacation ? ShopStatus.OnVacation : ShopStatus.Active;
    }

    public void UpdateRating(int newRating, int currentTotalReviews)
    {
        double totalScore = (Rating * currentTotalReviews) + newRating;
        Rating = Math.Round(totalScore / (currentTotalReviews + 1), 1);
    }
}