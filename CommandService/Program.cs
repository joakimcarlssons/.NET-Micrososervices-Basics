using CommandService.AsyncDataServices;
using CommandService.EventProcessing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
    services.AddScoped<ICommandRepo, CommandRepo>();
    services.AddControllers();

    services.AddHostedService<MessageBusSubscriber>();

    services.AddSingleton<IEventProcessor, EventProcessor>();

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}
