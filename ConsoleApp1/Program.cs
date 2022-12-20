using Microsoft.Owin.Hosting;
using System.Net.Http;
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string enderecoBase = "http://localhost:9000/";

            using (WebApp.Start<Startup>(url: enderecoBase))
            {
                HttpClient client = new HttpClient();

                var reposta = client.GetAsync(enderecoBase + "api/posts").Result;

                Console.WriteLine(reposta);
                Console.WriteLine(reposta.Content.ReadAsStringAsync().Result);

                Console.ReadLine();
            }
        }
    }
}
