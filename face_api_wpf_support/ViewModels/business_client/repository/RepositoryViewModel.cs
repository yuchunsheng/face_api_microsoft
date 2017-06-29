using face_api_commons.Common;
using face_api_commons.Model;
using face_api_wpf_support.Views.business_client;
using face_api_wpf_support.Views.business_client.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace face_api_wpf_support.ViewModels.business_client.repository
{
    class RepositoryViewModel: ObservableObject
    {
        private List<String> _reporitory_list;

        public List<string> Reporitory_list
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
        public RelayCommand goto_next_page_command
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
            home_page.details_model.load_item(business_client_id);
            next_page = home_page;
            next_page_checked = true;

        }

        private int business_client_id = 0;

        public RepositoryViewModel()
        {
            
        }

        public void init(int id)
        {
            this.business_client_id = id;

            if (business_client_id != 0)
            {
                Task<List<string>> get_repository_task = Task<List<string>>.Factory.StartNew(
                () =>
                {
                    List<string> result = new List<string>();
                    using (var context = new DemoContext())
                    {
                        var query =
                           from repository in context.FaceRepository
                           join face_repository in context.FaceRepositoryBusinessClient on repository.Id equals face_repository.FaceReposityId
                           where face_repository.BusinessClientId == business_client_id
                           select repository;

                        foreach (var repository in query)
                        {
                            result.Add((string)repository.FaceRepositoryId);
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
