using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace face_api_wpf_support.ViewModels
{
    public class Item
    {
        public Item(long Id, string name)
        {
            this.Id = Id.ToString();
            Name = name;
            //Matches = matches;
        }

        public Item(string name)
        {
            Name = name;
            //Matches = matches;
        }

        public String Id { get; set; }
        public string Name { get; set; }
        //public string Matches { get; set; }
    }
}
