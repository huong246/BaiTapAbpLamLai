using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaiTapAbp.Entities.Enum;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;


namespace BaiTapAbp.Entities;
 [Table("AbpUsers")]
public class UserEntity : IdentityUser
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    [MaxLength(100)]
    public string Address { get; set; } = string.Empty;
 
}