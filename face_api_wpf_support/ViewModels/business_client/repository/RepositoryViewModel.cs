using face_api_commons.Common;
using face_api_commons.Model;
using face_api_wpf_support.Views.business_client;
using face_api_wpf_support.Views.business_client.repository;
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
    public class RepositoryViewModel: ObservableObject
    {
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

        private List<RepositoryItem> _reporitory_list;

        public List<RepositoryItem> Reporitory_list
        {
            get
            {
                return _reporitory_list;
            }

            set
            {
                _reporitory_list = value;
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

        RelayCommand _go_home_command;
        public RelayCommand go_home_command
        {
            get
            {
                if (_go_home_command == null)
                    _go_home_command = new RelayCommand(new Action<object>(go_home));
                return _go_home_command;
            }
            set
            {
                _go_home_command = value;
            }

        }

        private void go_home(object obj)
        {
            BusinessClientDetailsPageFunction home_page = new BusinessClientDetailsPageFunction();
            home_page.details_model.load_item((int)obj);
            next_page = home_page;
            next_page_checked = true;

        }

        RelayCommand _add_repository_command;
        public RelayCommand add_repository_command
        {
            get
            {
                if (_add_repository_command == null)
                    _add_repository_command = new RelayCommand(new Action<object>(add_repository));
                return _add_repository_command;
            }
            set
            {
                _add_repository_command = value;
            }

        }

        private void add_repository(object obj)
        {
            AddRepositoryPage add_repository_page = new AddRepositoryPage();
            add_repository_page.load_item(obj);
            next_page = add_repository_page;
            next_page_checked = true;

        }

        RelayCommand _delete_repository_command;
        public RelayCommand delete_repository_command
        {
            get
            {
                if (_delete_repository_command == null)
                    _delete_repository_command = new RelayCommand(new Action<object>(delete_repository));
                return _delete_repository_command;
            }
            set
            {
                _delete_repository_command = value;
            }

        }

        private async void delete_repository(object obj)
        {
            string face_list_id = (string)obj;
            //delete all faces in the face repository
            //You can't delete a face repository with face in it.
            
            var faceServiceClient = new FaceServiceClient();
            FaceList face_list = null;
            bool face_api_error = false;
            try
            {
                face_list = await faceServiceClient.GetFaceListAsync(face_list_id);
            }
            catch(FaceAPIException ex)
            {
                face_api_error = true;
                Console.WriteLine(ex.ErrorMessage);
            }

            
            if (face_api_error || 
                (face_list != null && face_list.PersistedFaces != null && face_list.PersistedFaces.Count() > 0))
            {
                Dialog_open = true;
            }
            else
            {
                //remove relationship between business cline and the face repository
                //make the face repository avaliable
                Task retiring_face_repository_task = Task.Factory.StartNew(
                () =>
                {
                    using (var context = new DemoContext())
                    {
                        var business_face_repository = (from re_bu in context.FaceRepositoryBusinessClient
                                                        join re in context.FaceRepository on re_bu.FaceReposityId equals re.Id
                                                        where re.FaceRepositoryId.Equals(face_list_id)
                                                        select re_bu).Take(1);
                        var face_repository = context.FaceRepository.FirstOrDefault(i => i.FaceRepositoryId == face_list_id);
                        face_repository.Availiable = 1;

                        if (business_face_repository != null)
                        {
                            using (var dbContextTransaction = context.Database.BeginTransaction())
                            {
                                try
                                {
                                    context.FaceRepositoryBusinessClient.Remove((FaceRepositoryBusinessClient)business_face_repository);
                                    context.FaceRepository.Update(face_repository);
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

                retiring_face_repository_task.Wait();

                RepositoryPage repository_page = new RepositoryPage();
                repository_page.load_item(Business_client_id);
                next_page = repository_page;
                next_page_checked = true;
            }
            

            

            

        }


        private int _business_client_id = 0;
        public int Business_client_id
        {
            get
            {
                return _business_client_id;
            }

            set
            {
                _business_client_id = value;
            }
        }

        

        public RepositoryViewModel()
        {
            Dialog_open = false;
        }

        public void init(object id)
        {
            Business_client_id = (int)id;

            if (Business_client_id != 0)
            {
                Task<List<RepositoryItem>> get_repository_task = Task<List<RepositoryItem>>.Factory.StartNew(
                () =>
                {
                    List<RepositoryItem> result = new List<RepositoryItem>();
                    using (var context = new DemoContext())
                    {
                        var query =
                           from repository in context.FaceRepository
                           join face_repository in context.FaceRepositoryBusinessClient on repository.Id equals face_repository.FaceReposityId
                           where face_repository.BusinessClientId == Business_client_id
                           select repository;

                        foreach (var repository in query)
                        {
                            result.Add(new RepositoryItem((repository.Id).ToString(), 
                                repository.FaceRepositoryId, 
                                repository.FaceRepositoryName, 
                                repository.FaceRepositoryComments));
                        }
                    }

                    return result;

                });

                get_repository_task.Wait();

                Reporitory_list = get_repository_task.Result;
            }
            
        }
    }
}
