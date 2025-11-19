using System;
using System.IO;
using System.Threading.Tasks;
using BaiTapAbp.BlobContainers;
using BaiTapAbp.Entities;
using BaiTapAbp.IServices.Products;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;

namespace BaiTapAbp.Services;

public class ProductImageAppService(
    IBlobContainer<ProductImageContainer> blobContainer,
    IRepository<ProductImageEntity, int> imageRepo)
    : ApplicationService, IProductImageAppService
{
    private readonly IBlobContainer<ProductImageContainer> _blobContainer = blobContainer;
    private readonly IRepository<ProductImageEntity, int> _imageRepo = imageRepo;
     
    public async Task<string> UploadAsync(int productId, IRemoteStreamContent file)
    {
        //tao ten file duy nhat xau ngau nhien + duoi file
         var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
         // dinh nghia noi luu tru
         var blobPath = $"{productId}/{fileName}";

         //luu vao blob storage
         await _blobContainer.SaveAsync(
             blobPath,
             file.GetStream(),
             overrideExisting: true);
         //luu thong tin vao db
         await _imageRepo.InsertAsync(new ProductImageEntity()
         {
             ProductId = productId,
             ImageUrl = blobPath
         });
         return blobPath;
    }
}