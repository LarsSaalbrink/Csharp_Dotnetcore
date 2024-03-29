using System;
using System.IO;

namespace Web_fileSystem
{
    public class Web_file : IWebItem
    {
        private string _name = "";
        private string _path = "";
        private long _size = 0;
        private string _type = "file";
        private Web_folder? _parentFolder = null;
        private bool _wasDeleted = false;

        public string Name
        {
            get
            {
                CheckDeleted();
                return _name;
            }
            private set
            {
                CheckDeleted();
                _name = value;
            }
        }

        public string Path
        {
            get
            {
                CheckDeleted();
                return _path;
            }
            private set
            {
                CheckDeleted();
                _path = value;
            }
        }

        public long Size
        {
            get
            {
                CheckDeleted();
                return _size;
            }
            private set
            {
                CheckDeleted();
                _size = value;
            }
        }

        public string type
        {
            get
            {
                CheckDeleted();
                return _type;
            }
        }

        public Web_folder? ParentFolder
        {
            get
            {
                CheckDeleted();
                return _parentFolder;
            }
            private set
            {
                CheckDeleted();
                _parentFolder = value;
            }
        }

        public Web_file(string filePath, Web_folder parentFolder)
        {
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("The file does not exist.", filePath);
            }
            else
            {
                Name = fileInfo.Name;
                Path = filePath;
                Size = fileInfo.Length;
                ParentFolder = parentFolder;
            }
        }

        public void Delete()
        {
            Console.WriteLine("Deleting file: '" + Name + "'");
            File.Delete(Path);
            _wasDeleted = true;
        }

        private void CheckDeleted()
        {
            if (_wasDeleted)
            {
                throw new InvalidOperationException("Cannot access a deleted file.");
            }
        }
    }
}