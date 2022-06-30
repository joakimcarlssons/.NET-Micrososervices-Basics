using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        #region Private Members

        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        #endregion

        #region Constructor

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            try
            {
                // Setup connection
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                // Setup exchange and exhange type
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                // Subscribe to shutdown event
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                // Log that we're connected
                Console.WriteLine("--> Connected to Message Bus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not connect to the Message Bus: { ex.Message }");
            }
        }

        #endregion

        #region Public Methods

        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            // Serialize the DTO to a string
            var message = JsonSerializer.Serialize(platformPublishedDto);

            // Verify that connection is open
            if (_connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ connection is open, sending message...");
                SendMessage(message);
            }
            else
            {
                // If connection is not open
                Console.WriteLine("--> RabbitMQ connection is closed, NOT sending...");
            }
        }

        public void Dispose()
        {
            Console.WriteLine("--> Message Bus Disposed");

            // Check if channel is open
            if (_channel.IsOpen)
            {
                // Close down channel and connection
                _channel.Close();
                _connection.Close();
            }
        }

        #endregion

        #region Private Methods

        private void SendMessage(string message)
        {
            // Encode the message
            var body = Encoding.UTF8.GetBytes(message);

            // Publish message to channel
            _channel.BasicPublish(
                exchange: "trigger", 
                routingKey: "", 
                basicProperties: null,
                body: body);

            Console.WriteLine($"--> We have sent { message }");
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"--> RabbitMQ connection shutdown");
        }

        #endregion
    }
}
