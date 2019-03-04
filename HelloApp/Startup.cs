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
        // Middleware -обозначает небольшие компоненты приложения, конвейеры обработки 
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            // подключение middleware через класс
            // передача значений
            app.UseToken("123");

            // подключение middleware
            //app.UseMiddleware<TokenMiddleware>();


            //Метод Map(и методы расширения MapXXX()) применяется для сопоставления
            //пути запроса с определeнным делегатом, 
            //который будет обрабатывать запрос по этому пути
            app.Map("/index", Index);
            app.Map("/about", About);

            // вложенные методы Map, обрабатывают маршруты
            app.Map("/home", home =>
            {
                home.Map("/index", Index);
                home.Map("/about", About);

            });
            //Теперь метод About будет обарабатывать запрос не http://localhost:xxxx/about, а http://localhost:xxxx/home/about

            //// если приложение в процессе разработки 
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.MapWhen(context => {

                return context.Request.Query.ContainsKey("id") &&
                        context.Request.Query["id"] == "5";
            }, HandleId);

            //В данном случае если в запросе указан параметр id и он имеет значение 5, то запрос обрабатывается функцией HandleId().К подобным запросам будут относиться, например, запрос http://localhost:55234/?id=5 или http://localhost:55234/product?id=5&name=phone, так как обе строки запроса содержат параметр id равный 5. А все остальные запросы также будут обрабатываться делегатом, передаваемым в метод app.Run().




            int x = 2;
            int y = 8;
            int z = 0;

            // use позволяет после него вызывать другой контейнер
            app.Use(async (context, next) =>
            {
                z = x * y;
                await next.Invoke();
            });


            // То есть при вызове await next.Invoke() 
            //обработка запроса перейдет к тому компоненту, который установлен в методе app.Run().

            // обработка запроса - получаем контекст запроса в ввиде объекта context
            app.Run(async (context) =>
            {
                // отправить ответ в виде строки 
                //await context.Response.WriteAsync("Hello World!");
                // В браузере будет выводится название приложения, которое хранится в свойстве _env.Application
                //await context.Response.WriteAsync(_env.ApplicationName);

                //x = x * 2; // 2 * 2 = 4
                //await context.Response.WriteAsync($"Result: {x}"); // показывает что компоненты middleware создаются один раз при старте приложения 
                //await context.Response.WriteAsync($"x * y = {z}");

                // имя хоста к кторому обращается пользователь 
                string host = context.Request.Host.Value;
                // путь запроса 
                string path = context.Request.Path;
                // параметры строки запрос 
                string query = context.Request.QueryString.Value;


                // можем отрправлять код HTML 
                await context.Response.WriteAsync($"<h3>Host: {host}</h3>" +
                    $" <h3>Path: {path}</h3>" +
                    $" <h3>Query: {query}</h3>");


            });
        }


        private static void Index(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Index");
            });
        }

        private static void About(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("About");
            });
        }

        private static void HandleId(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("id is equal to 5");
            });
        }




    }
}
