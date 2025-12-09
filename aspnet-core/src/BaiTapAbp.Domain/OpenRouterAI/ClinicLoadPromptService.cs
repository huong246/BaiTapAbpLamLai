using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ord.MasterData.Services.OpenRouterAI
{
    public class ClinicLoadPromptService : ISingletonDependency
    {   private readonly ConcurrentDictionary<string, string> _promptCache = new();

        public async Task<string> GetMedicalResultInputPromptAsync()
        {
            return await GetPromptAsync("CLINIC_MEDICAL_RESULT", "ClinicMedicalResultPrompt.txt");
        }

        private async Task<string> GetPromptAsync(string promptName, string fileName)
        {
            if (_promptCache.TryGetValue(promptName, out var cachedPrompt))
                return cachedPrompt;

            var promptPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                //"Services",
                "OpenRouterAI",
                "Prompts",
                fileName
            );

            if (!File.Exists(promptPath))
            {
                throw new FileNotFoundException($"Prompt file not found: {promptPath}");
            }

            var content = await File.ReadAllTextAsync(promptPath);

            // cache lại
            _promptCache[promptName] = content;

            return content;
        }
    }
}
