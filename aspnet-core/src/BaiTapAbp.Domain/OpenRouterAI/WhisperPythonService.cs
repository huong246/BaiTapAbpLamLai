using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
namespace BaiTapAbp.OpenRouterAI;

public class WhisperPythonService : DomainService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _pythonApiBaseUrl;
    public WhisperPythonService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration) 
    {
        _httpClientFactory = httpClientFactory;
        _pythonApiBaseUrl = configuration["PythonApi:BaseUrl"] ?? string.Empty;
    }
    public async Task<PythonWhisperResponse> TranscribeAudioAsync(
        Stream fileStream, 
        string fileName, 
        string language = "auto")
    {
        try
        {
            Logger.LogInformation("Starting transcribe audio: {FileName}, Language: {Language}", 
                fileName, language);

            var client = _httpClientFactory.CreateClient(); 
            client.Timeout = TimeSpan.FromSeconds(120);

            using var form = new MultipartFormDataContent();
            using var fileContent = new StreamContent(fileStream);
            
            fileContent.Headers.ContentType = GetContentType(fileName);
            form.Add(fileContent, "file", fileName);

            if (language != "auto")
            {
                form.Add(new StringContent(language), "language");
                form.Add(new StringContent("false"), "auto_detect");
            }
            else
            {
                form.Add(new StringContent("true"), "auto_detect");
            }

            var endpoint = $"{_pythonApiBaseUrl}/api/transcribe";
            Logger.LogDebug("Calling Python API: {Endpoint}", endpoint);

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = form
            };

            request.Headers.Add("ngrok-skip-browser-warning", "true");
            request.Headers.Add("User-Agent", "DotNetAPI/1.0");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Logger.LogError(
                    "Python API Error - Status: {StatusCode}, Content: {Content}",
                    response.StatusCode,
                    errorContent
                );
                throw new Exception($"Python API Error: {response.StatusCode} - {errorContent}");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            Logger.LogDebug("Python API Response: {Response}", jsonString);

            var result = JsonSerializer.Deserialize<PythonWhisperResponse>(
                jsonString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (result == null || string.IsNullOrEmpty(result.Text))
            {
                Logger.LogWarning("Python API returned empty result");
                throw new Exception("Python API returned empty text");
            }

            Logger.LogInformation(
                "Transcribe successful - Text length: {Length}, Language: {DetectedLanguage}",
                result.Text.Length,
                result.Language ?? "unknown"
            );

            return result;
        }
        catch (TaskCanceledException ex)
        {
            Logger.LogError(ex, "Timeout calling Python API");
            throw new Exception("Timeout while transcribing audio. File might be too large.", ex);
        }
        catch (HttpRequestException ex)
        {
            Logger.LogError(ex, "Network error calling Python API");
            throw new Exception("Cannot connect to Python API. Please check if service is running.", ex);
        }
        catch (JsonException ex)
        {
            Logger.LogError(ex, "Failed to parse Python API response");
            throw new Exception("Invalid response from Python API", ex);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Unexpected error in TranscribeAudioAsync");
            throw;
        }
    }
    
    private System.Net.Http.Headers.MediaTypeHeaderValue GetContentType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension switch
        {
            ".m4a" => new System.Net.Http.Headers.MediaTypeHeaderValue("audio/mp4"),
            ".mp3" => new System.Net.Http.Headers.MediaTypeHeaderValue("audio/mpeg"),
            ".wav" => new System.Net.Http.Headers.MediaTypeHeaderValue("audio/wav"),
            ".ogg" => new System.Net.Http.Headers.MediaTypeHeaderValue("audio/ogg"),
            ".flac" => new System.Net.Http.Headers.MediaTypeHeaderValue("audio/flac"),
            _ => new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream")
        };
    }

    public class PythonWhisperResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("language")]
        public string? Language { get; set; }

        [JsonPropertyName("segments_count")]
        public int? SegmentsCount { get; set; }
    }
}