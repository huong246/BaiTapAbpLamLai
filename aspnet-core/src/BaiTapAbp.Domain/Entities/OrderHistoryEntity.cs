using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaiTapAbp.Entities;

[Table("OrderHistory")]
public class OrderHistoryEntity : FullAuditedEntity<int>
{
    
}