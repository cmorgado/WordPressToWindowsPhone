using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP_to_WP.Domain.Interfaces
{
    public interface IStorage
    {

        Task<bool> FolderExists(string folderName);
        Task RemoveFolder(string folderName);
        Task RemoveFile(string fileName);
        Task<bool> Exists(string fileName);
        Task<bool> Exists(string fileName, TimeSpan expitation);
        Task<string> ReadData(string fileName);

        Task WriteData(string fileName, string content);
        Task WriteBinary(string folder, string fileName, byte[] content);
    }
}
