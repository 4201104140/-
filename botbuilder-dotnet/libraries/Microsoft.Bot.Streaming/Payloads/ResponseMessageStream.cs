namespace Microsoft.Bot.Streaming.Payloads;

/// <summary>
/// An attachment contained within a <see cref="StreamingRequest"/>'s stream collection,
/// which itself contains any form of media item.
/// </summary>
public class ResponseMessageStream
{
    public ResponseMessageStream()
        : this(Guid.NewGuid())
    {
    }

    public ResponseMessageStream(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }

    public HttpContent? Content { get; set; }
}
