using System.Diagnostics.CodeAnalysis;

namespace ODataMemoryLeak.Services;

[ExcludeFromCodeCoverage]
public static class ODataConfiguration
{
    public const string SwaggerDocVersion = "v1";
    public const string SwaggerDocDescription = "Swagger OData Demo";
    public const string SwaggerDocTitle = "OData Demo API";
}