using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class ChatAPI
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public ChatAPI(string apiKey)
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
        _apiKey = apiKey;
    }

    public async Task<string> SendMessageAndGetResponse(string message)
    {
        var requestUrl = "engines/davinci-codex/completions";
        var requestBody = new
        {
            prompt = message,
            max_tokens = 100,
            temperature = 1,
            stop = "\n"
        };
        var requestJson = JsonConvert.SerializeObject(requestBody);

        using (var request = new HttpRequestMessage(HttpMethod.Post, requestUrl))
        {
            request.Content = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return "you suck";
                throw new Exception($"Failed to get response from OpenAI API. Status code: {response.StatusCode}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            dynamic? responseObject = JsonConvert.DeserializeObject(responseJson);
            string? outputText = responseObject.choices[0].text;

            return outputText;
        }
    }
}