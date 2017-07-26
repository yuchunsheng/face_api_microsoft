using face_api_commons.Common;
using face_api_wpf_support.Views;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace face_api_wpf_support.ViewModels.business_face_photo
{
    public class UploadBusinessFacePhotoViewModel: ObservableObject
    {
        private bool _next_page_checked;
        public bool next_page_checked
        {
            get { return _next_page_checked; }
            set
            {
                _next_page_checked = value;
                RaisePropertyChangedEvent("next_page_checked");
            }
        }

        private Page _next_page;
        public Page next_page
        {
            get
            {
                return _next_page;
            }

            set
            {
                _next_page = value;
            }
        }

        private string _photo_path = string.Empty;
        public object Photo_path
        {
            get
            {
                if (string.IsNullOrEmpty(_photo_path))
                    return DependencyProperty.UnsetValue;

                return _photo_path;
            }
            set
            {
                if (!(value is string))
                    return;

                _photo_path = value.ToString();
                RaisePropertyChangedEvent("Photo_path");
            }
            
        }

        RelayCommand _go_back_command;
        public RelayCommand go_back_command
        {
            get
            {
                if (_go_back_command == null)
                    _go_back_command = new RelayCommand(new Action<object>(go_home));
                return _go_back_command;
            }
            set
            {
                _go_back_command = value;
            }

        }

        private void go_home(object obj)
        {
            ListViewPage list_view_page = new ListViewPage();
            next_page = list_view_page;
            next_page_checked = true;

        }

        RelayCommand _upload_face_photo_command;
        public RelayCommand upload_face_photo_command
        {
            get
            {
                if (_upload_face_photo_command == null)
                    _upload_face_photo_command = new RelayCommand(new Action<object>(upload_face_photo));
                return _upload_face_photo_command;
            }
            set
            {
                _upload_face_photo_command = value;
            }

        }

        private async void upload_face_photo(object obj)
        {
            string imageFilePath = Photo_path.ToString();
            
            using (var fileStream = File.OpenRead(imageFilePath))
            {
                using (var faceServiceClient = new FaceServiceClient())
                {
                    Microsoft.ProjectOxford.Face.Contract.Face[] faces = await faceServiceClient.DetectAsync(
                        fileStream, false, true,
                        new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Glasses });

                    if (faces.Length != 1)
                    {
                        System.Windows.MessageBox.Show("No face in the photo");
                        return;
                    }
            
                }
                    
            }

            Guid persistedFaceId;
            //save the face to the face list
            using (var faceServiceClient = new FaceServiceClient())
            {
                string faceListId = "d7896b8a-92ba-4808-b335-c6634c309a74";
                Stream imageStream = File.OpenRead(imageFilePath);
                AddPersistedFaceResult result =  await faceServiceClient.AddFaceToFaceListAsync(faceListId, imageStream, imageFilePath);
                persistedFaceId = result.PersistedFaceId;
            }

            string photolocation ="";  //file name

            if (persistedFaceId != Guid.Empty)
            {
                photolocation = "e:\\temp\\temp\\" + persistedFaceId.ToString() + ".jpg";  //file name
                using (FileStream filestream = new FileStream(photolocation, FileMode.Create))
                {
                    BitmapImage image;
                    image = new BitmapImage(new Uri(imageFilePath));
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(filestream);
                }
            }

        }

        RelayCommand _browser_face_photo_command;
        public RelayCommand browser_face_photo_command
        {
            get
            {
                if (_browser_face_photo_command == null)
                    _browser_face_photo_command = new RelayCommand(new Action<object>(browse_face_photo));
                return _browser_face_photo_command;
            }
            set
            {
                _browser_face_photo_command = value;
            }

        }

        private void browse_face_photo(object obj)
        {
            

            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.Title = "Select A File";
            FileDialog.InitialDirectory = "";
            FileDialog.Filter = "Image Files (*.gif,*.jpg,*.jpeg,*.bmp,*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.png";
            FileDialog.FilterIndex = 1;

            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                Photo_path = FileDialog.FileName;
                
            }
            else
            {
                System.Windows.MessageBox.Show( "You didn't select any image file....");
            }

        }

        public UploadBusinessFacePhotoViewModel()
        {

        }

        public void init()
        {
            Photo_path = "/face_api_wpf_support;component/Images/ca1bb2bfd36cce6c4a89a8dc4548a22a.jpg";
        }

    }
}
