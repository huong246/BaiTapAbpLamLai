using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace BaiTapAbp.Dtos;

public class CategoryDto :EntityDto<int>
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}

public class CreateUpdateCategoryDto
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}

public class CategoryPagedRequestDto : BasePagedResultRequestDto
{
    
}