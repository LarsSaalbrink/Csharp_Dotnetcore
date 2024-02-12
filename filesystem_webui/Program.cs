using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();  // Use the static files middleware to serve web src from local files

app.MapGet("/", () => Results.File("index.html", "text/html"));
app.MapGet("/read_fs", async (HttpContext context) =>
{
    var folderPath = "fs_content"; // Local folder containing the filesystem displayed
    try
    {
        // Get file list
        var files = Directory.GetFiles(folderPath);
        Console.WriteLine("Files:");
        await context.Response.WriteAsync("\nFiles:\n");
        foreach (var file in files)
        {
            Console.WriteLine(file);
            await context.Response.WriteAsync(file + "\n");
        }

        // Get directory list
        var directories = Directory.GetDirectories(folderPath);
        Console.WriteLine("Directories:");
        await context.Response.WriteAsync("\nDirectories:\n");
        foreach (var directory in directories)
        {
            Console.WriteLine(directory);
            await context.Response.WriteAsync(directory + "\n");
        }
    }
    catch (Exception ex)
    {
        await context.Response.WriteAsync($"An error occurred: {ex.Message}");
    }
});

app.Run();