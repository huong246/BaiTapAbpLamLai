using BaiTapAbp.Dtos;
using BaiTapAbp.Entities;
using BaiTapAbp.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Volo.Abp.Domain.Repositories;

namespace BaiTapAbp.Services.Validator;

public class CreateUpdateProductDtoValidator: AbstractValidator<CreateUpdateProductDto>
{
    public CreateUpdateProductDtoValidator(IRepository<ProductEntity, int> productRepository, IStringLocalizer<BaiTapAbpResource> L)
    {
        CascadeMode = CascadeMode.StopOnFirstFailure;
        RuleFor(x => x.Name).NotEmpty().WithMessage(L["ProductNameRequired"])
            .Length(2, 101).WithMessage(L["ProductNameLength", 2, 101]);
        
        //gia ban hien tai
        RuleFor(x => x.Price).NotEmpty().WithMessage(L["ProductPriceRequired"]);
        //gia von nhap hang
        RuleFor(x => x.CostPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage(L["ProductCostPriceMustBePositive"]);
        
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0) 
            .WithMessage(L["ProductStock"]);
    }
}