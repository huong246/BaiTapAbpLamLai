using BaiTapAbp.Dtos;
using BaiTapAbp.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Volo.Abp.Domain.Repositories;

namespace BaiTapAbp.Services.Validator;

public class CreateUpdateCategoryDtoValidator : AbstractValidator<CreateUpdateCategoryDto>
{
    public CreateUpdateCategoryDtoValidator(IRepository<CategoryEntity, int> categoryRepository, IStringLocalizer<CreateUpdateCategoryDtoValidator> L)
    {
        CascadeMode = CascadeMode.StopOnFirstFailure;
        RuleFor(x=>x.Name).NotEmpty().WithMessage(L["CategoryNameRequired"])
            .Length(2,101).WithMessage(L["CategoryNameLength", 2, 101]);
    }
}