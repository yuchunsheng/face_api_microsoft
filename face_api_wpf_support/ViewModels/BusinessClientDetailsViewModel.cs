using face_api_commons.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace face_api_wpf_support.ViewModels
{
    public class BusinessClientDetailsViewModel
    {
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

        public BusinessClientDetailsViewModel()
        {

        }

        public void load_item(int id)
        {
            Task<Item> get_business_client_task = Task<Item>.Factory.StartNew(
                () =>
                {
                    BusinessClient business_client = new BusinessClient();
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
