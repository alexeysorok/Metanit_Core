using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HelloApp
{
    public class Startup
    {
        IHostingEnvironment _env;

        // используем необязательный конструктор 
        public Startup(IHostingEnvironment env)
        {
            _env = env;
        }
        
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {   
            // если приложение в процессе разработки 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // обработка запроса - получаем контекст запроса в ввиде объекта context
            app.Run(async (context) =>
            {
                // отправить ответ в виде строки 
                //await context.Response.WriteAsync("Hello World!");
                // В браузере будет выводится название приложения, которое хранится в свойстве _env.Application
                await context.Response.WriteAsync(_env.ApplicationName);

            });
        }


    }
}
