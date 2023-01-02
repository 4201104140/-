
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

#nullable enable
namespace Microsoft.DurableTask;

static class Check
{
    /// <summary>
    /// Checks in the provided element is null, throwing if it is.
    /// Throws <see cref="ArgumentException" /> if the conditions are not met.
    /// </summary>
    /// <param name="argument">The element to check.</param>
    /// <param name="name">The name of the element for the exception.</param>
    /// <typeparam name="T">The type of element to check.</typeparam>
    /// <returns>The original element.</returns>
    [return: NotNullIfNotNull("argument")]
    public static T NotNull<T>([NotNull] T? argument, [CallerArgumentExpression("argument")] string? name = default)
        where T : class
    {
        if (argument is null)
        {
            throw new ArgumentNullException(name);
        }

        return argument;
    }
}