//using IdentityServer.Extensions;
//using IdentityServer.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace IdentityServer.Endpoints;

//internal class AuthorizeEndpoint : AuthorizeEndpointBase
//{
//	public AuthorizeEndpoint()
//	{

//	}

//	public async Task<IEndpointResult> ProcessAsync(HttpContext context)
//	{
//        using var activity = Tracing.BasicActivitySource.StartActivity(Constants.EndpointNames.Authorize + "Endpoint");

//        Logger.LogDebug("Start authorize request");

//		NameValueCollection values;

//		if (HttpMethods.IsGet(context.Request.Method))
//		{
//			values = context.Request.Query.AsNameValueCollection();
//        }
//		else if (HttpMethods.IsPost(context.Request.Method))
//		{
//			values = context.Request.Form.AsNameValueCollection();
//		}
//		else
//		{
//            return new StatusCodeResult(HttpStatusCode.MethodNotAllowed);
//        }

//		var user = await UserSession.GetUserAsync();
//		var result = await 
//    }
//}
