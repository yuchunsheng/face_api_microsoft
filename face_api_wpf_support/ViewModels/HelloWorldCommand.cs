using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace face_api_wpf_support.ViewModels
{
    public class HelloWorldCommand : ICommand

    {

        public void Execute(object parameter)
        {
            MessageBox.Show((string)parameter);
        }

        public bool CanExecute(object parameter)
        {
            //return parameter != null;
            return true;

        }
        public event EventHandler CanExecuteChanged;

    }
}
