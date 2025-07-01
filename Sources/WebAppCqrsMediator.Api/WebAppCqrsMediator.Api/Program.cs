using Microsoft.EntityFrameworkCore;
using WebAppCqrsMediator.Domain.Entities;
using WebAppCqrsMediator.Domain.MessageQueues;
using WebAppCqrsMediator.Infrastructure;
using WebAppCqrsMediator.Infrastructure.Data;
using WebAppCqrsMediator.Infrastructure.MessageQueues;
using WebAppCqrsMediator.Mediator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMediatorService();
builder.Services.AddInfrastructureService(builder.Configuration);

builder.Services.AddDbContext<NodeDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection") ?? throw new InvalidOperationException("Connection string 'WebApiAppContext' not found.")));

builder.Services.AddScoped<IRabitMQProducer, RabitMQProducer>();
builder.Services.AddControllers();
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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<NodeDbContext>();
    context.Database.Migrate();
    if (!context.Nodes.Any())
    {
        context.Nodes.AddRange(
            new Node() { Id = Guid.Parse("8BF44F74-B153-47D0-9B79-FEB2443BF8F6"), Name = "00A", ParentId = Guid.Parse("6933BA92-88BC-4317-8C1C-4D1BE9CAC80C") },
            new Node() { Name = "00B", ParentId = Guid.Parse("8BF44F74-B153-47D0-9B79-FEB2443BF8F6") },
            new Node() { Name = "01C", ParentId = Guid.Parse("8BF44F74-B153-47D0-9B79-FEB2443BF8F6") }
        );
        context.SaveChanges();
    }
}

app.UseAuthorization();

app.MapControllers();

app.Run();
