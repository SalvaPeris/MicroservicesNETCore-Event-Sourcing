using Microservices.Posts.Commands.Api.Commands;
using Microservices.Posts.Commands.Domain.Aggregates;
using Microservices.Posts.Commands.Infrastructure.Config;
using Microservices.Posts.Commands.Infrastructure.Handlers;
using Microservices.Posts.Commands.Infrastructure.Repositories;
using Microservices.Posts.Commands.Infrastructure.Stores;
using Microservices.Posts.CQRS.Domain;
using Microservices.Posts.CQRS.Handlers;
using Microservices.Posts.CQRS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<PostAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();

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