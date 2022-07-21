using Microsoft.Bot.Streaming.Payloads;

namespace Microsoft.Bot.Streaming;

/// <summary>
/// An incoming request from a remote client.
/// </summary>
public class ReceiveRequest
{
    public string? Verd { get; set; }

    public string? Path { get; set; }

    public IList<IContentStream> Streams { get; set; } = new List<IContentStream>();
}
