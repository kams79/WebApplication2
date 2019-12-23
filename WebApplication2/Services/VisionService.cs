using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Services
{
    public class VisionService
    {
        public async Task<string> GetAnalysisAsync(string imageUrl)
        {
            List<VisualFeatureTypes> features = new List<VisualFeatureTypes>()
            {
              VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
              VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
              VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
              VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
              VisualFeatureTypes.Objects
            };

            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials("0d38d688442d4918a4b0d8f0866a0c27"))
            { Endpoint = "https://cvision-dicoding.cognitiveservices.azure.com/" };

            var results = await client.AnalyzeImageAsync(imageUrl, features);

            foreach (var caption in results.Description.Captions)
            {
                return caption.Text;
            }

            return null;
        }
    }
}
