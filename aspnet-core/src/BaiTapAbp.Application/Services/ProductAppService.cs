using System.Linq;
using System.Threading.Tasks;
using BaiTapAbp.Dtos;
using BaiTapAbp.Entities;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BaiTapAbp.Services;

public class ProductAppService(IRepository<ProductEntity, int> productRepository)
    : CrudAppService<ProductEntity, ProductDto, int, ProductPagedRequestDto, CreateUpdateProductDto>(productRepository)
{
    protected override async Task<IQueryable<ProductEntity>> CreateFilteredQueryAsync(ProductPagedRequestDto input)
    {
        var query = await base.CreateFilteredQueryAsync(input);
        query.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name.Contains(input.Filter));
        return query;
    }

    [HttpPost]
    [ActionName("GetProductsPaged")]
    public override async Task<PagedResultDto<ProductDto>> GetListAsync(ProductPagedRequestDto input)
    {
        return await base.GetListAsync(input);  
    }
    //create product
    [HttpPost]
    [ActionName("CreateProduct")]
    public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
    {
        return await base.CreateAsync(input);
    }
    //update product
    [HttpPost]
    [ActionName("UpdateProduct")]
    public override async Task<ProductDto> UpdateAsync(int id, CreateUpdateProductDto input)
    {
        return await base.UpdateAsync(id, input);
    }
    
    //detele product
    [HttpPost]
    [ActionName("DeleteProduct")]
    public override async Task DeleteAsync(int id)
    {
        await base.DeleteAsync(id);
    }

    protected override Task<ProductEntity> MapToEntityAsync(CreateUpdateProductDto createInput)
    {
        return base.MapToEntityAsync(createInput);
    }

    protected override Task MapToEntityAsync(CreateUpdateProductDto updateInput, ProductEntity entity)
    {
        return base.MapToEntityAsync(updateInput, entity);
    }
}