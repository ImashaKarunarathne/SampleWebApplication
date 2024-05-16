using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleWebApplication.Data;
using Microsoft.Azure.Cosmos;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SampleWebApplicationContext>(options =>
    options.UseCosmos(builder.Configuration.GetConnectionString("SampleWebApplicationContext") ?? throw new InvalidOperationException("Connection string 'SampleWebApplicationContext' not found."), "DATABASE_NAME"));

// Add services to the container.

builder.Services.AddControllers();
var services = builder.Services;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Cosmos DB client
//services.AddSingleton<CosmosClient>(InitializeCosmosClientInstance(builder.Configuration.GetSection("CosmosDB")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();

static CosmosClient InitializeCosmosClientInstance(IConfigurationSection configurationSection)
{
    string endpointUri = configurationSection.GetSection("EndpointUri").Value; 
    string primaryKey = configurationSection.GetSection("PrimaryKey").Value;  
    return new CosmosClient(endpointUri, primaryKey);
}

static Container InitializeCosmosContainer()
{
    var cosmosClient = new CosmosClient("<connection-string>");
    var database = cosmosClient.GetDatabase("<database-name>");
    var container = database.GetContainer("<container-name>");

    return container;
}
