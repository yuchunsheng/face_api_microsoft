using face_api_models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace face_api_wpf.ViewModels
{
    public class LoginViewModel:ObservableObject
    {
        private string _userName;

        public string UserName
        {
            get {
                using (var context = new DemoContext())
                {
                    var users = from a in context.User
                                  where a.UserName.StartsWith("t")
                                  orderby a.UserName
                               select a;

                    foreach (var user in users)
                    {
                        Console.WriteLine(user.UserName);
                        _userName = user.UserName;
                    }
                }
                return _userName;

            }
            set
            {
                _userName = value;
                RaisePropertyChangedEvent("UserName");
            }
        }

       
    }
}
