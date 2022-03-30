using E_proc.Models;
using E_proc.Repositories.Interfaces;

namespace E_proc.Repositories.Implementations
{
    public class FileDataRepository: IFileDataRepository
    {

        private readonly string AppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        private static List<FileRecord> fileDB = new List<FileRecord>();
        private readonly AuthContext _context;
        public FileDataRepository( AuthContext athContext )
        {

            _context = athContext;

        }
        public async Task<List<FileRecord>> SaveFileAsync(IFormFile[] myFile)
        {
            List<FileRecord> files = new List<FileRecord>();





            if (myFile != null)
            {
                for (int i = 0; i < myFile.Length; i++)
                {
                    FileRecord file = new FileRecord();

                    if (!Directory.Exists(AppDirectory))
                        Directory.CreateDirectory(AppDirectory);

                    var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(myFile[i].FileName);
                    var path = Path.Combine(AppDirectory, fileName);

                    file.Id = fileDB.Count() + 1;
                    file.FilePath = path;
                    file.FileName = fileName;
                    file.FileFormat = Path.GetExtension(myFile[i].FileName);
                    file.ContentType = myFile[i].ContentType;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await myFile[i].CopyToAsync(stream);
                    }

                    files.Add(file);

                }
                return files;
            }
            return files;


        }

        public FileData SaveToDB(List<FileRecord> record, int? TenderId = null)
        {
            if (record == null)
                throw new ArgumentNullException($"{nameof(record)}");

            List<FileData> filesData = new List<FileData>();
            for (int i = 0; i < record.Count; i++)
            {
                FileData fileData = new FileData();
                fileData.FilePath = record[i].FilePath;
                fileData.FileName = record[i].FileName;
                fileData.FileExtention = record[i].FileFormat;
                fileData.MimeType = record[i].ContentType;
                fileData.TenderId = TenderId;
                filesData.Add(fileData);

            }
            _context.FileData.AddRange(filesData);
            _context.SaveChanges();

            return filesData[0];
        }

        public FileData SaveToDBForOffer(List<FileRecord> record, int? OfferId = null)
        {
            if (record == null)
                throw new ArgumentNullException($"{nameof(record)}");

            List<FileData> filesData = new List<FileData>();
            for (int i = 0; i < record.Count; i++)
            {
                FileData fileData = new FileData();
                fileData.FilePath = record[i].FilePath;
                fileData.FileName = record[i].FileName;
                fileData.FileExtention = record[i].FileFormat;
                fileData.MimeType = record[i].ContentType;
                fileData.OfferId = OfferId;
                filesData.Add(fileData);

            }
            _context.FileData.AddRange(filesData);
            _context.SaveChanges();

            return filesData[0];
        }


        public List<FileRecord> GetAllFiles()
        {
            return _context.FileData.Select(n => new FileRecord
            {
                Id = n.Id,
                ContentType = n.MimeType,
                FileFormat = n.FileExtention,
                FileName = n.FileName,
                FilePath = n.FilePath
            }).ToList();
        }


        public  async Task<ReturnedFile> DownloadFile(int id)
        {
            if (!Directory.Exists(AppDirectory))
                Directory.CreateDirectory(AppDirectory);

            //getting file from inmemory obj
            //var file = fileDB?.Where(n => n.Id == id).FirstOrDefault();
            //getting file from DB
            var file = _context.FileData.Where(n => n.Id == id).FirstOrDefault();

            var path = Path.Combine(AppDirectory, file?.FilePath);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var contentType = "APPLICATION/octet-stream";
            var fileName = Path.GetFileName(path);
            ReturnedFile returnedFile = new ReturnedFile();
            returnedFile.memory = memory;
            returnedFile.fileName = fileName;
            returnedFile.contentType=contentType;

            return  returnedFile;
        }

        public async Task<FileData> GetFile(int id)
        {
            var file = _context.FileData.Where(fd => fd.Id == id).FirstOrDefault();
            return file;

        }
        public async Task<FileData> deleteFile(int id)
        {
            var file = _context.FileData.Where(fd => fd.Id == id).FirstOrDefault();
            if (file != null)
            {
                _context.FileData.Remove(file);
                _context.SaveChanges();
                

            }
            return file;

        }


    }



}
