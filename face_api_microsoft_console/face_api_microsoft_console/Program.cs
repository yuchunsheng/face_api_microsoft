using System;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System.Collections.ObjectModel;

namespace CSHttpClientSample
{
    static class Program
    {
        
        static void Main()
        {
            Console.Write("Enter the path to the JPEG image with faces to identify:");
            string imageFilePath = Console.ReadLine();

            //MakeDetectRequest(imageFilePath);
            CallFaceClientService(imageFilePath);

            Console.WriteLine("\n\n\nWait for the result below, then hit ENTER to exit...\n\n\n");
            Console.ReadLine();
        }

        static async void CallFaceClientService(string imageFilePath)
        {
            using (var fileStream = File.OpenRead(imageFilePath))
            {
                try
                {
                    string subscriptionKey = "??";
                    var faceServiceClient = new FaceServiceClient(subscriptionKey);
                    Microsoft.ProjectOxford.Face.Contract.Face[] faces = await faceServiceClient.DetectAsync(fileStream, false, true, new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Glasses });

                    Console.WriteLine("Response: Success. Detected {0} face(s) in {1}", faces.Length, imageFilePath);
                    Console.WriteLine(string.Format("{0} face(s) has been detected", faces.Length));

                    ObservableCollection<Face> DetectedFaces = new ObservableCollection<Face>();

                    foreach (var face in faces)
                    {
                        DetectedFaces.Add(new Face()
                        {
                            ImagePath = imageFilePath,
                            Left = face.FaceRectangle.Left,
                            Top = face.FaceRectangle.Top,
                            Width = face.FaceRectangle.Width,
                            Height = face.FaceRectangle.Height,
                            FaceId = face.FaceId.ToString(),
                            Gender = face.FaceAttributes.Gender,
                            Age = string.Format("{0:#} years old", face.FaceAttributes.Age),
                            IsSmiling = face.FaceAttributes.Smile > 0.0 ? "Smile" : "Not Smile",
                            Glasses = face.FaceAttributes.Glasses.ToString(),
                        });
                    }


                }
                catch (Exception ex) {
                    Console.WriteLine("Response: {0}", ex.Message);
                    if (ex is FaceAPIException )
                    {
                        Console.WriteLine("Response: {0}. {1}", ((FaceAPIException)ex).ErrorCode, ((FaceAPIException)ex).ErrorMessage);
                        return;
                    }

                    throw;
                }
                
                //catch (FaceAPIException ex)
                //{
                //    Console.WriteLine("Response: {0}. {}", ex.ErrorCode,  ex.ErrorMessage);
                //    return;
                //}
            }
        }

        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        static async void MakeDetectRequest(string imageFilePath)
        {
            var client = new HttpClient();

            // Request headers - replace this example key with your valid key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "????");

            // Request parameters.
            string queryString = "returnFaceId=true&returnFaceLandmarks=true&returnFaceAttributes=age,gender";

            // NOTE: You must use the same region in your REST call as you used to obtain your subscription keys.
            //   For example, if you obtained your subscription keys from westus, replace "westcentralus" in the 
            //   URI below with "westus".
            string uri = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect?" + queryString;

            HttpResponseMessage response;
            string responseContent;

            // Request body. Try this sample with a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                responseContent = response.Content.ReadAsStringAsync().Result;
            }

            //A peak at the JSON response.
            Console.WriteLine(responseContent);
        }
    }
}