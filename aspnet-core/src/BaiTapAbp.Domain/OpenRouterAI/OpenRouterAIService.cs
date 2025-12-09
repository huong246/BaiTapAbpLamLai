using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Ord.MasterData.Services.OpenRouterAI
{
    public class OpenRouterAIService : DomainService
    {
        private readonly string _apiKey = "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";
        private readonly string URL = "https://openrouter.ai/api/v1/chat/completions";

        public Task<string> AskLLMAsync(string prompt)
        {
            // Sửa lỗi: thêm null cho roleSystem parameter
            return AskLLMAsync(prompt, "google/gemma-3-12b-it:free", null);
        }

        public Task<string?> AskLLMAsync(string prompt, string modelLLMName)
        {
            // Sửa lỗi recursive call - thêm null cho roleSystem
            return AskLLMAsync(prompt, modelLLMName, null);
        }

        public async Task<string?> AskLLMAsync(string prompt, string modelLLMName, string? roleSystem)
        {
            try
            {
                Logger.LogInformation("Starting OpenRouter API call with model: {Model}", modelLLMName);

                var client = new RestClient(URL);
                var request = new RestRequest("", Method.Post);
                request.AddHeader("Authorization", $"Bearer {_apiKey}");
                request.AddHeader("Content-Type", "application/json");

                // Tạo danh sách messages động
                var messages = new List<object>();

                // Thêm system message nếu có
                if (!string.IsNullOrEmpty(roleSystem))
                {
                    messages.Add(new
                    {
                        role = "system",
                        content = roleSystem
                    });
                    Logger.LogDebug("Added system message to request");
                }

                // Luôn thêm user message
                messages.Add(new
                {
                    role = "user",
                    content = prompt
                });

                // Tạo body với messages đã xây dựng
                var body = new
                {
                    model = modelLLMName,
                    temperature = 0.2,
                    messages = messages.ToArray()
                };

                // Log request body để debug
                var bodyJson = JsonSerializer.Serialize(body);
                Logger.LogDebug("Request body: {RequestBody}", bodyJson);

                request.AddJsonBody(body);

                var response = await client.ExecuteAsync(request);

                // Log response details
                Logger.LogInformation("OpenRouter API response - Status: {StatusCode}, Success: {IsSuccessful}",
                    response.StatusCode, response.IsSuccessful);

                if (!response.IsSuccessful)
                {
                    // Log đầy đủ error details
                    Logger.LogError(
                        "OpenRouter API failed - Status: {StatusCode}, Error: {ErrorMessage}, Content: {Content}, Exception: {ErrorException}",
                        response.StatusCode,
                        response.ErrorMessage,
                        response.Content,
                        response.ErrorException?.ToString()
                    );

                    // Kiểm tra các lỗi cụ thể
                    var errorDetail = "";
                    if (response.Content != null)
                    {
                        try
                        {
                            using var errorDoc = JsonDocument.Parse(response.Content);
                            if (errorDoc.RootElement.TryGetProperty("error", out var errorElement))
                            {
                                var errorMessage = errorElement.TryGetProperty("message", out var msgElement)
                                    ? msgElement.GetString()
                                    : "Unknown error";
                                var errorType = errorElement.TryGetProperty("type", out var typeElement)
                                    ? typeElement.GetString()
                                    : "Unknown type";
                                errorDetail = $" Error Type: {errorType}, Message: {errorMessage}";
                            }
                        }
                        catch (JsonException jsonEx)
                        {
                            Logger.LogWarning(jsonEx, "Failed to parse error response as JSON");
                        }
                    }

                    throw new Exception($"OpenRouter API error - Status: {response.StatusCode}{errorDetail}. Full content: {response.Content}");
                }

                // Log successful response (be careful with sensitive data)
                Logger.LogDebug("Response content length: {Length} characters", response.Content?.Length ?? 0);

                if (string.IsNullOrEmpty(response.Content))
                {
                    Logger.LogWarning("OpenRouter API returned empty content");
                    return null;
                }

                using var doc = JsonDocument.Parse(response.Content);

                // Kiểm tra structure của response
                if (!doc.RootElement.TryGetProperty("choices", out var choices))
                {
                    Logger.LogError("Response missing 'choices' property. Content: {Content}", response.Content);
                    throw new Exception("Invalid response structure: missing 'choices'");
                }

                var choicesArray = choices.EnumerateArray().ToList();
                if (choicesArray.Count == 0)
                {
                    Logger.LogWarning("OpenRouter API returned empty choices array");
                    return null;
                }

                var firstChoice = choicesArray[0];
                if (!firstChoice.TryGetProperty("message", out var message))
                {
                    Logger.LogError("Choice missing 'message' property");
                    throw new Exception("Invalid response structure: missing 'message' in choice");
                }

                if (!message.TryGetProperty("content", out var content))
                {
                    Logger.LogError("Message missing 'content' property");
                    throw new Exception("Invalid response structure: missing 'content' in message");
                }

                var result = content.GetString();
                Logger.LogInformation("Successfully received response from OpenRouter API");

                return result;
            }
            catch (JsonException jsonEx)
            {
                Logger.LogError(jsonEx, "JSON parsing error in OpenRouter response");
                throw new Exception($"Failed to parse OpenRouter API response: {jsonEx.Message}", jsonEx);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unexpected error in AskLLMAsync");
                throw;
            }
        }
    }
}