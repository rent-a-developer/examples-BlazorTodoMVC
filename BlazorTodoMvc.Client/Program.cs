using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BlazorTodoMvc.Client.Model;
using BlazorTodoMvc.Client.Services;
using BlazorTodoMvc.Shared.Localization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTodoMvc.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddOptions();
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<ILocalizationApi, LocalizationApi>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddLocalizationServices();
            builder.Services.AddSingleton<TodoRepository>();

            var host = builder.Build();

            using (var serviceScope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var localizationApi = serviceScope.ServiceProvider.GetService<ILocalizationApi>();
                var localizationProvider = serviceScope.ServiceProvider.GetService<LocalizationProvider>();

                await localizationProvider.Initialize(localizationApi);
            }

            await host.RunAsync();
        }
    }
}
