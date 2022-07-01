using CommandService.Model;
using CommandService.SyncDataServices.Grpc;

namespace CommandService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            // Get a service scope to use DI in static method
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            // Get an instance of the IPlatformDataClient service
            var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

            // Get all platforms
            var platforms = grpcClient.ReturnAllPlatforms();

            // Seed data
            SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(), platforms);
        }

        private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
        {
            Console.WriteLine("--> Seeding new platforms...");

            foreach (var plat in platforms)
            {
                // Verify that the 
                if (!repo.ExternalPlatformExists(plat.ExternalId))
                {
                    repo.CreatePlatform(plat);
                }

                repo.SaveChanges();
            }
        }
    }
}
