using face_api_commons.Common;
using face_api_commons.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace face_api_wpf_support.ViewModels
{
    class BusinessClientAddViewModel : ObservableObject
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

        RelayCommand _goto_next_page_command;
        public RelayCommand goto_next_page_command
        {
            get
            {
                if (_goto_next_page_command == null)
                    _goto_next_page_command = new RelayCommand(new Action<object>(goto_next_page));
                return _goto_next_page_command;
            }
            set
            {
                _goto_next_page_command = value;
            }

        }

        private String _client_name;
        public String client_name
        {
            get { return _client_name; }
            set
            {
                _client_name = value;
                RaisePropertyChangedEvent("client_name");
            }
        }

        private void goto_next_page(object obj)
        {
            string param = (String)obj;
            Console.WriteLine(param);
            Console.WriteLine(client_name);

            if (client_name.Length > 0)
            {
                using (var context = new DemoContext())
                {
                    // Perform data access using the context 
                    BusinessClient business_clinet = new BusinessClient();
                    business_clinet.ClientName = client_name;

                    context.BusinessClient.Add(business_clinet);
                    context.SaveChanges();

                }
            }
            
            this.next_page_checked = true;
        }

        public BusinessClientAddViewModel()
        {
            client_name = "";
        }    
    }
}
