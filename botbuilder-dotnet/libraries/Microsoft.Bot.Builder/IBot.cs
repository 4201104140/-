namespace Microsoft.Bot.Builder;

/// <summary>
/// Represents a bot that can operate on incoming activities.
/// </summary>
/// <remarks>A <see cref="BotA"/>
/// </remarks>
public interface IBot
{
    /// <summary>
    /// When implemented in a bot, handles an incoming activity.
    /// </summary>
    /// <param name="turnContext">The context object for this turn.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the work queued to execute.</returns>
    /// <remarks>The <paramref name="turnContext"/> provides information about the
    /// incoming activity, and other data needed to process the activity.</remarks>
    /// <seealso cref="ITurnContext"/>
    /// <seealso cref="Bot.Schema.IActivity"/>
    Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken));
}
