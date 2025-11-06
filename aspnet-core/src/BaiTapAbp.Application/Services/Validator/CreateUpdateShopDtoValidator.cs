using BaiTapAbp.Dtos;
using BaiTapAbp.Entities;
using BaiTapAbp.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Volo.Abp.Domain.Repositories;

namespace BaiTapAbp.Services.Validator;

public class CreateUpdateShopDtoValidator: AbstractValidator<CreateUpdateShopDto>
{
    public CreateUpdateShopDtoValidator(IRepository<ShopEntity, int> repository, IStringLocalizer<BaiTapAbpResource> L)
    {
        CascadeMode = CascadeMode.StopOnFirstFailure;
        RuleFor(x => x.Name).NotEmpty().WithMessage(L["ShopNameRequired"])
            .Length(2, 100).WithMessage(L["ShopNameLength"]);
        RuleFor(x=>x.Address).NotEmpty().WithMessage(L["AddressShopRequired"])
            .Length(2,100).WithMessage(L["AddressShopLength"]);
    }
}