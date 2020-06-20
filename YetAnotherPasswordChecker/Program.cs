using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YetAnotherPasswordChecker.DAL;
using YetAnotherPasswordChecker.Infrastructure;

namespace YetAnotherPasswordChecker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var ctx = serviceScope.ServiceProvider.GetRequiredService<PasswordContext>();
                
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
                SeedDB.Seed(ctx);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
