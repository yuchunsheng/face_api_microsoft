using face_api_commons.Common;
using face_api_commons.Model;
using face_api_wpf_support.Views;
using face_api_wpf_support.Views.business_client.repository;
using face_api_wpf_support.Views.repository;
using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace face_api_wpf_support.ViewModels.repository
{
    public class ManageRepositoryViewModel: ObservableObject
    {
        private List<RepositoryItem> _all_repository;
        public List<RepositoryItem> all_repository
        {
            get
            {
                return _all_repository;
            }

            set
            {
                _all_repository = value;
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
            ListViewPage main_page = new ListViewPage();
            main_page.Load_business_client();
            next_page = main_page;
            next_page_checked = true;
        }


        RelayCommand _create_face_repository_command;
        public RelayCommand create_face_repository_command
        {
            get
            {
                if (_create_face_repository_command == null)
                    _create_face_repository_command = new RelayCommand(new Action<object>(create_face_repository));
                return _create_face_repository_command;
            }
            set
            {
                _create_face_repository_command = value;
            }

        }

        private void create_face_repository(object obj)
        {

            CreateNewRepository new_repository_page = new CreateNewRepository();
            new_repository_page.load_item();
            next_page = new_repository_page;
            next_page_checked = true;
        }

        RelayCommand _deletee_face_repository_command;
        public RelayCommand deletee_face_repository_command
        {
            get
            {
                if (_deletee_face_repository_command == null)
                    _deletee_face_repository_command = new RelayCommand(new Action<object>(delete_face_repository));
                return _deletee_face_repository_command;
            }
            set
            {
                _deletee_face_repository_command = value;
            }

        }

        private async void delete_face_repository(object obj)
        {
            //delete the face repository
            Console.WriteLine((string)obj);
            string face_list_id = (string)obj;
            var faceServiceClient = new FaceServiceClient();

            await faceServiceClient.DeleteFaceListAsync(face_list_id);

            Task delete_face_repository_task = Task.Factory.StartNew(
                () =>
                {
                    using (var context = new DemoContext())
                    {
                        var face_repository = context.FaceRepository.FirstOrDefault(i => i.FaceRepositoryId == face_list_id);

                        if (face_repository != null)
                        {
                            using (var dbContextTransaction = context.Database.BeginTransaction())
                            {
                                try
                                {
                                    context.FaceRepository.Remove((FaceRepository)face_repository);
                                    context.SaveChanges();
                                    dbContextTransaction.Commit();

                                }
                                catch (Exception)
                                {
                                    dbContextTransaction.Rollback();
                                }

                            }
                            
                        }
                    }

                });

            delete_face_repository_task.Wait();

            ManageRepositoryPage mange_repository_page = new ManageRepositoryPage();
            mange_repository_page.load_item();
            next_page = mange_repository_page;
            next_page_checked = true;
        }


        public ManageRepositoryViewModel()
        {

        }

        public void init()
        {
            load_avaliable_repository();
        }

        public void load_avaliable_repository()
        {
            List<RepositoryItem> result = new List<RepositoryItem>(); 

            Task<List<RepositoryItem>> get_repository_task = Task<List<RepositoryItem>>.Factory.StartNew(
                () =>
                {
                    
                    using (var context = new DemoContext())
                    {
                        var query =
                           from repository in context.FaceRepository
                           //where repository.Availiable == 1 
                           select repository;

                        foreach (var repository in query)
                        {
                            result.Add(new RepositoryItem((repository.Id).ToString(), repository.FaceRepositoryId, repository.FaceRepositoryName, repository.FaceRepositoryComments));
                        }
                    }

                    return result;
                });

            get_repository_task.Wait();

            all_repository = get_repository_task.Result;
        }

    }
}
