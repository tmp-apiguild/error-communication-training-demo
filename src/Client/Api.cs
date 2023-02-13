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

    public async Task<FormResponse> Send(FormRequest request)
    {
        var httpresponse = await api.PostAsJsonAsync("api/PostForm", request);

        logger.LogInformation(JsonConvert.SerializeObject(httpresponse));

        var response = new FormResponse
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

public class FormResponse
{
    public bool Success { get; set; }

    public int StatusCode { get; set; }

    public ValidationError Error { get; set; }

    public string Content { get; set; }
}
