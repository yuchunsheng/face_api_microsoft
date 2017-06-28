using face_api_commons.Common;
using face_api_commons.Model;
using face_api_wpf_support.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace face_api_wpf_support.ViewModels
{
    public class BusinessClientDetailsViewModel: ObservableObject
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

        private Item _item;
        public Item item
        {
            get
            {
                return _item;
            }

            set
            {
                _item = value;
            }
        }

        private BusinessClient business_client = null;

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
            BusinessClientPage home_page = new BusinessClientPage();
            home_page.init();
            next_page = home_page;
            next_page_checked = true;

        }

        RelayCommand _update_command;
        public RelayCommand update_command
        {
            get
            {
                if (_update_command == null)
                    _update_command = new RelayCommand(new Action<object>(update));
                return _update_command;
            }
            set
            {
                _update_command = value;
            }

        }

        private void update(object obj)
        {
            if (business_client != null)
            {
                business_client.ClientName = item.Name;

                Task update_business_client_task = Task.Factory.StartNew(
                () =>
                {
                    using (var context = new DemoContext())
                    {
                        context.BusinessClient.Update(business_client);
                        context.SaveChanges();
                    }

                });

                update_business_client_task.Wait();
            }
            this.go_home(obj);

        }

        RelayCommand _delete_command;
        public RelayCommand delete_command
        {
            get
            {
                if (_delete_command == null)
                    _delete_command = new RelayCommand(new Action<object>(delete));
                return _delete_command;
            }
            set
            {
                _delete_command = value;
            }

        }

        private void delete(object obj)
        {
            if (business_client != null)
            {
                Task delete_business_client_task = Task.Factory.StartNew(
                () =>
                {
                    using (var context = new DemoContext())
                    {
                        context.BusinessClient.Remove(business_client);
                        context.SaveChanges();
                    }
                    
                });

                delete_business_client_task.Wait();

            }
            this.go_home(obj);
        }

        public BusinessClientDetailsViewModel()
        {

        }

        public void load_item(int id)
        {
            Task<Item> get_business_client_task = Task<Item>.Factory.StartNew(
                () =>
                {
                    using (var context = new DemoContext())
                    {
                        var business_client_result = context.BusinessClient.FirstOrDefault(i => i.Id == id);

                        if (business_client_result != null)
                        {
                            business_client = (BusinessClient)business_client_result;
                        }
                        
                    }
                    return new Item(business_client.Id, business_client.ClientName);

                });

            get_business_client_task.Wait();

            item = get_business_client_task.Result;
        }
        
    }
}
