using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Xamarin.Forms;

namespace FaceTest
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private const string subscriptionKey = "12ad77b056ca473680f31ee5c15d6e4a";
        private const string faceEndpoint = "https://southeastasia.api.cognitive.microsoft.com/";        
        private const string remoteImageUrl = "https://static.independent.co.uk/s3fs-public/thumbnails/image/2018/03/18/15/billgates.jpg";

        private static readonly FaceAttributeType[] faceAttributes = { FaceAttributeType.Age, FaceAttributeType.Gender };
        private FaceClient faceClient;

        public MainPage()
        {
            InitializeComponent();

            var credential = new ApiKeyServiceClientCredentials(subscriptionKey);           
            faceClient = new FaceClient(credential);
            faceClient.Endpoint = faceEndpoint;
        }

        protected override async void OnAppearing()
        {
            var faces = await faceClient.Face.DetectWithUrlAsync(remoteImageUrl, true, false, faceAttributes);
            foreach (var face in faces)
            {
                label.Text = face.FaceAttributes.Gender.ToString();
            }
        }
    }
}
