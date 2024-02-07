using Confluent.Kafka;
using Microservices.Posts.CQRS.Consumers;
using Microservices.Posts.Queries.Domain.Repositories;
using Microservices.Posts.Queries.Infrastructure.Consumers;
using Microservices.Posts.Queries.Infrastructure.DataAccess;
using Microservices.Posts.Queries.Infrastructure.Handlers;
using Microservices.Posts.Queries.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using EventHandler = Microservices.Posts.Queries.Infrastructure.Handlers.EventHandler;

var builder = WebApplication.CreateBuilder(args);

Action<DbContextOptionsBuilder> configureDbContext = o =>
{
    o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
};

builder.Services.AddDbContext<DatabaseContext>(configureDbContext);
builder.Services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));

//Create database and tables
CultureInfo.CurrentCulture = new CultureInfo("en-US");
var dataContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
dataContext.Database.EnsureCreated();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IEventHandler, EventHandler>();
builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection(nameof(ConsumerConfig)));
builder.Services.AddScoped<IEventConsumer, EventConsumer>();

builder.Services.AddControllers();
builder.Services.AddHostedService<ConsumerHostedService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
