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
    public int Stock { get; set; }
    public int ShopId { get; set; }
}
public class CreateUpdateProductDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [Range(0, (double)decimal.MaxValue)]
    public decimal Price { get; set; }
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
    [Required]
    public int ShopId { get; set; }
    [Required]
    public int CategoryId { get; set; }
}

public class ProductPagedRequestDto : BasePagedResultRequestDto
{
    
}