//// Copyright (c) Microsoft Corporation. All rights reserved.
//// Licensed under the MIT License.

//using System;
//using System.IO.Pipes;
//using System.Threading.Tasks;
//using Microsoft.Bot.Builder.Streaming;
//using Microsoft.Bot.Streaming.UnitTests.Mocks;
//using Xunit;

//namespace Microsoft.Bot.Streaming.UnitTests
//{
//    public class NamedPipeTests
//    {
//        public async Task DisconnectWorksAsIntenededAsync()
//        {
//            // Truncating GUID to make sure the full path does not exceed 104 characters;
//            var pipeName = Guid.NewGuid().ToString().Substring(0, 18);
//            var readStream = new NamedPipeServerStream(pipeName, PipeDirection.In, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Byte, PipeOptions.WriteThrough | PipeOptions.Asynchronous);
//            var writeStream = new NamedPipeClientStream(".", pipeName, PipeDirection.Out, PipeOptions.WriteThrough | PipeOptions.Asynchronous);
//            new StreamingRequestHandler(new MockBot(), new )
//        }
//    }
//}
