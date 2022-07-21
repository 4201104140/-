using Microsoft.Bot.Streaming.Payloads;

namespace Microsoft.Bot.Streaming;

public class ReceiveResponse
{
    public int StatusCode { get; set; }

    public IList<IContentStream> Streams { get; set; } = new List<IContentStream>();
}
