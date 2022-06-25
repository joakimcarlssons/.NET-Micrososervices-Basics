namespace PlatformService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        #region Private Members

        private readonly AppDbContext _context;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PlatformRepository(AppDbContext context)
        {
            _context = context;
        } 

        #endregion

        public void CreatePlatform(Platform platform)
        {
            if (platform == null) throw new ArgumentNullException(nameof(platform));

            _context.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return _context.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges() =>  (_context.SaveChanges() >= 0);
    }
}
