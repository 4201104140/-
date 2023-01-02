﻿using IdentityServer.Extensions;
using IdentityServer.Hosting;
using IdentityServer.ResponseHandling;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace IdentityServer.Endpoints.Results;

internal class TokenErrorResult : IEndpointResult
{
    public TokenErrorResponse Response { get; }

    public TokenErrorResult(TokenErrorResponse error)
    {
        if (error.Error.IsMissing()) throw new ArgumentNullException(nameof(error.Error), "Error must be set");

        Response = error;
    }

    public async Task ExecuteAsync(HttpContext context)
    {
        context.Response.StatusCode = 400;
        context.Response.SetNoCache();

        var dto = new ResultDto
        {
            error = Response.Error,
            error_description = Response.ErrorDescription,

            custom = Response.Custom
        };

        await context.Response.WriteJsonAsync(dto);
    }

    internal class ResultDto
    {
        public string error { get; set; }
        public string error_description { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> custom { get; set; }
    }
}
