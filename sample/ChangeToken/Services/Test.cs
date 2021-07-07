using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeTokenSample.Services
{
    public interface ITest
    {
        public void ConsoleA();
    }
    public class TestA : ITest
    {
        public void ConsoleA()
        {
            Console.WriteLine("A");
        }
    }

    public class TestB : ITest
    {
        public void ConsoleA()
        {
            Console.WriteLine("B");
        }
    }
}
