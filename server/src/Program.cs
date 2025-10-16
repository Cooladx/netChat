

using SignalRWebpack.Hubs;



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
    options.PayloadSerializerOptions.TypeInfoResolver = new System.Text.Json.Serialization.Metadata.DefaultJsonTypeInfoResolver();

});


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
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
app.MapHub<ChatHub>("/hub");


// Starts application and only stops till it's shut down manually.
app.Run();

