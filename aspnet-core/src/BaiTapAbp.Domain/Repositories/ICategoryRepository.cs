using System.Threading.Tasks;
using BaiTapAbp.Entities;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace BaiTapAbp.Repositories;

public interface ICategoryRepository :  IRepository<CategoryEntity, int>, IScopedDependency
{
    Task<bool> HaveCategoryInDbAsync(string categoryName);
    Task<bool> HaveProductInCategoryAsync(int categoryId);
    Task<bool> CheckCategoryExistInDbAsync(int categoryId);
}