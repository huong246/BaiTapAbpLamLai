using System;

namespace Ord.MasterData.Services.OpenRouterAI
{
    public static class LLMUtil
    {
        //lam sach json, tra ve json nguyen mau
        public static string CleanJsonFromLlmResult(string llmResult)
        {
            if (string.IsNullOrWhiteSpace(llmResult))
            {
                return string.Empty;
            }
            var cleaned = llmResult
               .Replace("```json", "", StringComparison.OrdinalIgnoreCase)
               .Replace("```", "")
               .Trim();
            return cleaned;
        }
    }
}
