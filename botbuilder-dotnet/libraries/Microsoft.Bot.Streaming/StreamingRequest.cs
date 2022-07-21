using Microsoft.Bot.Streaming.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Bot.Streaming;

public class StreamingRequest
{
    public const string GET = "GET";

    public const string POST = "POST";

    public const string PUT = "PUT";

    public const string DELETE = "DELETE";

    public string? Verb { get; set; }

    public IList<ResponseMessageStream> Streams = new List<ResponseMessageStream>();

    public static StreamingRequest CreateGet(string path = null!, HttpContent body = null!) => CreateRequest(GET);


}
