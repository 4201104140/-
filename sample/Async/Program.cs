using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Async
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var task1 = Talk1();

            //Talk2();

            //await task1;
            await foreach (var word in ReadWordsFromStreamAsync())
            {
                Console.Write(word);
            }
            
        }

        static async IAsyncEnumerable<string> ReadWordsFromStreamAsync()
        {
            string data =
                @"This is a line of text.
                  Here is the second line of text.
                  And there is one more for good measure.
                  Wait, that was the penultimate line.";

            using var readStream = new StringReader(data);
            
            string line = await readStream.ReadLineAsync();
            while (line != null)
            {
                foreach (string word in line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    yield return word;
                }
                await Task.Delay(1000);
                yield return "\n";
                line = await readStream.ReadLineAsync();
            }
        }

        static Task Talk1()
        {
            Console.WriteLine("1");
            return Task.CompletedTask;
        }

        static void Talk2()
        {
            Console.WriteLine("2");
        }
    }
}
