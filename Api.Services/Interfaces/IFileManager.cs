using System;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IFileManager
    {
        bool CreateFile();

        bool DelteFiles();

        Task<bool> SaveToFileAsync(string message);

        Task<string[]> GetAllLinesAsync();
    }
}
