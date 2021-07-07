using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CancelTasks
{
    class Program
    {
        static readonly CancellationTokenSource s_cts = new CancellationTokenSource();

        static readonly HttpClient s_client = new HttpClient 
        {
            MaxResponseContentBufferSize = 1_000_000
        };

        static readonly IEnumerable<string> s_urlList = new string[]
        {
            "https://docs.microsoft.com",
            "https://docs.microsoft.com/aspnet/core",
            "https://docs.microsoft.com/azure",
            "https://docs.microsoft.com/azure/devops",
            "https://docs.microsoft.com/dotnet",
            "https://docs.microsoft.com/dynamics365",
            "https://docs.microsoft.com/education",
            "https://docs.microsoft.com/enterprise-mobility-security",
            "https://docs.microsoft.com/gaming",
            "https://docs.microsoft.com/graph",
            "https://docs.microsoft.com/microsoft-365",
            "https://docs.microsoft.com/office",
            "https://docs.microsoft.com/powershell",
            "https://docs.microsoft.com/sql",
            "https://docs.microsoft.com/surface",
            "https://docs.microsoft.com/system-center",
            "https://docs.microsoft.com/visualstudio",
            "https://docs.microsoft.com/windows",
            "https://docs.microsoft.com/xamarin"
        };

        #region 1

        //static async Task SumPageSizesAsync()
        //{
        //    var stopwatch = Stopwatch.StartNew();

        //    int total = 0;
        //    foreach (string url in s_urlList)
        //    {
        //        int contentLength = await ProcessUrlAsync(url, s_client, s_cts.Token);
        //        total += contentLength;
        //    }

        //    stopwatch.Stop();

        //    Console.WriteLine($"\nTotal bytes returned:  {total:#,#}");
        //    Console.WriteLine($"Elapsed time:          {stopwatch.Elapsed}\n");
        //}

        //static async Task<int> ProcessUrlAsync(string url, HttpClient client, CancellationToken token)
        //{
        //    HttpResponseMessage response = await client.GetAsync(url, token);
        //    byte[] content = await response.Content.ReadAsByteArrayAsync(token);
        //    Console.WriteLine($"{url,-60} {content.Length,10:#,#}");

        //    return content.Length;
        //}

        //static async Task Main(string[] args)
        //{
        //    Console.WriteLine("Application started.");
        //    //Console.WriteLine("Press the ENTER key to cancel...\n");

        //    //Task cancelTask = Task.Run(() =>
        //    //{
        //    //    while (Console.ReadKey().Key != ConsoleKey.Enter)
        //    //    {
        //    //        Console.WriteLine("Press the ENTER key to cancel...");
        //    //    }

        //    //    Console.WriteLine("\nENTER key pressed: cancelling downloads.\n");
        //    //    s_cts.Cancel();
        //    //});

        //    //Task sumPageSizesTask = SumPageSizesAsync();

        //    //await Task.WhenAny(new[] { cancelTask, sumPageSizesTask });

        //    try
        //    {
        //        s_cts.CancelAfter(3500);

        //        await SumPageSizesAsync();
        //    }
        //    catch (TaskCanceledException)
        //    {
        //        Console.WriteLine("\nTasks cancelled: timed out.\n");
        //    }
        //    finally
        //    {
        //        s_cts.Dispose();
        //    }

        //    Console.WriteLine("Application ending.");
        //}

        #endregion

        #region 2
        static Task Main() => SumPageSizesAsync();

        static async Task SumPageSizesAsync()
        {
            var stopwatch = Stopwatch.StartNew();

            IEnumerable<Task<int>> downloadTasksQuery =
                from url in s_urlList
                select ProcessUrlAsync(url, s_client);

            List<Task<int>> downloadTasks = downloadTasksQuery.ToList();

            int total = 0;
            while (downloadTasks.Any())
            {
                Task<int> finishedTask = await Task.WhenAny(downloadTasks);
                downloadTasks.Remove(finishedTask);
                total += await finishedTask;
            }

            stopwatch.Stop();

            Console.WriteLine($"\nTotal bytes returned:  {total:#,#}");
            Console.WriteLine($"Elapsed time:          {stopwatch.Elapsed}\n");
        }


        static async Task<int> ProcessUrlAsync(string url, HttpClient client)
        {
            byte[] content = await client.GetByteArrayAsync(url);
            Console.WriteLine($"{url,-60} {content.Length,10:#,#}");

            return content.Length;
        }

        #endregion
    }
}
