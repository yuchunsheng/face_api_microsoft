
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
            business_client_list = new List<Item>();
        }

        public async Task load_business_client()
        {
            try
            {
                List<BusinessClient> business_clients_1 = await SomethingAsync();
                foreach (var client in business_clients_1)
                {
                    business_client_list.Add(new Item(client.ClientName));
                }
            }
            catch (Exception e){
                Console.WriteLine(e.ToString());
            }
            


        }

        public async Task<List<BusinessClient>> SomethingAsync()
        {
            List<BusinessClient> business_client_list_1 = new List<BusinessClient>();
            var task = Task.Run(() =>
            {
                
                using (var context = new DemoContext())
                {
                    var business_clients = from a in context.BusinessClient
                                           orderby a.ClientName
                                           select a;

                    foreach (var client in business_clients)
                    {
                        business_client_list_1.Add(client);
                    }

                    return business_client_list_1;
                }
                
            });

            return await task;

        }


        public void add_item_and_update(Item item)
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

        public void save_item(Item item)
        {
            using (var context = new DemoContext())
            {
                // Perform data access using the context 
                BusinessClient business_clinet = new BusinessClient();
                business_clinet.ClientName = item.Name;

                context.BusinessClient.Add(business_clinet);
                context.SaveChanges();

            }
        }

        public void refresh()
        {
            this.business_client_list.Clear();
            Task get_business_client_task = Task.Factory.StartNew( () =>
            {
                using (var context = new DemoContext())
                {
                    var business_clients = from a in context.BusinessClient
                                           orderby a.ClientName
                                           select a;

                    foreach (var client in business_clients)
                    {
                        this.business_client_list.Add(new Item(client.ClientName));
                    }
                }
            } );

            get_business_client_task.Wait();
        }

    }

   
        
}
