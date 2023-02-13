using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static Common.ValidationError;

namespace Function.Api;

public class PostForm
{
    private readonly IValidator<FormRequest> validator;

    public PostForm(IValidator<FormRequest> validator)
    {
        this.validator = validator;
    }

    [FunctionName(nameof(PostForm))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var form = JsonConvert.DeserializeObject<FormRequest>(body) ?? new FormRequest();

        var validation = validator.Validate(form);
        if (!validation.IsValid)
        {
            return new BadRequestObjectResult(new ValidationError
            {
                invalidParameters = validation.Errors.Select(e => new InvalidParameter
                {
                    name = e.PropertyName,
                    reason = e.ErrorMessage
                })
            });
        }

        return new OkResult();
    }
}
