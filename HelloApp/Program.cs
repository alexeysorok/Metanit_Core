using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HelloApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();

            //var host = new WebHostBuilder()
            //.UseKestrel() // настраиваем веб-сервер kestrel
            //.UseContentRoot(Directory.GetCurrentDirectory()) // настраиваем корневой каталог приложения
            //.UseIISIntegration() // интеграция с IIS - будет прокси серверов и передвать в kestrel 
            //.UseStartup<Startup>() // устанавливаем главный файл приложения
            //.Build(); // создаем хост 

            //host.Run(); // запускаем приложение

            using (var host = WebHost.Start("http://localhost:8080", async context =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync("Привет мир!");
            }))
            {
                Console.WriteLine("Application has been startded"); // показываем что приложение запустилось
                host.WaitForShutdown();
            }
        }

        // public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //     WebHost.CreateDefaultBuilder(args)
        //         .UseStartup<Startup>();
    }
}
