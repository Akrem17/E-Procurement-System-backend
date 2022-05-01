using E_proc.Models;

namespace E_proc.Repositories.Interfaces
{
    public interface IFileDataRepository
    {
        Task<List<FileRecord>> SaveFileAsync(IFormFile[] myFile);

        FileData SaveToDB(List<FileRecord> record, int? TenderId = null);

        List<FileRecord> GetAllFiles();
        Task<ReturnedFile> DownloadFile(int id);

        Task<FileData> GetFile(int id);
        Task<FileData> deleteFile(int id);
        FileData SaveToDBForOffer(List<FileRecord> record, int? OfferId = null);

        FileData updateFile( int? fileId = null);



    }
}
