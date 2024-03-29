﻿using CommandService.Model;

namespace CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        #region Private Members

        private readonly AppDbContext _context;

        #endregion

        #region Constructor

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        public void CreateCommand(int platformId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;
            _context.Commands.Add(command);
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _context.Platforms.Add(platform);
        }

        public bool ExternalPlatformExists(int externalPlatformId)
            => _context.Platforms.Any(p => p.ExternalId == externalPlatformId);

        public IEnumerable<Platform> GetAllPlatforms()
            => _context.Platforms.ToList();

        public Command GetCommand(int platformId, int commandId)
            => _context.Commands
            .FirstOrDefault(c => c.PlatformId == platformId && c.Id == commandId);

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
            => _context.Commands
            .Where(c => c.PlatformId == platformId)
            .OrderBy(c => c.Platform.Name);


        public bool PlatformExists(int platformId)
            => _context.Platforms.Any(p => p.Id == platformId);

        public bool SaveChanges() 
            => (_context.SaveChanges() >= 0);
    }
}
