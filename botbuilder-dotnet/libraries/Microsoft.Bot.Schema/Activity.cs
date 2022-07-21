using System.Text.Json.Serialization;

namespace Microsoft.Bot.Schema;

/// <summary>
/// An Activity is the basic communication type for the Bot Framework 3.0
/// protocol.
/// </summary>
public partial class Activity
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Activity"/> class.
	/// </summary>
	public Activity(string type = default!, string id = default!, string channelId = default!)
	{
		Type = type;
		Id = id;
		ChannelId = channelId;
	}

	[JsonPropertyName("type")]
	public string Type { get; set; }

	[JsonPropertyName("id")]
	public string Id { get; set; }

    [JsonPropertyName("channelId")]
    public string ChannelId { get; set; }
}
