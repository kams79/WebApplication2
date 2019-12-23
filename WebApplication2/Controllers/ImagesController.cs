using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("[controller]/[action]")]
    public class ImagesController : Controller
    {
        private readonly ImageStore imageStore;
        private readonly VisionService visionService;

        public ImagesController(ImageStore imageStore, VisionService visionService)
        {
            this.imageStore = imageStore;
            this.visionService = visionService;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile image)
        {
            if (image != null)
            {
                using (var stream = image.OpenReadStream())
                {
                    var imageId = await imageStore.SaveImage(stream);
                    return RedirectToAction("Show", new { imageId });
                }
            }
            return View();
        }

        [HttpGet("{imageId}")]
        public async Task<IActionResult> Show(string imageId)
        {
            var imageUrl = imageStore.UriFor(imageId);
            string analyze = await visionService.GetAnalysisAsync(imageUrl);
            var model = new ShowModel
            {
                Uri = imageUrl,
                Analysis = analyze
            };

            return View(model);
        }

    }
}
