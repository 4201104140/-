namespace Microsoft.Bot.Streaming.Payloads;

public interface IContentStream
{
    Guid Id { get; }

    string ContentType { get; }

    int? Length { get; set; }

    Stream Stream { get; }
}
