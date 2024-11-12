using App;
using Application.Events.Realisation;
using Infrastructure.EvensSourcerer.DI;
using Persistense;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
    .Configuration.GetValue<string>("ConnectionString");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddEventSourcerer()
    .AddEventProducer<ClientCreatedEvent>("ClientCreated")
    .AddEventProducer<BillCreatedEvent>("BillCreated")
    .AddEventProducer<BillAmountChangeEvent>("BillAmountChange");

builder.Services
    .AddAppService()
    .AddPersistense(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
