using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using netChat;
using Npgsql;
using netChat.Classes;

var builder = WebApplication.CreateSlimBuilder(args);

// debugging for terminal.
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Add debugging for SignalR and json serialization.
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
}).AddJsonProtocol(options =>
{
    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
    options.PayloadSerializerOptions.TypeInfoResolver = new NetChatContext();
});

// Sets up our connection to the database
var sql_client_builder = new NpgsqlConnectionStringBuilder
{
    Host = "127.0.0.1:5432",
    Username = "postgres",
    Password = "<password>",
    Database = "netChatDB"
};

builder.Services.AddDbContext<NetChatDBContext>(options => options.UseNpgsql(sql_client_builder.ConnectionString));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

// Builds web app
var app = builder.Build();

// Allows server to locate and serve index.html
app.UseDefaultFiles();
app.UseStaticFiles();

// UseCors must be called before MapHub.
app.UseCors();
// Links url with endpoint hub for websocket.
app.MapHub<netChat.Hubs.ChatHub>("/hub");

// Starts application and only stops till it's shut down manually.
app.Run();

namespace netChat
{
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(float))]
    [JsonSerializable(typeof(double))]
    [JsonSerializable(typeof(long))]
    [JsonSerializable(typeof(int))]
    [JsonSerializable(typeof(bool))]
    public partial class NetChatContext : JsonSerializerContext { }

    public class NetChatDBContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> UserDB { get; set; }
    }
}