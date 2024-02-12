using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Web_fileSystem
{
    public class Web_folder
    {
        private bool _wasDeleted = false;
        private string _name = "";
        private string _path = "";
        private List<Web_file>? _files = null;
        private long _size = 0;
        private Web_folder? _parentFolder = null;

        public const string root_name = "fs_content";  // Folder to use as root for the fs
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

        public Web_folder(string folderPath)
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

                // Limit access to within the fs folder for this project 
                // #pragma warning disable CS8602  // If directoryInfo.Exists, this can never happen
                ParentFolder = (directoryInfo?.Parent?.Name == root_name) ? ParentFolder = null : ParentFolder = new Web_folder(folderPath);
                // #pragma warning restore CS8602

                Files = Directory.GetFiles(folderPath)
                                 .Select(filePath => new Web_file(filePath, this))
                                 .ToList();

                Size = Files.Sum(file => file.Size);
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
                file.Delete();       // Delete on local fs
                Files?.Remove(file);  // Remove Files object from list of this folder
            }
        }

        private void CheckDeleted()
        {
            if (_wasDeleted)
            {
                throw new InvalidOperationException("Cannot access a deleted folder.");
            }
        }
    }
}