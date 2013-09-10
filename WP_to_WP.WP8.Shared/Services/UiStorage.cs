using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WP_to_WP.Domain.Code;

namespace WP_to_WP.Shared.Services
{
    public class UiStorage : Domain.Interfaces.IStorage
    {
        Cimbalino.Phone.Toolkit.Services.AsyncStorageService storage = new Cimbalino.Phone.Toolkit.Services.AsyncStorageService();

        private static readonly StorageFolder Storage = ApplicationData.Current.LocalFolder;

        public Task<bool> FolderExists(string folderName)
        {
            return storage.DirectoryExistsAsync(folderName);
        }

        public Task RemoveFolder(string folderName)
        {
            return storage.DeleteDirectoryAsync(folderName);
        }

        public async Task<bool> Exists(string fileName)
        {
            return await storage.FileExistsAsync(fileName);
            //

            //try
            //{
            //    var file = await Storage.GetFileAsync(fileName);

            //    return file != null;
            //}
            //catch (FileNotFoundException)
            //{
            //    return false;
            //}
        }

        public Task<bool> Exists(string fileName, TimeSpan expitation)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ReadData(string fileName)
        {
            var r = await storage.ReadAllTextAsync(fileName);
            return r;
        }

        public Task WriteData(string fileName, string content)
        {
            return storage.WriteAllTextAsync(fileName, content);
        }

        public Task WriteBinary(string folder, string fileName, byte[] content)
        {
            return storage.WriteAllBytesAsync(folder + fileName, content);
        }

        public Task RemoveFile(string StorageId)
        {
            return storage.DeleteFileAsync(StorageId);
        }
    }
}
