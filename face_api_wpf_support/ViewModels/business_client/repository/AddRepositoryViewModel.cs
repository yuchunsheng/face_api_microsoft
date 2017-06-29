using face_api_commons.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace face_api_wpf_support.ViewModels.business_client.repository
{
    class AddRepositoryViewModel
    {
        private List<string> _availiable_repository;
        public List<string> Availiable_repository
        {
            get
            {
                return _availiable_repository;
            }

            set
            {
                _availiable_repository = value;
            }
        }

        public Item Business_client
        {
            get
            {
                return _business_client;
            }

            set
            {
                _business_client = value;
            }
        }

        private Item _business_client;


        public AddRepositoryViewModel()
        {

        }

        public void load_avaliable_repository()
        {
            Task<List<string>> get_repository_task = Task<List<string>>.Factory.StartNew(
                () =>
                {
                    List<string> result = new List<string>();
                    using (var context = new DemoContext())
                    {
                        var query =
                           from repository in context.FaceRepository
                           where repository.Availiable == 1 
                           select repository;

                        foreach (var repository in query)
                        {
                            result.Add((string)repository.FaceRepositoryId);
                        }
                    }

                    return result;

                });

            get_repository_task.Wait();

            Availiable_repository = get_repository_task.Result;
        }
    }
}
