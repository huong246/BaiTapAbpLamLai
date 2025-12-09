using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ord.MasterData.Services.OpenRouterAI
{
    
    public class VoiceInputDto { 
    public string SpeechToText { get; set; } = string.Empty;
    }
    /// <summary>
    /// Response model từ voice input API
    /// </summary>
    public class VoiceInputResponse
    {
        //tra ve fieldName chuan json c#
        [JsonPropertyName("fields")]
        public List<FieldData>? Fields { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }

    /// <summary>
    /// Field data model
    /// </summary>
    public class FieldData
    {
        [JsonPropertyName("field_name")]
        public string? FieldName { get; set; }
        [JsonPropertyName("content")]
        public string? Content { get; set; }
    }
}
