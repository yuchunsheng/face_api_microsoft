using face_api_commons.Common;
using face_api_wpf_support.Views.business_face_search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace face_api_wpf_support.ViewModels.business_face_search
{
    public class BusinessFaceSearchDetailViewModel : ViewModelBase
    {
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

        RelayCommand _go_back_command;
        public RelayCommand go_back_command
        {
            get
            {
                if (_go_back_command == null)
                    _go_back_command = new RelayCommand(new Action<object>(go_back));
                return _go_back_command;
            }
            set
            {
                _go_back_command = value;
            }
        }

        private void go_back(object obj)
        {
            BusinessFaceSearchResultPage business_face_search_result_page = new BusinessFaceSearchResultPage();

            next_page = business_face_search_result_page;
            next_page_checked = true;

        }
    }
}
