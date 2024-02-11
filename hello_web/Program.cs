var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseStaticFiles();  // Use the static files middleware to serve web src from local files

app.MapGet("/", () => Results.File("index.html", "text/html"));
app.MapGet("/test", () => Results.File("test", "text/plain"));
app.Run();
