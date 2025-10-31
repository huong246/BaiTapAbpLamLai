using System.Linq;
using System.Threading.Tasks;
using BaiTapAbp.Dtos;
using BaiTapAbp.Entities;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BaiTapAbp.Services;

public class ShopAppService(IRepository<ShopEntity, int> shopRepository)
    : CrudAppService<ShopEntity, ShopDto, int, ShopPagedRequestDto, CreateUpdateShopDto>(shopRepository)
{
    /*protected override async Task<IQueryable<ShopEntity>> CreateFilteredQueryAsync(CreateUpdateShopDto input)
    {
        var query = await base.CreateFilteredQueryAsync(input);
        query.WhereIf(!string.IsNullOrWhiteSpace(input.Filter),)
    }*/
}