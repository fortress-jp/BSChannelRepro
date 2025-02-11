using BSChannelRepro;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<BackgroundWorkerService>();
builder.Services.AddSingleton<SyncChannel>();
builder.Services.AddLogging();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
