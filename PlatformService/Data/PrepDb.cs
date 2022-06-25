namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            // Get the service scope in order to use services in static methods
            using var serviceScope = app.ApplicationServices.CreateScope();

            // Seed initial data using the AppDbContext service of the initialized service scope
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());

        }

        private static void SeedData(AppDbContext context)
        {
            // If we don't have any data...
            if (!context.Platforms.Any())
            {
                Debug.WriteLine("--> Seeding data...");

                context.Platforms.AddRange(
                    new()
                    {
                        Name = "Dot Net",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new()
                    {
                        Name = "SQL Server Express",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new()
                    {
                        Name = "Kubernetes",
                        Publisher = "Cloud Native Computing Foundation",
                        Cost = "Free"
                    }
                );

                context.SaveChanges();
            }
            else
            {
                Debug.WriteLine("--> We already have data.");
            }
        }
    }
}
