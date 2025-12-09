using System;
using System.Threading.Tasks;
using BaiTapAbp.Hubs;
using Microsoft.AspNetCore.SignalR;
using Ord.MasterData.Services.OpenRouterAI.Events;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace BaiTapAbp.EventHandlers;

public class MedicalVoiceSignalRHandler : ILocalEventHandler<MedicalVoiceProcessedEto>, ITransientDependency
{
    private readonly IHubContext<MedicalVoiceHub> _hubContext;

    public MedicalVoiceSignalRHandler(IHubContext<MedicalVoiceHub> hubContext)
    {
        _hubContext = hubContext;
    }
    public async Task HandleEventAsync(MedicalVoiceProcessedEto eventData)
    {
        if (eventData.Success)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMedicalData", new 
            {
                fields = eventData.Fields,
                timestamp = DateTime.UtcNow,
                source = "voice_input"
            });
        }
    }
}