using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace BaiTapAbp.Dtos;

public class CartDto : AuditedEntityDto<int>
{
    [Required]
    public int ProductId {get; set;}
    [Required]
    public int ShopId {get; set;}
    [Required]
    public int CustomerId {get; set;}
    [Required]
    public int Quantity {get; set;}
}

public class CreateUpdateCartDto
{
    [Required]
    public int ProductId {get; set;}
    [Required]
    public int ShopId {get; set;}
    [Required]
    public int CustomerId {get; set;}
    [Required]
    public int Quantity {get; set;}
}

public class CartPagedRequestDto : BasePagedResultRequestDto
{
    
}