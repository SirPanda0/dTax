using dTax.Auth;
using dTax.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using dTax.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace dTax.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FileStorageController : BaseUtilsController
    {

        private IFileStorageRepository fileStorageRepository;
        public FileStorageController(IFileStorageRepository injectedfileStorageRepository)
        {
            fileStorageRepository = injectedfileStorageRepository;
        }

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadFile(IFormFile uploadedFile)
        {
            Log.Information("Загружаем файл");

            long id = 0;

            if (uploadedFile != null)
            {
                byte[] fileData = null;
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await uploadedFile.CopyToAsync(memoryStream);
                        fileData = memoryStream.ToArray();
                    }

                    var fileStorage = new FileStorage()
                    {
                        ContentData = fileData,
                        FileName = uploadedFile.FileName
                        //Drivers =uploadedFile.
                    };
                    fileStorageRepository.Insert(fileStorage);
                    fileStorageRepository.Commit();
                    id = fileStorage.FileId;
                    Log.Debug("Успешно загружено outdata id = {0}", id);
                }
                catch (Exception e)
                {
                    Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                    BadRequest();
                }
            }
            else
            {
                //Logging.Logger.Log.InfoFormat("uploadedFile = null");
                Log.Information("uploadedFile = null");
            }
            return Ok(id);
        }


        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [HttpGet]
        [Route("Download")]
        public IActionResult DownloadFile(long id)
        {
            var fileStorageModel = fileStorageRepository.GetById(id);
            if (fileStorageModel != null)
            {
                var content = fileStorageModel.ContentData;
                var contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
                string fileName = fileStorageModel.FileName;
                return File(content, contentType, fileName);
            }
            else
            {
                return NotFound("Файл отсутсвует");
            }
        }





    }
}

