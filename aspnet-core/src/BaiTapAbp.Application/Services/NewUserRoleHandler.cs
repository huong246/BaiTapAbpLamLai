using System.Linq;
using System.Threading.Tasks;
using BaiTapAbp.Authorization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Identity;


namespace BaiTapAbp.Services;

public class NewUserRoleHandler(IdentityUserManager userManager)
    : ILocalEventHandler<EntityCreatedEventData<IdentityUser>>, ITransientDependency
{
    private readonly IdentityUserManager _userManager = userManager;

    public async Task HandleEventAsync(EntityCreatedEventData<IdentityUser> eventData)
    {
        var user = eventData.Entity;
        var existingRoles = await userManager.GetRolesAsync(user);
        if (existingRoles.Any())
        {
            return;
        }
        await _userManager.AddToRoleAsync(user, UserRole.Customer);
    }
}