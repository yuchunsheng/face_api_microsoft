using face_api_commons.Common;
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

        private void goto_next_page(object obj)
        {
            this.next_page_checked = true;
        }

        public BusinessClientAddViewModel()
        {

        }    
    }
}
