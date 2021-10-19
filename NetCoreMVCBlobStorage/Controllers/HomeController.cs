using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using NetCoreMVCBlobStorage.Models;
using NetCoreMVCBlobStorage.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMVCBlobStorage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ImageService _imageService;
        public HomeController(ILogger<HomeController> logger, ImageService imageService)
        {
            _logger = logger;
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile image)
        {
            StorageConfig storageConfig = new StorageConfig();
            storageConfig.AccountKey = "yJTolZwqJSvr+ulkcZPRNWtmNWTI97P0grwXRE6TQolgGXuZXHQGv/3HDcuyaV9e2FK32NukNN5682WGqq4n6A==";
            storageConfig.AccountName = "demostorage1291";
            storageConfig.ImageContainer = "images";

            if (image != null)
            {
                using (var stream = image.OpenReadStream())
                {
                    var imageUri = await _imageService.UploadFileToStorage(stream, image.FileName, storageConfig);
                    return RedirectToAction("show", new { imageUri });
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult show(Uri imageUri)
        {
            ImageModel model = new ImageModel();
            model.uri = imageUri;
            return View(model);

            //StorageConfig storageConfig = new StorageConfig();
            //storageConfig.AccountKey = "jBRHwJoxMLrdjVnqjlKIOQ0NS+0LJAh2wcEUJgiOEgLa+q9UMhAE/jzdlSh5tW5KoP2jtk8jMS+mvV7AKyofxQ==";
            //storageConfig.AccountName = "tempstorage1291";
            //storageConfig.ImageContainer = "images";
            //_imageService.RetriveImages(storageConfig, imageUri);
            //return View();       
        }
    }
}
