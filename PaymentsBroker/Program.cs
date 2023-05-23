using PaymentsBroker.Consumers;
using PaymentsBroker.Mongo;
using PaymentsBroker.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PaymentDatabaseSettings>(
    builder.Configuration.GetSection("Mongo"));

builder.Services.AddHostedService<PaymentConsumer>();

builder.Services.AddSingleton<PaymentRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();


app.Run();