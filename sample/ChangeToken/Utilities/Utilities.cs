using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ChangeTokenSample.Utilities
{
    public static class Utilities
    {

        #region snippet2
        public async static Task<string> GetFileContent(string filePath)
        {
            var runCount = 1;

            while (runCount < 4)
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        using (var fileStreamReader = File.OpenText(filePath))
                        {
                            return await fileStreamReader.ReadToEndAsync();
                        }
                    }
                    else
                    {
                        throw new FileNotFoundException();
                    }
                }
                catch (IOException ex)
                {
                    if (runCount == 3 || ex.HResult != -2147024864)
                    {
                        throw;
                    }
                    else
                    {
                        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, runCount)));
                        runCount++;
                    }
                }
            }

            return null;
        }
        #endregion
    }
}
