
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using face_api_commons.Model;
using face_api_commons.Common;

namespace face_api_wpf_support.ViewModels
{
    
    public class ListViewModel
    {
        public List<Item> business_client_list { get; private set; }
        

        public ListViewModel()
        {
            
            init();
        }

        private void init()
        {
            business_client_list = new List<Item>();
            using (var context = new DemoContext())
            {
                var business_clients = from a in context.BusinessClient
                            orderby a.ClientName
                            select a;

                foreach (var client in business_clients)
                {
                    business_client_list.Add(new Item(client.ClientName));
                }
            }
            

        }

        public void add_item(Item item)
        {
            using (var context = new DemoContext())
            {
                // Perform data access using the context 
                BusinessClient business_clinet = new BusinessClient();
                business_clinet.ClientName = item.Name;

                context.BusinessClient.Add(business_clinet);
                context.SaveChanges();

            }

            this.business_client_list.Clear();
            using (var context = new DemoContext())
            {
                var business_clients = from a in context.BusinessClient
                                       orderby a.ClientName
                                       select a;

                foreach (var client in business_clients)
                {
                    business_client_list.Add(new Item(client.ClientName));
                }
            }

        }

    }

   
        
}
