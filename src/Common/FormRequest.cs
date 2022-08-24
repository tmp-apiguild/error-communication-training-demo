using Newtonsoft.Json;

namespace Common;

public class FormRequest
{
    public string email { get; set; }

    public string height { get; set; }

    public static implicit operator string(FormRequest request) => JsonConvert.SerializeObject(request, Formatting.Indented);
}
