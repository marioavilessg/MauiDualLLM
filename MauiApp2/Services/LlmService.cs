using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp2.Services
{
    public class LlmService
    {
        private readonly HttpClient _client = new();

        public async Task<string> GetResponseAsync(
            string userText,
            string systemPrompt,
            string model,
            double temperature,
            int maxTokens,
            string uri)
        {
            var payload = new
            {
                model = model,
                messages = new[]
                {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userText }
            },
                temperature = temperature,
                max_tokens = maxTokens
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);

            return doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString()!;
        }
    }
}
