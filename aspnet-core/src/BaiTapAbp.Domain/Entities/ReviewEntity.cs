using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;
[Table("Review")]
public class ReviewEntity : FullAuditedAggregateRoot<int>
{
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int CustomerId { get; set; }
    public int Rating { get; set; }
    [MaxLength(2000)]
    public string? Comment { get; set; }
    public DateTime ReviewAt { get; set; }
     
}