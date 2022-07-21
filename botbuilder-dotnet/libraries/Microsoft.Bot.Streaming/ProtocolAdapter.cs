using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Bot.Streaming;

internal class ProtocolAdapter
{
    private readonly RequestHandler _requestHandler;

	public ProtocolAdapter(RequestHandler requestHandler)
	{
		_requestHandler = requestHandler;
	}

	public async Task<ReceiveResponse> SendRequestAsync(StreamingResquest)
}
