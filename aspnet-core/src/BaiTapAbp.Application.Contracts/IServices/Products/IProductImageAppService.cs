using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace BaiTapAbp.IServices.Products;

public interface IProductImageAppService : IApplicationService
{

    Task<string> UploadAsync(int productId, IRemoteStreamContent file);
}