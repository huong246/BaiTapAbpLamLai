using System.Linq;
using System.Threading.Tasks;
using BaiTapAbp.Dtos;
using BaiTapAbp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BaiTapAbp.Services;

public class CategoryAppService(IRepository<CategoryEntity, int> categoryRepository)
    : CrudAppService<CategoryEntity, CategoryDto, int, CategoryPagedRequestDto, CreateUpdateCategoryDto>(
        categoryRepository)
{
    protected override async Task<IQueryable<CategoryEntity>> CreateFilteredQueryAsync(CategoryPagedRequestDto input)
    {
        var query = await base.CreateFilteredQueryAsync(input);
        query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name.Contains(input.Filter));
        return query;
    }

    [HttpPost]
    [ActionName("GetPaged")]
    [AllowAnonymous]
    public override async Task<PagedResultDto<CategoryDto>> GetListAsync(CategoryPagedRequestDto input)
    {
        return await base.GetListAsync(input);
    }

    /*
    [HttpPost]
    [ActionName("Create")]
    [Authorize(RolePermissions.Categories.Create)]
    public override async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
    {
        
    }
    */
}