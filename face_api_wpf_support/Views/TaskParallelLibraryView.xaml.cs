using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace face_api_wpf_support.Views
{
    /// <summary>
    /// Interaction logic for TaskParallelLibraryView.xaml
    /// </summary>
    public partial class TaskParallelLibraryView : Page
    {
        public TaskParallelLibraryView()
        {
            InitializeComponent();
            this.progress_bar.Visibility = Visibility.Hidden;
        }

        private void go_home(object sender, RoutedEventArgs e)
        {
            Page home = new ListViewPage();
            this.NavigationService.Navigate(home);
        }

        private async void buttonAsync_Click(object sender, RoutedEventArgs e)
        {
            buttonAsync.IsEnabled = false;
            MyPopup.IsOpen = true;

            // The Progress<T> constructor captures our UI context,
            //  so the lambda will be run on the UI thread.
            var progress = new Progress<string>(ReportProgress);

            this.progress_bar.Visibility = Visibility.Visible;

            // DoProcessing is run on the thread pool.
            int uploads =  await UploadPicturesAsync(progress);

            //textBoxResults.Text = "Done!";

            this.progress_bar.Visibility = Visibility.Hidden;

            MyPopup.IsOpen = false;
            buttonAsync.IsEnabled = true;


        }

        void ReportProgress(string value)
        {
            //Update the UI to reflect the progress value that is passed back.
            textBoxResults.Text = value + "%";
        }


        async Task<int> UploadPicturesAsync(IProgress<string> progress)
        {
            int processCount = await Task.Run<int>(() =>
            {
                for (int i = 0; i != 100; ++i)
                {
                    Thread.Sleep(100); // CPU-bound work
                    if (progress != null)
                        progress.Report(i.ToString());
                }
                return 100;

            });
            
            return 100;

        }
        
    }
}
