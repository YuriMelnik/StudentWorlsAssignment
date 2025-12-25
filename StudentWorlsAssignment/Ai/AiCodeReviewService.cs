using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.InkML;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentWorlsAssignment.Ai;

public sealed class AiCodeReviewService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _fullEndpoint; // будет включать модель

    public AiCodeReviewService(HttpClient httpClient, string apiKey, string endpoint, string model)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
        // Google API строит эндпоинт иначе: базовый_адрес + модель : метод
        _fullEndpoint = $"{endpoint}{model}:generateContent?key={apiKey}";
    }

    /// <summary>
    /// Отправляет промпт на Google Gemini API и возвращает текстовый ответ.
    /// </summary>
    public async Task<string> GetReviewAsync(string prompt)
    {
        // 1. Формируем тело запроса в формате Google Gemini
        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts= new[]
                    {
                        new {text = prompt}
                    }
                }
            },
            generationConfig = new
            {
                temperature = 0.5f,
                maxOutputTokens = 8192
            }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // 2. Создаем и отправляем HTTP-запрос (POST)
        // API-ключ уже в URL, отдельный заголовок авторизации не нужен
        var response = await _httpClient.PostAsync(_fullEndpoint, content);

        // 3. Обрабатываем ответ
        if (!response.IsSuccessStatusCode)
        {
            string errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Ошибка Google Gemini API: {response.StatusCode}. Содержимое: {errorContent}");
        }
        var responseJson = await response.Content.ReadAsStringAsync();
        var responseData = JsonSerializer.Deserialize<GeminiResponse>(responseJson);

        if (responseData?.Candidates?.Count > 0 
            && responseData.Candidates[0]?.Content?.Parts?.Count > 0)
        {
            return responseData.Candidates[0].Content.Parts[0].Text;
        }
        throw new InvalidOperationException("Google Gemini вернул пустой или некорректный ответ");
    }

}

internal class GeminiResponse
{
    [JsonPropertyName("candidates")]
    public List<Candidate> Candidates { get; set; }
}

public class Candidate
{
    [JsonPropertyName("content")]
    public Content Content { get; set; }
}

public class Content
{
    [JsonPropertyName("parts")]
    public List<Part> Parts { get; set; }
}

public class Part
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
}