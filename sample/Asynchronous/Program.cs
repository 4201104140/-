using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Asynchronous
{
    class Program
    {
        #region 1
        //static void Main(string[] args)
        //{
        //    var stopwatch = Stopwatch.StartNew();
        //    Coffee cup = PourCoffee();
        //    Console.WriteLine("coffee is ready");

        //    Egg eggs = FryEggs(2);
        //    Console.WriteLine("eggs are ready");

        //    Bacon bacon = FryBacon(3);
        //    Console.WriteLine("bacon is ready");

        //    Toast toast = ToastBread(2);
        //    ApplyButter(toast);
        //    ApplyJam(toast);
        //    Console.WriteLine("toast is ready");

        //    Juice oj = PourOJ();
        //    Console.WriteLine("oj is ready");
        //    Console.WriteLine("Breakfast is ready!");

        //    stopwatch.Stop();
        //    Console.WriteLine($"Elapsed time:          {stopwatch.Elapsed}\n");
        //}

        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        //private static Toast ToastBread(int slices)
        //{
        //    for (int slice = 0; slice < slices; slice++)
        //    {
        //        Console.WriteLine("Putting a slice of bread in the toaster");
        //    }
        //    Console.WriteLine("Start toasting...");
        //    Task.Delay(3000).Wait();
        //    Console.WriteLine("Remove toast from toaster");

        //    return new Toast();
        //}

        //private static Bacon FryBacon(int slices)
        //{
        //    Console.WriteLine($"putting {slices} slices of bacon in the pan");
        //    Console.WriteLine("cooking first side of bacon...");
        //    Task.Delay(3000).Wait();
        //    for (int slice = 0; slice < slices; slice++)
        //    {
        //        Console.WriteLine("flipping a slice of bacon");
        //    }
        //    Console.WriteLine("cooking the second side of bacon...");
        //    Task.Delay(3000).Wait();
        //    Console.WriteLine("Put bacon on plate");

        //    return new Bacon();
        //}

        //private static Egg FryEggs(int howMany)
        //{
        //    Console.WriteLine("Warming the egg pan...");
        //    Task.Delay(3000).Wait();
        //    Console.WriteLine($"cracking {howMany} eggs");
        //    Console.WriteLine("cooking the eggs ...");
        //    Task.Delay(3000).Wait();
        //    Console.WriteLine("Put eggs on plate");

        //    return new Egg();
        //}

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }

        #endregion

        #region 2
        static async Task Main(string[] args)
        {
            var stopwatch = Stopwatch.StartNew();



            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");

            Task<Egg> eggsTask = FryEggsAsync(2);
            Task<Bacon> baconTask = FryBaconAsync(3);
            Task<Toast> toastTask = ToastBreadAsync(2);

            Toast toast = await toastTask;
            ApplyButter(toast);
            ApplyJam(toast);
            Console.WriteLine("toast is ready");
            Juice oj = PourOJ();
            Console.WriteLine("oj is ready");

            Egg eggs = await eggsTask;
            Console.WriteLine("eggs are ready");
            Bacon bacon = await baconTask;
            Console.WriteLine("bacon is ready");

            Console.WriteLine("Breakfast is ready!");

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time:          {stopwatch.Elapsed}\n");
        }

        private static Task<Juice> PourOJAsync()
        {
            Console.WriteLine("Pouring orange juice");
            return Task.FromResult(new Juice());
        }

        private static Task ApplyJamAsync(Toast toast)
        {
            Console.WriteLine("Putting jam on the toast");
            return Task.CompletedTask;
        }


        private static Task ApplyButterAsync(Toast toast)
        {
            Console.WriteLine("Putting butter on the toast");
            return Task.CompletedTask;
        }

        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        private static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }

        private static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private static async Task<Coffee> PourCoffeeAsync()
        {
            Console.WriteLine("Pouring coffee");
            await Task.CompletedTask;
            return new Coffee();
        }
        #endregion

        #region record
        private record Juice();

        private record Toast();

        private record Bacon();

        private record Egg();

        private record Coffee();
        #endregion
    }
}
