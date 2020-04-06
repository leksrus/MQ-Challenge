using System;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IFileManager
    {
        void CreateFile(string fileName, string filesRoute);

        void DeleteFiles(string fileRoute);

        string[] GetFiles(string fileRoute);

        Task<bool> SaveToFileAsync(string message, string fileName, string filesRoute);

        Task<string[]> GetAllLinesAsync(string fileName, string filesRoute);
    }
}
