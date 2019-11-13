using System;
using Microsoft.Extensions.Configuration;

namespace JsonCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("class1.json");
            var conf = builder.Build();
            Console.WriteLine($"classno:{conf["classno"]}");
            Console.WriteLine($"classdes:{conf["classdes"]}");

            Console.WriteLine(conf["students:0:name"]);
            Console.WriteLine(conf["students:1:name"]);
            Console.WriteLine(conf["students:2:name"]);


            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
