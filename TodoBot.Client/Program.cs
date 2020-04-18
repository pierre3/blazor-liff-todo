using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using LineDC.Liff;
using Microsoft.Extensions.Configuration;
using TodoBot.Client.Services;
using System.Net.Http;

namespace TodoBot.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(new HttpClient 
            { 
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
            });
            builder.Services.AddSingleton<ILiffClient>(serviceProvider => {
                var appSettings = serviceProvider
                    .GetRequiredService<IConfiguration>().Get<AppSettings>();
                return new LiffClient(appSettings.LiffId);
            });
            builder.Services.AddSingleton<ITodoClient>(serviceProvider => {
                var httpClient = serviceProvider.GetRequiredService<HttpClient>();
                var appsettings = serviceProvider.GetRequiredService<IConfiguration>().Get<AppSettings>();
                return new TodoClient(httpClient, appsettings.FunctionUrl);
            });
            await builder.Build().RunAsync();
        }
    }
}
