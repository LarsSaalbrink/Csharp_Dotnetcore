using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Web_fileSystem
{
    public class Web_folder
    {
        public const string root_name = "fs_content";  // Folder to use as root for the fs
        public string Name { get; private set; }
        public string Path { get; private set; }
        public List<Web_file> Files { get; private set; }
        public long Size { get; private set; }  //Bytes
        public Web_folder? ParentFolder { get; private set; }

        public Web_folder(string folderPath)
        {
            var directoryInfo = new DirectoryInfo(folderPath);
            Name = directoryInfo.Name;
            Path = folderPath;

            // Limit access to within the fs folder for this project 
            ParentFolder = (directoryInfo.Parent.Name == root_name) ? ParentFolder = null : ParentFolder = new Web_folder(directoryInfo.Parent.FullName);

            Files = Directory.GetFiles(folderPath)
                             .Select(filePath => new Web_file(filePath, this))
                             .ToList();

            Size = Files.Sum(file => file.Size);
        }

        public Web_file? FindFile(string name)
        {
            return Files.FirstOrDefault(file => file.Name == name);
        }

        public void Delete_file(string name)
        {
            Web_file file = FindFile(name);
            if (file != null)
            {
                file.Delete();       // Delete on local fs
                Files.Remove(file);  // Remove Files object from list of this folder
            }
        }
    }
}