using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Web_fileSystem
{
    public class Web_folder : IWebItem
    {
        private bool _wasDeleted = false;
        private string _name = "";
        private string _path = "";
        private List<Web_file>? _files = null;
        private List<Web_folder>? _folders = null;
        private long _size = 0;
        private string _type = "folder";
        private Web_folder? _parentFolder = null;

        public const string root_name = "filesystem_webui";  // Parent to root folder
        public string Name
        {
            get
            {
                CheckDeleted();
                return _name;
            }
            set
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
        public List<Web_file>? Files
        {
            get
            {
                CheckDeleted();
                return _files;
            }
            private set
            {
                CheckDeleted();
                _files = value;
            }
        }
        public List<Web_folder>? Folders
        {
            get
            {
                CheckDeleted();
                return _folders;
            }
            private set
            {
                CheckDeleted();
                _folders = value;
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

        public Web_folder(string folderPath, Web_folder? parentFolder = null)
        {
            var directoryInfo = new DirectoryInfo(folderPath);
            if (!directoryInfo.Exists)
            {
                throw new DirectoryNotFoundException($"The directory {folderPath} does not exist.");
            }
            else
            {
                Name = directoryInfo.Name;
                Path = folderPath;
                ParentFolder = parentFolder;

                Files = Directory.GetFiles(folderPath)
                                 .Select(filePath => new Web_file(filePath, this))
                                 .ToList();
                Folders = Directory.GetDirectories(folderPath)
                                   .Select(folderPath => new Web_folder(folderPath, this))
                                   .ToList();

                Size = Files.Sum(file => file.Size) + Folders.Sum(folder => folder.Size);
            }


        }

        public Web_file? FindFile(string name)
        {
            CheckDeleted();
            return Files?.FirstOrDefault(file => file.Name == name);
        }

        public void Delete_file(string name)
        {
            CheckDeleted();
            Web_file? file = FindFile(name);
            if (file != null)
            {
                file.Delete();        // Delete on local fs
                Files?.Remove(file);  // Remove Files object from list of this folder
            }
        }

        private void DeleteAllFiles()
        {
            CheckDeleted();
            if (Files != null)
            {
                foreach (var file in Files)
                {
                    file.Delete();
                }
                Files.Clear();
            }
        }

        public void Delete()
        {
            CheckDeleted();

            // Delete all files in the folder
            DeleteAllFiles();

            // Recurse into any subfolders and delete them
            if (Folders != null)
            {
                foreach (var folder in Folders)
                {
                    folder.Delete();
                }
                Folders.Clear();
            }

            // Delete the folder itself
            Directory.Delete(Path);
            _wasDeleted = true;
        }

        private void CheckDeleted()
        {
            if (_wasDeleted)
            {
                throw new InvalidOperationException("Cannot access a deleted folder.");
            }
        }

        public List<object> GetContents()
        {
            CheckDeleted();
            var items = new List<object>();
            if (Folders != null)
            {
                items.AddRange(Folders);
            }
            if (Files != null)
            {
                items.AddRange(Files);
            }
            return items;
        }
    }
}