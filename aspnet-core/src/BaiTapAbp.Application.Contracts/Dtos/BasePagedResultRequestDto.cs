using BaiTapAbp.Utils;
using Volo.Abp.Application.Dtos;

namespace BaiTapAbp.Dtos;

public class BasePagedResultRequestDto:PagedResultRequestDto
{
    public string? Filter { get; set; }
    public string? FilterFts => Filter.ConvertFts();
}