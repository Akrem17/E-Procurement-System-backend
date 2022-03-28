using E_proc.Models;
using E_proc.Models.StatusModel;
using E_proc.Repositories.Interfaces;
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
        private IFileDataRepository _fileRepository;
        public FileManagerController(AuthContext context, IFileDataRepository fileRepository)
        {
            _context = context;
            _fileRepository = fileRepository;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostAsync ([FromForm] FileModel model,int? tenderId)
        {


            try
            {
                List<FileRecord> files = await _fileRepository.SaveFileAsync(model.MyFile);

           
                    if (files.Count()!=0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        files[i].AltText = model.AltText;
                        files[i].Description = model.Description;
                    }
                   
                  
                    
                    var file= _fileRepository.SaveToDB(files, tenderId);
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


      


        [HttpGet]
        public List<FileRecord> GetAllFiles()
        {
      
            return _fileRepository.GetAllFiles();

         
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
           
            ReturnedFile returnedFile= await _fileRepository.DownloadFile(id);
            if(returnedFile!=null)
            return File(returnedFile.memory, returnedFile.contentType, returnedFile.fileName);
            return new Success(false, "message.failed");

        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
           var file = await _fileRepository.GetFile(id);  
            if(file!=null)
            return new Success(true, "message.success", file);
            return new Success(false, "message.notFound");


        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteFile(int id)
        {
            var file = await _fileRepository.deleteFile(id);
            if (file != null)
            {
              
                return new Success(true, "message.success", file);

            }
            return new Success(false, "message.failed");

        }


    }
}
