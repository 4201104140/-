using Microsoft.Bot.Streaming.Payloads;
using System.Net;

namespace Microsoft.Bot.Streaming;

public class StreamingResponse
{
    public int StatusCode { get; set; }

    public List<ResponseMessageStream>? Streams { get; set; }

    public static StreamingResponse NotFound(HttpContent body = null!) => CreateResponse(HttpStatusCode.NotFound, body);

    public static StreamingResponse OK(HttpContent body = null!) => CreateResponse(HttpStatusCode.OK, body);

    public static StreamingResponse CreateResponse(HttpStatusCode statusCode, HttpContent body = null!)
    {
        var response = new StreamingResponse()
        {
            StatusCode = (int)statusCode,
        };

        if (body != null)
        {
            response.AddStream(body);
        }
        
        return response;
    }

    public void AddStream(HttpContent content)
    {
        if (content == null)
        {
            throw new ArgumentNullException(nameof(content));
        }

        Streams ??= new List<ResponseMessageStream>();

        Streams.Add(
            new()
            {
                Content = content,
            });
    }
}
