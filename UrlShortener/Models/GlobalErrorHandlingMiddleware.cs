﻿using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace UrlShortener.Models;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context) 
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new
            {
                message = ex.Message,
                statusCode = response.StatusCode
            };

            var errorJson = JsonSerializer.Serialize(errorResponse);

            await response.WriteAsync(errorJson);
        }
    }
}
