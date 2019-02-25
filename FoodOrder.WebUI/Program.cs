using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FoodOrder.WebUI {
    public class Program {
        public static void Main(string[] args) {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) {
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseStartup<Startup>();
        }
    }
}