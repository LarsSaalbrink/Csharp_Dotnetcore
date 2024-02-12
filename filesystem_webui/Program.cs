using System;
using System.IO;
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
    var folderPath = "fs_content"; // Local folder containing the filesystem displayed
                                   // try
                                   // {


    // Get directory list
    var directoryPaths = Directory.GetDirectories(folderPath);
    var directories = directoryPaths.Select(path => new Web_folder(path)).ToList();
    Console.WriteLine("Directories:");
    await context.Response.WriteAsync("\nDirectories:\n");
    foreach (var directory in directories)
    {
        Console.WriteLine(directory.Name);
        await context.Response.WriteAsync(directory.Name + "\n");
    }

    directories[0].Delete_file("apple.fruit");



    // Get file list
    var filePaths = Directory.GetFiles(folderPath);
    var files = filePaths.Select(path => new Web_file(path, null)).ToList();
    Console.WriteLine("Files:");
    await context.Response.WriteAsync("\nFiles:\n");
    foreach (var file in files)
    {
        Console.WriteLine(file.Name);
        await context.Response.WriteAsync(file.Name + "\n");
    }




    // }
    // catch (Exception ex)
    // {
    //     await context.Response.WriteAsync($"An error occurred: {ex.Message}");
    // }
});

app.Run();