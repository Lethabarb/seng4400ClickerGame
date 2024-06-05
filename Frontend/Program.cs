using Amazon.SQS;
using Frontend;
using Frontend.Services;
using Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddSingleton<IQueueService, QueueService>();
        builder.Services.AddSingleton<IGameService, GameService>();
        builder.Services.AddSingleton<LeaderboardService>();
        //builder.Services.AddTransient<ICookieService, CookieService>();




        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        await builder.Build().RunAsync();
    }
}

