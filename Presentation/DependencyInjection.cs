using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        /*
         Here's the explanation for the ApiVersioningOptions properties:
            + DefaultApiVersion - Sets the default API version. Typically, this will be v1.0.
            + ReportApiVersions - Reports the supported API versions in the api-supported-versions response header.
            + AssumeDefaultVersionWhenUnspecified - Uses the DefaultApiVersion when the client didn't provide an explicit version.
            + ApiVersionReader - Configures how to read the API version specified by the client. The default value is QueryStringApiVersionReader.
         The AddApiExplorer method is helpful if you are using Swagger. It will fix the endpoint routes and substitute the API version route parameter.
         */

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}