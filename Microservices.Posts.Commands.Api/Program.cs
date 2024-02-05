using Microservices.Posts.Commands.Infrastructure.Config;
using Microservices.Posts.Commands.Infrastructure.Repositories;
using Microservices.Posts.Commands.Infrastructure.Stores;
using Microservices.Posts.CQRS.Domain;
using Microservices.Posts.CQRS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventStore, EventStore>();

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

app.Run();