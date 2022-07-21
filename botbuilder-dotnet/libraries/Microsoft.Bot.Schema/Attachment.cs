using System.Text.Json.Serialization;

namespace Microsoft.Bot.Schema;

public class Attachment
{
	public Attachment()
	{
	}

	public Attachment(string contentType = default!, string contentUrl = default!, object content = default!)
	{
		ContentType = contentType;
		ContentUrl = contentUrl;
		Content = content;
	}

	[JsonPropertyName("contentType")]
    public string? ContentType { get; set; }

    [JsonPropertyName("contentUrl")]
    public string? ContentUrl { get; set; }

    [JsonPropertyName("content")]
    public object? Content { get; set; }
}
