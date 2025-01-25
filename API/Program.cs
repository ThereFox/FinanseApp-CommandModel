using App;
using Application.Events.Realisation;
using Infrastructure.EvensSourcerer.DI;
using Infrastructure.TransactionalOutbox;
using Persistense;
using Persistense.Dapper.TransactionalOutbox.PollingPublisher;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
    .Configuration.GetValue<string>("ConnectionString");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddOutbox()
    .AddEventProducer()
    .AddPollingPublisher()
    .AddEventSender<ClientCreatedEvent>("ClientCreated")
    .AddEventSender<BillCreatedEvent>("BillCreated")
    .AddEventSender<BillAmountChangeEvent>("BillAmountChange");

builder.Services
    .AddAppService()
    .AddPersistense(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapSwagger();
}

app.Services.AddOutboxTableInitialiser();

app.UseAuthorization();

app.MapControllers();

app.Run();
