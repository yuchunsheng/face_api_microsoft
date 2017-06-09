using face_api_models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }

    public class Item
    {
        public Item(string name)
        {
            Name = name;
            //Matches = matches;
        }

        public string Name { get; set; }
        //public string Matches { get; set; }
    }
        
}
