using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using BaiTapAbp.OpenRouterAI;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;
namespace Ord.MasterData.Services.OpenRouterAI
{
    public class MedicalVoiceApiService(ClinicLoadPromptService loadPromptService,
        OpenRouterAIService openRouterAIService, WhisperPythonService _whisperService
            ) : DomainService
    { 
        //private readonly IHubContext<MedicalVoiceHub> _hubContext;
        /// <summary>
        /// Xử lý voice input và trả về các fields đã nhận diện
        /// </summary>
        /// <param name="voiceText">Text đã được chuyển đổi từ giọng nói</param>
        /// <returns>Response chứa danh sách fields và nội dung</returns>
        public async Task<VoiceInputResponse> ProcessVoiceInputAsync(string speechToText)
        {
            //kiem tra text duoc chuyen tu audio
            if (string.IsNullOrEmpty(speechToText))
            {
                return new VoiceInputResponse
                {
                    Success = false,
                    Message = "Voice text is empty",
                    Fields = new List<FieldData>()
                };
            }
            //dung clinicLoadPrompt de gan text tren vao cuoi
            var prompt = (await loadPromptService.GetMedicalResultInputPromptAsync()) + speechToText;
            //gọi OpenAI xu ly prompt => json
            var result = await openRouterAIService.AskLLMAsync(prompt, "google/gemma-3-12b-it:free");
            if (string.IsNullOrEmpty(result))
            {
                return new VoiceInputResponse
                {
                    Success = false,
                    Message = "LLM returned empty result",
                    Fields = new List<FieldData>()
                };
            }
            //lam sach json, bo nhung phan thua do openAI tra ve
            result = LLMUtil.CleanJsonFromLlmResult(result);
            return new VoiceInputResponse
            {
                Success = true,
                Message = "Processed successfully",
                Fields = JsonSerializer.Deserialize<List<FieldData>>(result)
            };
        }
        public async Task<VoiceInputResponse> ProcessVoiceFileAsync(
            Stream audioFile, 
            string fileName,
            string language = "auto")
        {
            try
            {
                var speechToText = await _whisperService.TranscribeAudioAsync(
                    audioFile, 
                    fileName, 
                    language
                );
                var speechText  = speechToText.Text;
                return await ProcessVoiceInputAsync(speechText);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error processing voice file");
                return new VoiceInputResponse
                {
                    Success = false,
                    Message = $"Error: {ex.Message}",
                    Fields = new List<FieldData>()
                };
            }
        }
    }
     
}
