using E_proc.Models;
using E_proc.Models.StatusModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_proc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        private readonly string AppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        private static List<FileRecord> fileDB = new List<FileRecord>();
        private readonly AuthContext _context;
        public FileManagerController(AuthContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostAsync ([FromForm] FileModel model,int? tenderId)
        {


            try
            {
                List<FileRecord> files = await SaveFileAsync(model.MyFile);

           
                    if (files.Count()!=0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        files[i].AltText = model.AltText;
                        files[i].Description = model.Description;
                    }
                   
                  
                    
                    var file=SaveToDB(files, tenderId);
                    return new Success(true,"message.success", file);
                }
                

                else
                    return new Success(false, "message.failed");
           
            }
            catch (Exception ex)
            {
                return new Success(false,"message."+ex.Message);
            }
        }


        private async Task<List<FileRecord>> SaveFileAsync(IFormFile[] myFile)
        {
            List<FileRecord >files = new List<FileRecord>();

            
                
            

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

        private FileData SaveToDB(List<FileRecord> record,int? TenderId=null)
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
       

        [HttpGet]
        public List<FileRecord> GetAllFiles()
        {
            //getting data from inmemory obj
            //return fileDB;
            //getting data from SQL DB
            return _context.FileData.Select(n => new FileRecord
            {
                Id = n.Id,
                ContentType = n.MimeType,
                FileFormat = n.FileExtention,
                FileName = n.FileName,
                FilePath = n.FilePath
            }).ToList();
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
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

            return File(memory, contentType, fileName);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
           var file= _context.FileData.Where(fd => fd.Id == id).FirstOrDefault();
            return new Success(true, "message.success", file);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteFile(int id)
        {
            var file = _context.FileData.Where(fd => fd.Id == id).FirstOrDefault();
            if (file != null)
            {
                _context.FileData.Remove(file);
                _context.SaveChanges();
                return new Success(true, "message.success", file);

            }
            return new Success(false, "message.failed", file);

        }


    }
}
