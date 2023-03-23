using CleanServices.API.Contracts.Error.Responses;
using CleanServices.API.Infrastructure.Exceptions.StatusCode;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace CleanServices.API.Infrastructure.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder UseCustomInvalidModelStateResponse(this IMvcBuilder builder)
    {
        return builder.ConfigureApiBehaviorOptions(options =>
            options.InvalidModelStateResponseFactory = action =>
            {
                var detailsList = new List<ErrorDetails>();
                // TODO: target or code here?
                foreach (var (target, modelEntry) in action.ModelState)
                {
                    var errors = modelEntry.Errors.ToList();
                    errors.ForEach(error =>
                    {
                        detailsList.Add(new ErrorDetails
                        {
                            Target = target,
                            Message = error.ErrorMessage
                        });
                    });
                }
                
                var response = new BadRequestException().ToErrorResponse();
            }
        );
    }

    private static IEnumerable<string> SelectModelErrors(this ModelStateDictionary modelState)
        => modelState.Select(x => x.Value).SelectMany(x => x!.Errors).Select(x => x.ErrorMessage);
}