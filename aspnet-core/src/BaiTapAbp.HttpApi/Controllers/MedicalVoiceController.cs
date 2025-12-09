using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ord.MasterData.Services.OpenRouterAI;
using Volo.Abp.AspNetCore.Mvc;

namespace BaiTapAbp.Controllers;
[ApiController]
[Route("api/medical-voice")]
public class MedicalVoiceController : AbpController
{
    private readonly MedicalVoiceApiService _medicalVoiceApiService;
    private readonly ILogger<MedicalVoiceController> _logger;
    public MedicalVoiceController(
        MedicalVoiceApiService medicalVoiceApiService,
        ILogger<MedicalVoiceController> logger)
    {
        _medicalVoiceApiService = medicalVoiceApiService;
        _logger = logger;
    }

    [HttpPost("process-file")]
    [RequestSizeLimit(52428800)] // 50MB limit
    [ProducesResponseType(typeof(VoiceInputResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<VoiceInputResponse> ProcessVoiceFile(
        IFormFile file,
        [FromForm] string language = "auto")
    {
        try
        {
            _logger.LogInformation(
                "Received voice file - Name: {FileName}, Size: {Size} bytes, Language: {Language}",
                file?.FileName,
                file?.Length,
                language
            );
            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("No file received");
                return new VoiceInputResponse
                {
                    Success = false,
                    Message = "No audio file provided",
                    Fields = []
                };
            }
            
            if (file.Length > 52428800)
            {
                _logger.LogWarning("File too large: {Size} bytes", file.Length);
                return (new VoiceInputResponse
                {
                    Success = false,
                    Message = "File too large. Maximum size is 50MB",
                    Fields = new System.Collections.Generic.List<FieldData>()
                });
            }
            var allowedExtensions = new[] { ".m4a", ".mp3", ".wav", ".ogg", ".flac" };
            var fileExtension = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();
                
            if (!Array.Exists(allowedExtensions, ext => ext == fileExtension))
            {
                _logger.LogWarning("Invalid file type: {Extension}", fileExtension);
                return (new VoiceInputResponse
                {
                    Success = false,
                    Message = $"Invalid file type. Allowed: {string.Join(", ", allowedExtensions)}",
                    Fields = new System.Collections.Generic.List<FieldData>()
                });
            }
            _logger.LogInformation("Starting voice processing...");
            await using var stream = file.OpenReadStream();
            var result= await _medicalVoiceApiService.ProcessVoiceFileAsync(
                stream,
                file.FileName,
                language
            );
            _logger.LogInformation(
                "Voice processing completed - Success: {Success}, Fields count: {Count}",
                result.Success,
                result.Fields?.Count ?? 0
            );
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing voice file");
                
            return (new VoiceInputResponse
            {
                Success = false,
                Message = $"Server error: {ex.Message}",
                Fields = new System.Collections.Generic.List<FieldData>()
            });
        }
    }
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok(new
        {
            status = "healthy",
            timestamp = DateTime.UtcNow,
            service = "Medical Voice API"
        });
    }

    [HttpPost("process-text")]
        public async Task<ActionResult<VoiceInputResponse>> ProcessText(
            [FromBody] VoiceInputDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto?.SpeechToText))
                {
                    return BadRequest(new VoiceInputResponse
                    {
                        Success = false,
                        Message = "Text is empty",
                        Fields = new System.Collections.Generic.List<FieldData>()
                    });
                }

                var result = await _medicalVoiceApiService.ProcessVoiceInputAsync(dto.SpeechToText);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing text");
                return StatusCode(500, new VoiceInputResponse
                {
                    Success = false,
                    Message = $"Error: {ex.Message}",
                    Fields = new System.Collections.Generic.List<FieldData>()
                });
            }
        }
    }