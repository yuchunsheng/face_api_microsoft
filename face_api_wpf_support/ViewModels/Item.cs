using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace face_api_wpf_support.ViewModels
{
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
