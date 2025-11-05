using BaiTapAbp.Dtos;
using BaiTapAbp.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Volo.Abp.Domain.Repositories;

namespace BaiTapAbp.Services.Validator;

public class CreateUpdateCartDtoValidator : AbstractValidator<CreateUpdateCartDto>
{
   public CreateUpdateCartDtoValidator(IRepository<CartEntity, int> cartRepository,  IStringLocalizer<CreateUpdateCategoryDtoValidator> L)
   {
      CascadeMode = CascadeMode.StopOnFirstFailure;
      RuleFor(x => x.CustomerId).NotEmpty().WithMessage(L["CustomerIdRequired"]);
      RuleFor(x=>x.ProductId).NotEmpty().WithMessage(L["ProductIdRequired"]);
      RuleFor(x=>x.Quantity).NotEmpty().WithMessage(L["QuantityRequired"]);
      RuleFor(x=>x.ShopId).NotEmpty().WithMessage(L["ShopIdRequired"]);
   }
    
}