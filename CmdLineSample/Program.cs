using System;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;

namespace CmdLineSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var setting = new Dictionary<string, string> { { "name", "yanh" }, { "age", "12" } };

            var builder = new ConfigurationBuilder().AddCommandLine(args);
            var conf = builder.Build();
            Console.WriteLine($"name:{conf["name"]}");
            Console.WriteLine($"age:{conf["age"]}");
            Console.ReadLine();
        }
    }
}
