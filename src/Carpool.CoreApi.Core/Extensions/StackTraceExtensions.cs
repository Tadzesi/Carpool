using Carpool.CoreApi.Core.Operations;
using GuardNet;
using System.Diagnostics;
using System.Reflection;

namespace Carpool.CoreApi.Core.Extensions;

internal static class StackTraceExtensions
{
    private const string _undefined = "Undefined";

    public static string GetMethodName(this StackTrace stackTrace, string? containsSequence = null)
    {
        Guard.NotNull(stackTrace, nameof(stackTrace));

        var methodStackFrames = stackTrace
            .GetFrames()
            .Select(i => i.GetMethod())
            .ToList();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        MethodBase? methodBase = methodStackFrames.Find(i => i.ToString().Contains(containsSequence ?? nameof(OperationResponse)));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        return string.Join(".", methodBase?.ReflectedType?.Name ?? _undefined, methodBase?.Name ?? _undefined);
    }
}
