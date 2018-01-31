using ComicsManager.Common;
using ComicsManager.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ComicsManager.BackOffice.Controllers
{
    /// <summary>
    /// Contrôleur dédié à l'upload de fichier
    /// </summary>
    public class FilesController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FilesController(
            ComicsManagerContext context,
            IOptions<AppSettings> config,
            IHostingEnvironment hostingEnvironment)
            : base(context, config)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            var result = new List<Guid>();

            var files = HttpContext.Request.Form.Files;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileEntity = new Model.Models.File
                    {
                        Type = Path.GetExtension(file.ContentDisposition)
                    };

                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        fileEntity.Path = memoryStream.ToArray();
                    }

                    _context.Files.Add(fileEntity);
                    await _context.SaveChangesAsync();

                    result.Add(fileEntity.Id);
                }
            }

            TempData.Add(Constants.UploadedFilesTempDataKey, result);

            return Ok();
        }
    }
}