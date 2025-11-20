using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("Product")]
public class ProductEntity : FullAuditedAggregateRoot<int>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; } //gia ban hien tai
    [Column(TypeName = "decimal(18,2)")] 
    private decimal? OriginalPrice { get; set; } //gia goc dung de hien thi trong giam gia (gach bo)
    [Column(TypeName = "decimal(18,2)")]
    public decimal CostPrice { get; private set; }  //gia von nhap hang (tinh loi nhuan)
    public int Stock { get; set; }
    [Required]
    public int ShopId {get; init;}
    [Required]
    public int CategoryId {get;init;}
    [MaxLength(2000)]
    public string? Description { get; init; }
    public int SaleCount { get; set; }
    [MaxLength(100)]
    public string? Size {get; init;}
    [MaxLength(100)]
    public string? Color {get; init;}
    public double RatingAverage { get; private set; }
    public bool IsPublished { get; private set; }
    [MaxLength(100)]
    public string? ThumbnailUrl { get; private set; }
   
    
    private ProductEntity() { }

    public ProductEntity(
        int shopId,
        int categoryId,
        string name,
        decimal price,
        decimal costPrice,
        int stock,
        string? description,
        int saleCount,
        string? size,
        string? color, double ratingAverage, decimal? originalPrice)
    {
        ShopId = shopId;
        CategoryId = categoryId;
        SetName(name);
        SetPrice(price, null);  // chưa giảm giá
        SetCostPrice(costPrice);
        AddStock(stock);
        Description = description;
        SaleCount = saleCount;
        Size = size;
        Color = color;
        RatingAverage = ratingAverage;
    }

    public void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), maxLength:100);
    }

    private void SetPrice(decimal newPrice, decimal? originalPrice)
    {
        if (newPrice < 0)
        {
            throw new BusinessException("Product: PriceCannotBeNegative");
        }

        Price = newPrice;
        if (originalPrice.HasValue && originalPrice.Value > newPrice)
        {
            OriginalPrice = originalPrice.Value;
        }
        else
        {
            OriginalPrice = null;
        }
    }
    public void SetCostPrice(decimal cost)
    {
        if (cost < 0)
            throw new BusinessException("Product:CostPriceCannotBeNegative");

        CostPrice = cost;
    }


    public void AddStock(int quantity)
    {
        if(quantity<0) throw new BusinessException("Product: QuantityCannotBeNegative");
        Stock += quantity;
    }

    public void ReduceStock(int quantity, bool isSale = true)
    {
        if(quantity<0) throw new BusinessException("Product: QuantityCannotBeNegative");
        if (Stock < quantity)
        {
            throw new BusinessException("Product:OutOfStock").WithData("CurrentStock", Stock);
        }
        Stock -= quantity;
        if (isSale)
        {
            SaleCount += quantity;
        }
    }

    private string? NormalizeAttribute(string? input)
    {
        return input?.Trim().ToUpperInvariant();
    }
    public void SetThumbnail(string url) {
        ThumbnailUrl = Check.NotNullOrWhiteSpace(url, nameof(url));
    }
    public void SetPublishStatus(bool isPublished) {
        IsPublished = isPublished;
    }
    
    
}