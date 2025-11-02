using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaiTapAbp.Entities.Enum;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;
[Table("SellerRequest")]
public class SellerRequestEntity : FullAuditedEntity<int>
{
    public Guid UserId { get; protected set; }
    public Guid? ReviewerId  { get; protected set; }
    public RequestStatus Status { get; protected set; }
    public DateTime CreationAt { get; protected set; }
    public DateTime? ReviewAt { get; protected set; }
    [MaxLength(256)]
    public string?  Reason { get; protected set; }
   
    protected SellerRequestEntity() { }

    public SellerRequestEntity(Guid userId)
    {
        UserId = userId;
        Status = RequestStatus.Pending;
        CreationAt = DateTime.Now;
    }
    public void Approve(Guid adminUserId)
    {
        Status = RequestStatus.Approved;
        ReviewerId = adminUserId;
        ReviewAt = DateTime.Now;
    }
    public void Reject(Guid adminUserId, string reason)
    {
        Status = RequestStatus.Rejected;
        ReviewerId = adminUserId;
        ReviewAt = DateTime.Now;
        Reason = reason;
    }
}