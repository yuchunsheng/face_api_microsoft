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

            BitmapImage myBitmapImage = new BitmapImage(new Uri(photolocation));
            myBitmapImage.Freeze();

            this.face_doc_photo = myBitmapImage;
            
        }

        public String face_doc_id { get; set; }
        public BitmapImage face_doc_photo { get; set; }
    }
}
