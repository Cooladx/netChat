using System.Text.Json.Serialization;
using Microsoft.Extensions.WebEncoders.Testing;

using Microsoft.AspNetCore.SignalR;
using SignalRWebpack.Hubs;


var builder = WebApplication.CreateSlimBuilder(args);


builder.Services.AddSignalR();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:5019")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials();
        });
});
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


// UseCors must be called before MapHub.
app.UseCors();

// Links url with endpoint hub for websocket.
app.MapHub<ChatHub>("/hub");


// Starts application and only stops till it's shut down manually.
app.Run();

