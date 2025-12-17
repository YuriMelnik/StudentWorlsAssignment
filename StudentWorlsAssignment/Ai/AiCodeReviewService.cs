using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace StudentWorlsAssignment.Ai;

public sealed class AiCodeReviewService
{
    private readonly HttpClient _httpClient;

    public AiCodeReviewService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> ReviewAsync(string code)
    {
        // Простейший контракт: отправляем JSON { code: "...", language: "csharp" }
        var payload = new
        {
            code,
            language = "csharp"
        };

        using var response = await _httpClient.PostAsJsonAsync("https://your-ai-endpoint/review", payload);
        response.EnsureSuccessStatusCode();

        // Ответ ожидаем как { feedback: "..." }
        using var stream = await response.Content.ReadAsStreamAsync();
        var json = await JsonDocument.ParseAsync(stream);
        return json.RootElement.GetProperty("feedback").GetString() ?? string.Empty;
    }
}
