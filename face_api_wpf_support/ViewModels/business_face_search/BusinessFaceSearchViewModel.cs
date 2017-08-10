using face_api_commons.Common;
using face_api_commons.Model;
using face_api_wpf_support.Views;
using face_api_wpf_support.Views.business_face_search;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace face_api_wpf_support.ViewModels.business_face_search
{
    class BusinessFaceSearchViewModel: ObservableObject
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

        private bool _dialog_open;
        public bool Dialog_open
        {
            get
            {
                return _dialog_open;
            }

            set
            {
                _dialog_open = value;
                RaisePropertyChangedEvent("Dialog_open");
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

        RelayCommand _upload_face_photo_command;
        public RelayCommand upload_face_photo_command
        {
            get
            {
                if (_upload_face_photo_command == null)
                    _upload_face_photo_command = new RelayCommand(new Action<object>(search_face));
                return _upload_face_photo_command;
            }
            set
            {
                _upload_face_photo_command = value;
            }

        }

        public BusinessFaceSearchViewModel()
        {

        }

        public void init()
        {
        }

        private void go_home(object obj)
        {
            ListViewPage list_view_page = new ListViewPage();
            next_page = list_view_page;
            next_page_checked = true;

        }

        private async void search_face(object obj)
        {
            //string imageFilePath = Photo_path.ToString();

            Guid faceId;
            string faceListId = "d7896b8a-92ba-4808-b335-c6634c309a74";
            int maxNumOfCandidatesReturned = 20;

            using (var fileStream = File.OpenRead(Photo_path.ToString()))
            {
                using (var faceServiceClient = new FaceServiceClient())
                {
                    Microsoft.ProjectOxford.Face.Contract.Face[] faces = await faceServiceClient.DetectAsync(fileStream, true, false, null);

                    //new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Glasses });

                    if (faces.Length == 0)
                    {
                        System.Windows.MessageBox.Show("No face in the photo");
                        return;
                    }
                    else
                    {
                        faceId = faces[0].FaceId;
                    }

                }

            }
            SimilarPersistedFace[] result = null;

            //save the face to the face list
            using (var faceServiceClient = new FaceServiceClient())
            {
                //string faceListId = "d7896b8a-92ba-4808-b335-c6634c309a74";
                try
                {
                    result = await faceServiceClient.FindSimilarAsync(faceId, faceListId , FindSimilarMatchMode.matchFace, maxNumOfCandidatesReturned);
                }
                catch (FaceAPIException e)
                {
                    Console.WriteLine(e.ErrorMessage);
                }
                
            }
            
            BusinessFaceSearchResultPage face_result_page = new BusinessFaceSearchResultPage();
            face_result_page.load_item(result);
            next_page = face_result_page;
            next_page_checked = true;

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
                System.Windows.MessageBox.Show("You didn't select any image file....");
            }

        }
    }
}
