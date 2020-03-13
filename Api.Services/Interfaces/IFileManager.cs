using System;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IFileManager
    {
        bool CreateFile();

        bool DeleteFiles();

        string[] GetFiles();

        Task<bool> SaveToFileAsync(string message);

        Task<string[]> GetAllLinesAsync();
    }
}
