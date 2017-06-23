using face_api_commons.Common;
using face_api_commons.Model;
using face_api_wpf_support.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace face_api_wpf_support.ViewModels
{
    class BusinessClientViewModel: ObservableObject
    {
        private List<Item> _business_client_list;
        public List<Item> business_client_list
        {

            get { return _business_client_list; }

            set {
                _business_client_list = value;
                RaisePropertyChangedEvent("business_client_list");
            }

        }

        RelayCommand _add_new_command;
        public RelayCommand add_new_command
        {
            get
            {
                if (_add_new_command == null)
                    _add_new_command = new RelayCommand(new Action<object>(show_add_new));
                return _add_new_command;
            }
            set
            {
                _add_new_command = value;
            }

        }

        private void show_add_new(object obj)
        {
            throw new NotImplementedException();
        }

        public BusinessClientViewModel()
        {
            business_client_list = new List<Item>();
        }

        public void load_business_client()
        {
            List<Item> business_clients_1 = new List<Item>();

            Task<List<Item>> get_business_client_task = Task<List<Item>>.Factory.StartNew(
                () =>
                {
                    List<Item> business_client_list_1 = new List<Item>();
                    using (var context = new DemoContext())
                    {
                        var business_clients = from a in context.BusinessClient
                                               orderby a.ClientName
                                               select a;

                        foreach (var client in business_clients)
                        {
                            business_client_list_1.Add(new Item(client.Id, client.ClientName));
                        }

                        return business_client_list_1;
                    }
                    
                }
                );

            get_business_client_task.Wait();

            business_client_list = get_business_client_task.Result;

        }


        
    }
}
