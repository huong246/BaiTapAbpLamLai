using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Application.Dtos;
 

namespace BaiTapAbp.Dtos;

public class ProductDto: EntityDto<int>
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public decimal? OriginalPrice { get; set; }
    public int Stock { get; set; }
    public int ShopId { get; set; }
    public int CategoryId { get; set; }
    public string? Description { get; set; }
    public string? Size { get; set; }
    public string? Color { get; set; }
}
public class CreateUpdateProductDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [Range(0, (double)decimal.MaxValue)]
    public decimal Price { get; set; }
    [Required]
    [Range(0, (double)decimal.MaxValue)]
    public decimal CostPrice { get; set; }
    [Required]
    [Range(0, (double)decimal.MaxValue)]
    public decimal? OriginalPrice { get; set; }
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
    [Required]
    public int ShopId { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [MaxLength(2000)]
    public string? Description { get; set; }
    [MaxLength(100)]
    public string? Size { get; set; }
    [MaxLength(100)]
    public string? Color { get; set; }
}

public class ProductPagedRequestDto : BasePagedResultRequestDto
{
    
}