using PlatformService.SyncDataServices.Http;

namespace PlatformService.Helpers
{
    public static class ServiceHelpers
    {
        /// <summary>
        /// Sets up and configures the services in the application
        /// </summary>
        /// <param name="builder">The instance where the services are being set up</param>
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsProduction())
            {
                Console.WriteLine("--> Using SqlServer Db");
                builder.Services.AddDbContext<AppDbContext>(opt =>  
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
            }
            else
            {
                // Use in memory database if we're not in production
                Console.WriteLine("--> Using InMem Db");
                builder.Services.AddDbContext<AppDbContext>(opt =>
                    opt.UseInMemoryDatabase("InMem"));
            }

            builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
            builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
