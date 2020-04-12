using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using LineDC.Liff;
using Microsoft.Extensions.Configuration;

namespace TodoBot.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddSingleton<ILiffClient>(serviceProvider => {
                var appSettings = serviceProvider
                    .GetRequiredService<IConfiguration>().Get<AppSettings>();
                return new LiffClient(appSettings.LiffId);
            });
            await builder.Build().RunAsync();
        }
    }
}
