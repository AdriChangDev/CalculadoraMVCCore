using Microsoft.AspNetCore;

namespace Calculadora
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        // EF Core uses this method at design time to access the DbContext
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}