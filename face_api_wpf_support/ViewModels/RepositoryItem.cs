using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace face_api_wpf_support.ViewModels
{
    public class RepositoryItem
    {
        public RepositoryItem()
        {
            
        }

        public RepositoryItem(string id, string repository_id, string repository_name, string repository_comment)
        {
            this.Id = id;
            this.repository_id = repository_id;
            this.repository_name = repository_name;
            this.repository_comment = repository_comment;

        }

        public String Id { get; set; }
        public string repository_id { get; set; }
        public string repository_name { get; set; }
        public string repository_comment { get; set; }
    }
}
