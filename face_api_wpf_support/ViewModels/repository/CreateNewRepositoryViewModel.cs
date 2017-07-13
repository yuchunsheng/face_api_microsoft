using face_api_commons.Common;
using face_api_commons.Model;
using face_api_wpf_support.Views.business_client.repository;
using face_api_wpf_support.Views.repository;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace face_api_wpf_support.ViewModels.business_client.repository
{
    public class CreateNewRepositoryViewModel : ObservableObject
    {
        private string _repository_name;
        public string repository_name
        {
            get
            {
                return _repository_name;
            }

            set
            {
                _repository_name = value;
            }
        }

        private string _repository_comment;
        public string repository_comment
        {
            get
            {
                return _repository_comment;
            }

            set
            {
                _repository_comment = value;
            }
        }
        
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

        RelayCommand _go_back_command;
        public RelayCommand go_back_command
        {
            get
            {
                if (_go_back_command == null)
                    _go_back_command = new RelayCommand(new Action<object>(go_back));
                return _go_back_command;
            }
            set
            {
                _go_back_command = value;
            }

        }

        private void go_back(object obj)
        {
            ManageRepositoryPage manage_repository_page = new ManageRepositoryPage();
            manage_repository_page.load_item();
            next_page = manage_repository_page;
            next_page_checked = true;

        }

        RelayCommand _create_new_repository_command;
        public RelayCommand create_new_repository_command
        {
            get
            {
                if (_create_new_repository_command == null)
                    _create_new_repository_command = new RelayCommand(new Action<object>(create_new_repository));
                return _create_new_repository_command;
            }
            set
            {
                _create_new_repository_command = value;
            }

        }

        private void create_new_repository(object obj)
        {
            if (!String.IsNullOrEmpty(repository_name))
            {
                call_face_client_service();

                Console.WriteLine(repository_name);
            }

        }


        public CreateNewRepositoryViewModel()
        {

        }

        public void init()
        {
            repository_name = "";
        }

        private async void call_face_client_service()
        {
            try
            {
                string face_list_id = Guid.NewGuid().ToString();
                var faceServiceClient = new FaceServiceClient();

                //await faceServiceClient.CreateFaceListAsync(face_list_id, repository_name, repository_comment);

                //save the face_list_name, face_list_id and comments in the database
                Task add_face_repository_task = Task.Factory.StartNew(
                () =>
                {
                    using (var context = new DemoContext())
                    {
                        // Perform data access using the context 
                        FaceRepository face_repository = new FaceRepository();
                        face_repository.FaceRepositoryId = face_list_id;
                        face_repository.FaceRepositoryName = repository_name;
                        face_repository.FaceRepositoryComments = repository_comment;
                        face_repository.Availiable = 1;

                        context.FaceRepository.Add(face_repository);
                        context.SaveChanges();

                    }

                });

                add_face_repository_task.Wait();

                ManageRepositoryPage repository_page = new ManageRepositoryPage();
                repository_page.load_item();
                next_page = repository_page;
                this.next_page_checked = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Response: {0}", ex.Message);
                if (ex is FaceAPIException)
                {
                    Console.WriteLine("Response: {0}. {1}", ((FaceAPIException)ex).ErrorCode, ((FaceAPIException)ex).ErrorMessage);
                    return;
                }

                throw;
            }

        }
    }
}
