using face_api_commons.Common;
using face_api_commons.Model;
using face_api_wpf_support.Views.business_face_search;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace face_api_wpf_support.ViewModels.business_face_search
{
    public class BusinessFaceSearchResultViewModel: ObservableObject
    {
        private bool _next_page_checked;
        public bool next_page_checked
        {
            get { return _next_page_checked; }
            set
            {
                _next_page_checked = value;
                RaisePropertyChangedEvent("next_page_checked");
            }
        }

        private Page _next_page;
        public Page next_page
        {
            get
            {
                return _next_page;
            }

            set
            {
                _next_page = value;
            }
        }

        private bool _dialog_open;
        public bool Dialog_open
        {
            get
            {
                return _dialog_open;
            }

            set
            {
                _dialog_open = value;
                RaisePropertyChangedEvent("Dialog_open");
            }
        }

        private string _photo_path = string.Empty;
        public object Photo_path
        {
            get
            {
                if (string.IsNullOrEmpty(_photo_path))
                    return DependencyProperty.UnsetValue;

                return _photo_path;
            }
            set
            {
                if (!(value is string))
                    return;

                _photo_path = value.ToString();
                RaisePropertyChangedEvent("Photo_path");
            }

        }

        private List<FaceDocItem> _similar_face_list ;
        public List<FaceDocItem> Similar_face_list
        {
            get
            {
                if (_similar_face_list != null)
                {
                    return _similar_face_list;
                }
                else
                {
                    return new List<FaceDocItem>();
                }
            }

            set
            {
                _similar_face_list = value;
                RaisePropertyChangedEvent("Similar_face_list");
            }
        }

        RelayCommand _go_back_command;
        public RelayCommand go_back_command
        {
            get
            {
                if (_go_back_command == null)
                    _go_back_command = new RelayCommand(new Action<object>(go_home));
                return _go_back_command;
            }
            set
            {
                _go_back_command = value;
            }

        }

        

        private void go_home(object obj)
        {
            BusinessFaceSearchPage face_search_page = new BusinessFaceSearchPage();
            next_page = face_search_page;
            next_page_checked = true;

        }

        public void load_item(SimilarPersistedFace[] face_list)
        {
            if (face_list != null)
            {
                var face_dict = new Dictionary<string, double>();
                var temp_similar_list = new List<string>();

                foreach (var similar_face in face_list)
                {
                    Console.WriteLine(similar_face.PersistedFaceId.ToString() + ":::" + similar_face.Confidence);
                    face_dict.Add(similar_face.PersistedFaceId.ToString(), similar_face.Confidence);
                }

                var ordered_face_list = face_dict.OrderBy(x => x.Value);
                
                List<FaceDocItem> result = new List<FaceDocItem>();

                Task<List<FaceDocItem>> get_repository_task = Task<List<FaceDocItem>>.Factory.StartNew(
                    () =>
                    {

                        using (var context = new DemoContext())
                        {
                            var face_doc_list = from face_docs in context.FaceDocs
                                                where face_dict.Keys.Contains(face_docs.FaceDocId)
                                                select face_docs;

                            foreach (var face_doc in face_doc_list)
                            {
                                var temp = new FaceDocItem(face_doc.FaceDocId, face_doc.UserData);
                                temp.similarity = face_dict[face_doc.FaceDocId];

                                result.Add(temp);
                            }
                        }

                        return result;
                    });

                get_repository_task.Wait();

                var temp_face_doc_list = get_repository_task.Result;

                Similar_face_list = temp_face_doc_list.OrderByDescending(o => o.similarity).ToList();
            }
            else
            {
                Similar_face_list = new List<FaceDocItem>();
            }
               
            
        }
    }
}
