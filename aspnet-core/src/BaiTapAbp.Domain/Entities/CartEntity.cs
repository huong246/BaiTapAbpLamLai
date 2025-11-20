using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("Cart")]
public class CartEntity : FullAuditedEntity<int>
{
     [Required]
     public int CustomerId {get; set; }
     [Required] 
     public int ShopId {get; set; }
     [Required] 
     public int ProductId {get; set; }
     [Required]
     public int Quantity {get; set; }
     [Column(TypeName = "decimal(18,2)")] 
     public decimal UnitPrice { get; private set; }

     public CartEntity(int customerId, int shopId, int productId, int quantity, decimal unitPrice)
     {
          CustomerId = customerId;
          ShopId = shopId;
          ProductId = productId;
          UnitPrice = unitPrice;
          SetQuantity(quantity);
     }
     
     public void SetQuantity(int quantity)
     {
          if (quantity <= 0)
          {
               throw new BusinessException("Cart:QuantityMustBePositive");
          }

          if (quantity > 100)
          {
               throw new BusinessException("Cart:QuantityLimitExceeded");
          }

          Quantity = quantity;
     }

     public void IncreaseQuantity(int amount)
     {
          if (amount < 0) throw new BusinessException("Cart:InvalidAmount");
          SetQuantity(Quantity + amount);
     }

     public void UpdatePrice(decimal newPrice)
     {
          UnitPrice = newPrice;
     }
}