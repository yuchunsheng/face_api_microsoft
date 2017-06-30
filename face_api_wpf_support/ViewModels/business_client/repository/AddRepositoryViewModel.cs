using face_api_commons.Common;
using face_api_commons.Model;
using face_api_wpf_support.Views.business_client.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace face_api_wpf_support.ViewModels.business_client.repository
{
    public class AddRepositoryViewModel: ObservableObject
    {
        private List<string> _availiable_repository;
        public List<string> Availiable_repository
        {
            get
            {
                return _availiable_repository;
            }

            set
            {
                _availiable_repository = value;
            }
        }

        public Item Business_client
        {
            get
            {
                return _business_client;
            }

            set
            {
                _business_client = value;
            }
        }
        private Item _business_client;

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
            RepositoryPage repository_page = new RepositoryPage();
            repository_page.load_item((int)obj);
            next_page = repository_page;
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

            AddRepositoryPage repository_page = new AddRepositoryPage();
            Console.WriteLine(obj.ToString());
            repository_page.load_item(obj);
            next_page = repository_page;
            next_page_checked = true;
        }
        

        public AddRepositoryViewModel()
        {

        }

        public void init(object obj)
        {
            Business_client_id = (int)obj;
            load_avaliable_repository();
        }

        public void load_avaliable_repository()
        {
            Task<List<string>> get_repository_task = Task<List<string>>.Factory.StartNew(
                () =>
                {
                    List<string> result = new List<string>();
                    using (var context = new DemoContext())
                    {
                        var query =
                           from repository in context.FaceRepository
                           where repository.Availiable == 1 
                           select repository;

                        foreach (var repository in query)
                        {
                            result.Add((string)repository.FaceRepositoryId);
                        }
                    }

                    return result;

                });

            get_repository_task.Wait();

            Availiable_repository = get_repository_task.Result;
        }


    }
}
