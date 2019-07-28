using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace FaceNet
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var credential = new ApiKeyServiceClientCredentials("b225200629ab4207afa72cbecbc4dcb0");
            var client = new FaceClient(credential);
            client.Endpoint = "https://southeastasia.api.cognitive.microsoft.com";
            var attributes = new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Glasses };

            var stream = File.OpenRead("bond.jpg");
            var faces = await client.Face.DetectWithStreamAsync(stream, true, false, attributes);

            // var imageUrl = "https://thethaiger.com/wp-content/uploads/2019/07/66829455_640821486424524_965362129626464256_n.jpg";
            // var faces = await client.Face.DetectWithUrlAsync(imageUrl, true, false, attributes);

            foreach (var face in faces)
            {
                Console.WriteLine($"{face.FaceId}, {face.FaceRectangle.Left}, {face.FaceRectangle.Top}, {face.FaceRectangle.Width}, {face.FaceRectangle.Height}");
                Console.WriteLine($"{face.FaceAttributes.Gender}, {face.FaceAttributes.Age}, {face.FaceAttributes.Smile}, {face.FaceAttributes.Glasses}");                
            }
        }
    }
}
