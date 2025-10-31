using AutoMapper;
using BaiTapAbp.Dtos;
using BaiTapAbp.Entities;

namespace BaiTapAbp.Mapper;

public class BaiTapAbpApplicationAutoMapperProfile : Profile
{
    public BaiTapAbpApplicationAutoMapperProfile()
    {
        CreateMap<UserEntity, UserDto>();
        CreateMap<CreateUpdateAppUserDto, UserEntity>();
        
        CreateMap<ShopEntity, ShopDto>();
        CreateMap<CreateUpdateShopDto, ShopEntity>();
        
        CreateMap<ProductEntity, ProductDto>();
        CreateMap<CreateUpdateProductDto, ProductEntity>();

    }
}