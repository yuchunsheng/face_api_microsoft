using face_api_wpf.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace face_api_wpf.ViewModels
{
    public class FaceRepositoryViewModel
    {
        public DelegateCommand command { get; private set; }

        public FaceRepositoryViewModel()
        {
            command = new DelegateCommand(showMessage);
        }

        public void showMessage()
        {
            MessageBox.Show("test");
        }

    }
}
