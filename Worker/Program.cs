using Microsoft.Extensions.Configuration;
using Shared;
using Shared.Models;
using Worker;
var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<WorkerConfiguration>(builder.Configuration.GetSection(nameof(ServiceConfigurations.WorkerConfiguration)));
builder.Services.AddSingleton<IWorkerFactory, WorkerFactory>();
builder.Services.AddHostedService<Worker.ProductEmailConsumer>();
builder.Services.AddHostedService<Worker.ProductNotiConsumer>();

var host = builder.Build();
host.Run();
