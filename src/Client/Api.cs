using System.Net.Http.Json;
using Common;
using Newtonsoft.Json;

namespace Client;

public class Api
{
    private readonly HttpClient api;
    private readonly ILogger<Api> logger;

    public Api(HttpClient api, ILogger<Api> logger)
    {
        this.api = api;
        this.logger = logger;
    }

    public async Task<Response> PostForm(FormRequest request)
    {
        var httpresponse = await api.PostAsJsonAsync("form", request);

        logger.LogInformation(JsonConvert.SerializeObject(httpresponse));

        var response = new Response
        {
            Success = httpresponse.IsSuccessStatusCode,
            StatusCode = (int)httpresponse.StatusCode,
            Content = await httpresponse.Content.ReadAsStringAsync()
        };

        if (!response.Success)
            response.Error = JsonConvert.DeserializeObject<ValidationError>(response.Content);

        return response;
    }
}

public class Response
{
    public bool Success { get; set; }

    public int StatusCode { get; set; }

    public ValidationError Error { get; set; }

    public string Content { get; set; }
}
