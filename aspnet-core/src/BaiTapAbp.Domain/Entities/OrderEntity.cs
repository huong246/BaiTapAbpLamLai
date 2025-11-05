using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("Order")]
public class OrderEntity : FullAuditedEntity<int>
{
    
}