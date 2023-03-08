using KeyValApi.Helpers;
using KeyValApi.Models;
using KeyValApi.Services;
using KeyValApi.Settings;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<IMongoCollection<KeyValModel>>(sp =>
{

    var mongoConfig = builder.Configuration.GetSection("MongoDB");

    var mongoClient = new MongoClient(mongoConfig.GetValue<string>("ConnectionString"));

    return mongoClient
        .GetDatabase(mongoConfig.GetValue<string>("DatabaseName"))
        .GetCollection<KeyValModel>(mongoConfig.GetValue<string>("KeyValCollectionName"));
});

builder.Services.AddSingleton<KeyValHelpers>();
builder.Services.AddSingleton<IKeyValService, KeyValService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
