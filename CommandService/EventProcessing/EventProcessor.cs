using CommandService.Dtos;
using CommandService.Model;
using System.Text.Json;

namespace CommandService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        #region Private Members

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.PlatformPublished:
                    AddPlatform(message);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Determines the type of event sent by extracting the payload (<paramref name="notificationMessage"/>)
        /// </summary>
        /// <param name="notificationMessage">The payload/message sent</param>
        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            switch (eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("--> Platform Published event detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("--> Could not determine event type");
                    return EventType.Undetermined;
            }
        }

        private void AddPlatform(string platformPublishedMessage)
        {

            using var scope = _scopeFactory.CreateScope();

            // Inject repository because lifetime is shorter than EventProcessor objects
            var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

            // Extract the dto
            var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

            try
            {
                // Get the model
                var platform = _mapper.Map<Platform>(platformPublishedDto);

                // Verify that it doesn't exist
                if (!repo.ExternalPlatformExists(platform.ExternalId))
                {
                    repo.CreatePlatform(platform);
                    repo.SaveChanges();

                    Console.WriteLine("--> Platform added!");
                }
                else
                {
                    Console.WriteLine("--> Platform already exists...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not add Platform to DB { ex.Message }");
            }
        }

        #endregion
    }

    enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}
