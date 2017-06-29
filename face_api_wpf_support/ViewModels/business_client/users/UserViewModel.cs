using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace face_api_wpf_support.ViewModels.business_client.users
{
    class UserViewModel
    {
        private List<string> _user_list;
        public List<string> User_list
        {
            get
            {
                return _user_list;
            }

            set
            {
                _user_list = value;
            }
        }

        private int business_client_id;

        public UserViewModel(int id)
        {
            business_client_id = id;
            Console.WriteLine(id);
        }
    }
}
