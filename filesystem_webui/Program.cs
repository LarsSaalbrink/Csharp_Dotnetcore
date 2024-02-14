using System;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Web_fileSystem;  // Custom filesystem implementation

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();  // Use the static files middleware to serve web src from local files

app.MapGet("/", () => Results.File("index.html", "text/html"));
app.MapGet("/read_fs", async (HttpContext context) =>
{
    var folderName = "fs_content"; // Local folder containing the filesystem displayed
    try
    {
        string fsRootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "fs_content");
        var rootFolder = new Web_folder(fsRootFolderPath);

        // Generate a list of files and folders representing the filesystem
        var fileSystem = new List<IWebItem>();
        var folders = new Stack<Web_folder>();  // Create a stack for folders to be processed
        folders.Push(rootFolder);

        while (folders.Count > 0)
        {
            var currentFolder = folders.Pop();
            fileSystem.Add(currentFolder);

            foreach (var file in currentFolder.Files)
            {
                fileSystem.Add(file);
            }

            foreach (var subfolder in currentFolder.Folders)
            {
                folders.Push(subfolder);
            }
        }


        var jsonString = JsonSerializer.Serialize(fileSystem);

        Console.WriteLine("Responding with JSON of files: " + jsonString);
        await context.Response.WriteAsync(jsonString);
    }
    catch (Exception ex)
    {
        await context.Response.WriteAsync($"An error occurred: {ex.Message}");
    }
});

app.Run();