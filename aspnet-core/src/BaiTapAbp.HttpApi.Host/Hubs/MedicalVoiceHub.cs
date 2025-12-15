using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Ord.MasterData.Services.OpenRouterAI;

namespace BaiTapAbp.Hubs;

public class MedicalVoiceHub : Hub
{
    private readonly ILogger<MedicalVoiceHub> _logger;
    public MedicalVoiceHub(ILogger<MedicalVoiceHub> logger)
    {
        _logger = logger;
    }
    
    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
        
        await Clients.Caller.SendAsync("Connected", new
        {
            connectionId = Context.ConnectionId,
            message = "Connected to Medical Voice Hub",
            timestamp = DateTime.UtcNow
        });

        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation(
            "Client disconnected: {ConnectionId}, Exception: {Exception}",
            Context.ConnectionId,
            exception?.Message
        );

        await base.OnDisconnectedAsync(exception);
    }
    
    public async Task SubscribeToMedicalUpdates()
    {
        _logger.LogInformation(
            "Client {ConnectionId} subscribed to medical updates",
            Context.ConnectionId
        );

        await Clients.Caller.SendAsync("SubscriptionConfirmed", new
        {
            message = "You are now subscribed to medical field updates",
            timestamp = DateTime.UtcNow
        });
    }
    
    public async Task BroadcastMedicalFields(List<FieldData> fields)
    {
        _logger.LogInformation(
            "Broadcasting {Count} medical fields to all clients",
            fields.Count
        );

        await Clients.All.SendAsync("ReceiveMedicalFields", new
        {
            fields = fields,
            timestamp = DateTime.UtcNow,
            source = "voice_input"
        });
    }
    
    public async Task SendMedicalFieldsToClient(
        string connectionId, 
        List<FieldData> fields)
    {
        _logger.LogInformation(
            "Sending {Count} medical fields to client {ConnectionId}",
            fields.Count,
            connectionId
        );

        await Clients.Client(connectionId).SendAsync("ReceiveMedicalFields", new
        {
            fields = fields,
            timestamp = DateTime.UtcNow,
            source = "voice_input"
        });
    }
    
    public async Task SendFieldUpdate(FieldData field)
    {
        _logger.LogDebug(
            "Broadcasting single field update: {FieldName}",
            field.FieldName
        );

        await Clients.All.SendAsync("ReceiveFieldUpdate", new
        {
            field = field,
            timestamp = DateTime.UtcNow
        });
    }

    public async Task SendStatusUpdate(string status, string? message = null)
    {
        await Clients.All.SendAsync("ReceiveStatus", new
        {
            status = status,
            message = message,
            timestamp = DateTime.UtcNow
        });
    }
    
}