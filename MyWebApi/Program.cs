using System;
using Microsoft.Owin.Hosting;

namespace MyWebApi
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var baseAddress = "http://*:5000";
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"MyWebApi running on {baseAddress}. Press Enter to exit.");
                Console.ReadLine();
            }
        }
    }
}