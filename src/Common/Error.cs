using Newtonsoft.Json;

namespace Common;

public class ValidationError
{
    public string category { get; set; } = "BusinessError";

    public string type { get; set; } = "https://developer.horizonafs.io/errors/validation-error";

    public string title { get; set; } = "Request did not validate";

    public IEnumerable<InvalidParameter> invalidParameters { get; set; }

    public static implicit operator string(ValidationError error) => JsonConvert.SerializeObject(error, Formatting.Indented);

    public class InvalidParameter
    {
        public string name { get; set; }

        public string reason { get; set; }
    }
}