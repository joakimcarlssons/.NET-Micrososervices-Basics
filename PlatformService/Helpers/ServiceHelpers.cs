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
            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseInMemoryDatabase("InMem"));

            builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
            builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
