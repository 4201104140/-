using Microsoft.Extensions.Logging;

namespace Microsoft.Bot.Streaming;

/// <summary>
/// Implemented by classes used to process incoming requests sent over an IStreamingTransport and adhering to the Bot Framework Protocol v3 with Streaming Extensions.
/// </summary>
public abstract class RequestHandler
{
    public abstract Task<StreamingResponse> ProcessRequestAsync(ReceiveRequest request, ILogger<RequestHandler> logger, object context = null!, CancellationToken cancellationToken = default(CancellationToken));
}
