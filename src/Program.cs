using System.Text.Json.Serialization;
using Microsoft.Extensions.WebEncoders.Testing;

using Microsoft.AspNetCore.SignalR;
using SignalRWebpack.Hubs;


var builder = WebApplication.CreateSlimBuilder(args);


builder.Services.AddSignalR();



// Builds web app
var app = builder.Build();

// Allows server to locate and serve index.html
app.UseDefaultFiles();
app.UseStaticFiles();


var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};

app.UseWebSockets(webSocketOptions);

// Links url with endpoint hub for websocket.
app.MapHub<ChatHub>("/hub");


// Starts application and only stops till it's shut down manually.
app.Run();

// builder.Services.ConfigureHttpJsonOptions(options =>
// {
//     options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
// });

// public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

// [JsonSerializable(typeof(Todo[]))]
// internal partial class AppJsonSerializerContext : JsonSerializerContext
// {

// }
