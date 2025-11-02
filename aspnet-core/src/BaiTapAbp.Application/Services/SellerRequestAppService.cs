using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaiTapAbp.Authorization;
using BaiTapAbp.Dtos;
using BaiTapAbp.Entities;
using BaiTapAbp.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace BaiTapAbp.Services;
[Authorize]
public class SellerRequestAppService(
    IRepository<SellerRequestEntity, int> requestRepository,
    IdentityUserManager userManager)
    : ApplicationService
{
    
    public async Task<PagedResultDto<SellerRequestDto>> GetListAsync(SellerRequestPageDto input)
    { 
        var query = await requestRepository.GetQueryableAsync();
        query = query.Where(r => r.Status == RequestStatus.Pending);
        var totalCount = await AsyncExecuter.CountAsync(query);
        query = query.Skip(input.SkipCount).Take(input.MaxResultCount);
        var requests = await AsyncExecuter.ToListAsync(query);
        var dto = ObjectMapper.Map<List<SellerRequestEntity>, List<SellerRequestDto>>(requests);
        return new PagedResultDto<SellerRequestDto>(totalCount, dto);
    }
     
    [Authorize(Roles = UserRole.Customer)]
    public async Task CreateRequestAsync()
    {
        var userId = CurrentUser.Id; 
        if (!userId.HasValue)
        {
            throw new AbpAuthorizationException("Authentication required to perform this action.");
        }
        var actualUserId = userId.Value;
        var existingRequest = await requestRepository.FirstOrDefaultAsync(r=>r.UserId == actualUserId && r.Status == RequestStatus.Pending);
        if (existingRequest != null)
        {
            throw new UserFriendlyException("You have already created a request!!!.");
        }
        var request = new SellerRequestEntity(actualUserId);
        await requestRepository.InsertAsync(request);
    }

    [Authorize(RolePermissions.SellerRequests.Manage)]
    public async Task ApproveRequestAsync(int requestId)
    {
        var request = await requestRepository.GetAsync(requestId);
        if (request == null)
        {
            throw new UserFriendlyException("Request not found!!!.");
        }
        if (request.Status != RequestStatus.Pending)
        {
            throw new UserFriendlyException("This request reviewed!!!!");
        }
        var user = await userManager.GetByIdAsync(request.UserId);
        await userManager.AddToRoleAsync(user, UserRole.Seller);
        request.Approve(CurrentUser.GetId());
        await requestRepository.UpdateAsync(request);
    }

    [Authorize(RolePermissions.SellerRequests.Manage)]
    public async Task RejectRequestAsync(int requestId, string reason)
    {
        var request = await requestRepository.GetAsync(requestId);
        if (request == null)
        {
            throw new UserFriendlyException("Request not found!!!.");
        }
        if (request.Status != RequestStatus.Pending)
        {
            throw new UserFriendlyException("This request reviewed!!!!");
        }
        request.Reject(CurrentUser.GetId(), reason);
        await requestRepository.UpdateAsync(request);
    }
}