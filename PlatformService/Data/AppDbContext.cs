namespace PlatformService.Data
{
    public class AppDbContext : DbContext
    {
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        #endregion

        #region DbSets

        public DbSet<Platform> Platforms { get; set; }

        #endregion
    }
}
