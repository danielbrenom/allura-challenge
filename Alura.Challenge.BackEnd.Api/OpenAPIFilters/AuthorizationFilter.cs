using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Alura.Challenge.BackEnd.Api.OpenAPIFilters
{
    public class AuthorizationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var requiredScopes = context.MethodInfo.GetCustomAttributes(true)
                                        .OfType<AuthorizeAttribute>()
                                        .Select(attr => attr.Policy)
                                        .Distinct()
                                        .ToList();
            if (!requiredScopes.Any()) return;
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized - Não autorizado" });

            var securityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "JWT" }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new()
                {
                    [securityScheme] = requiredScopes.ToList()
                }
            };
        }
    }
}