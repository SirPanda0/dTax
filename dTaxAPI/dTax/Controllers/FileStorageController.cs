using dTax.Auth;
using dTax.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using dTax.Entity.Models;
using dTax.Data.Interfaces;
using dTax.Entity.Models.FileContents;
using dTax.Entity.Models.FileStorages;

namespace dTax.Controllers
{

    [Route("api/[controller]")]
    public class FileStorageController : BaseUtilsController
    {

        private IFileStorageRepository fileStorageRepository;
        private IFileContentRepository fileContentRepository;
        public FileStorageController(IFileStorageRepository injectedfileStorageRepository, IFileContentRepository injectedfileContentRepository)
        {
            fileStorageRepository = injectedfileStorageRepository;
            fileContentRepository = injectedfileContentRepository;
        }

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadFile(IFormFile uploadedFile)
        {
            Log.Information("Загружаем файл");

            Guid id = Guid.Empty;

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

                    FileContent filecontent = new FileContent()
                    {
                        ContentData = fileData
                    };
                    fileContentRepository.Insert(filecontent);
                    fileContentRepository.Commit();

                    FileStorage fileStorage = new FileStorage()
                    {
                        FileContentId = filecontent.Id,
                        FileName = uploadedFile.FileName

                        //Drivers =uploadedFile.
                    };
                    fileStorageRepository.Insert(fileStorage);
                    fileStorageRepository.Commit();
                    id = fileStorage.Id;
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


        [PolicyAuthorize(AuthorizePolicy.Operator)]
        [HttpGet]
        [Route("Download")]
        public IActionResult DownloadFile(Guid id)
        {
            var fileStorageModel = fileStorageRepository.GetById(id);
            if (fileStorageModel != null)
            {
                var content = fileStorageModel.FileContent.ContentData;
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

