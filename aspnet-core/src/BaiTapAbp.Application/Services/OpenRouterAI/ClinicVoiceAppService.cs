using System.Threading.Tasks;
using Ord.MasterData.Services.OpenRouterAI;
using Volo.Abp.Application.Services;
using Microsoft.AspNetCore.SignalR;  
 

namespace Ord.MasterData.Services.OpenRouterAI.AppService
{
    public class ClinicVoiceAppService(MedicalVoiceApiService medicalVoiceApiService) : ApplicationService
    {
       
        
        public async Task<VoiceInputResponse> ConvertVoiceToClinicFieldsAsync(VoiceInputDto input)
        {
            return await medicalVoiceApiService.ProcessVoiceInputAsync(input.SpeechToText);
        }
    }
}
