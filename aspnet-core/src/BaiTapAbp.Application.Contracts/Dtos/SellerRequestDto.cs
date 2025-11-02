using System;
using System.ComponentModel.DataAnnotations;
using BaiTapAbp.Entities.Enum;
using Volo.Abp.Application.Dtos;

namespace BaiTapAbp.Dtos;

public class SellerRequestDto: EntityDto<int>
{
    public Guid UserId { get; protected set; }
    public Guid? ReviewerId  { get; protected set; }
    public RequestStatus Status { get; protected set; }
    public DateTime CreationAt { get; protected set; }
    public DateTime? ReviewAt { get; protected set; }
    [MaxLength(256)]
    public string?  Reason { get; protected set; }
}

public class CreateUpdateSellerRequestDto
{
    public Guid UserId { get; protected set; }
    public Guid? ReviewerId  { get; protected set; }
    public RequestStatus Status { get; protected set; }
    public DateTime CreationAt { get; protected set; }
    public DateTime? ReviewAt { get; protected set; }
    [MaxLength(256)]
    public string?  Reason { get; protected set; }
}
public class SellerRequestPageDto : PagedAndSortedResultRequestDto
{
    public RequestStatus? Status { get; set; }
}