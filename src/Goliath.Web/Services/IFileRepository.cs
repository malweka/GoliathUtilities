using System.IO;

namespace Goliath.Web.Services
{
    public interface IFileRepository
    {
        void CreatDirectory(string directoryName);

        void DeleteDirectory(string directoryName);

        bool DirectoryExists(string directoryName);

        string[] GetFiles(string directory, string filter = null);

        void CreateFile(Stream fileStream, string filePath);

        void DeleteFile(string filePath);

        string RootDirectory { get; }

        bool FileExists(string fileName);

        Stream GetFile(string filePath);


    }
}