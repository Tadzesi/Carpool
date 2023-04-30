﻿using System.Diagnostics;

namespace Carpool.CoreApi.Middleware;

public class ResponseTimeMiddleware
{
    private readonly RequestDelegate _next;

    public ResponseTimeMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = new Stopwatch();

        context.Response.OnStarting(() =>
        {
            watch.Stop();

            context.Response
                .Headers
                .Add("X-Processing-Time-Milliseconds",
                        new[] { watch.ElapsedMilliseconds.ToString() });

            return Task.CompletedTask;
        });

        watch.Start();

        await _next(context);
    }
}