using face_api_commons.Common;
using face_api_commons.Model;
using face_api_wpf_support.Views;
using face_api_wpf_support.Views.business_client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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

        private PageFunction<string> _next_page;
        public PageFunction<string> next_page
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

        private bool _add_page_checked;
        public bool add_page_checked
        {
            get { return _add_page_checked; }
            set
            {
                _add_page_checked = value;
                RaisePropertyChangedEvent("add_page_checked");
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
            add_page_checked = true;
        }

        private bool _details_page_checked;
        public bool details_page_checked
        {
            get { return _details_page_checked; }
            set
            {
                _details_page_checked = value;
                RaisePropertyChangedEvent("details_page_checked");
            }
        }

        RelayCommand _show_details_command;
        public RelayCommand show_details_command
        {
            get
            {
                if (_show_details_command == null)
                    _show_details_command = new RelayCommand(new Action<object>(show_details_page));
                return _show_details_command;
            }
            set
            {
                _show_details_command = value;
            }

        }

        

        

        
        private void show_details_page(object obj)
        {
            int client_id = Convert.ToInt32(obj);
            Console.WriteLine(client_id);
            BusinessClientDetailsPageFunction details_page = new BusinessClientDetailsPageFunction();
            details_page.details_model.load_item(client_id);

            next_page = details_page;
            details_page_checked = true;
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
