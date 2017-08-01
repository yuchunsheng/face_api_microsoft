using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace face_api_wpf_support.ViewModels
{
    public class FaceDocItem
    {
        public FaceDocItem(string face_doc_id, string face_doc_uri)
        {
            this.face_doc_id = face_doc_id.ToString();
            string photolocation = face_doc_uri + face_doc_id + ".jpg";  //file name

            //BitmapImage myBitmapImage = new BitmapImage(new Uri(photolocation));
            //myBitmapImage.Freeze();

            //this.face_doc_photo = myBitmapImage;

            var image = new BitmapImage();

            using (var stream = new FileStream(photolocation, FileMode.Open))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                //Makes the current object unmodifiable and sets its IsFrozen property to true.
                image.Freeze();
            }

            this.face_doc_photo = image;

        }

        public String face_doc_id { get; set; }
        public BitmapImage face_doc_photo { get; set; }

        public void CreatePhoto(byte[] photoSource)
        {

            BitmapImage photo = new BitmapImage();
            MemoryStream strm = new MemoryStream();

            int offset = 78;
            strm.Write(photoSource, offset, photoSource.Length - offset);

            // Read the image into the bitmap object
            photo.BeginInit();
            photo.StreamSource = strm;
            photo.EndInit();
            photo.Freeze();

        }
    }
}
