using System.IO;

namespace Web_fileSystem
{
    public class Web_file
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public long Size { get; private set; }
        public Web_folder ParentFolder { get; private set; }

        public Web_file(string filePath, Web_folder parentFolder)
        {
            var fileInfo = new FileInfo(filePath);
            Name = fileInfo.Name;
            Path = filePath;
            Size = fileInfo.Length;
            ParentFolder = parentFolder;
        }

        public void Delete()
        {
            File.Delete(Path);
            Console.PrintLine("Deleted file: '" + Name + "'");
        }
    }
}