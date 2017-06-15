using face_api_wpf_support.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace face_api_wpf_support.Views
{
    /// <summary>
    /// Interaction logic for WPFAsyncQueryView.xaml
    /// </summary>
    public partial class WPFAsyncQueryView : Page
    {
        ObservableCollection<Employee> employees;
        CancellationTokenSource cancelToken;
        Progress<double> progressOperation;

        public WPFAsyncQueryView()
        {
            InitializeComponent();
            employees = new ObservableCollection<Employee>();
            btnCancel.IsEnabled = false;
        }

        // Displaying Employees in DataGrid 
        private async void btnLoadEmployee_Click(object sender, RoutedEventArgs e)
        {
            cancelToken = new CancellationTokenSource();
            btnLoadEmployee.IsEnabled = false;
            btnCancel.IsEnabled = true;
            txtStatus.Text = "Loading.....";
            progressOperation = new Progress<double>(value => progress.Value = value);

            try
            {

                var Emps = await LoadEmployeesAsync(cancelToken.Token, progressOperation);

                foreach (var item in Emps)
                {
                    dgEmp.Items.Add(item);
                }

                txtStatus.Text = "Operation Completed";
            }
            catch (OperationCanceledException ex)
            {
                txtStatus.Text = "Operation cancelled" + ex.Message;
            }
            catch (Exception ex)
            {
                txtStatus.Text = "Operation cancelled" + ex.Message;
            }
            finally
            {
                cancelToken.Dispose();
                btnLoadEmployee.IsEnabled = true;
                btnCancel.IsEnabled = false;
            }


        }


        // Async Method to Load Employees

        async Task<ObservableCollection<Employee>> LoadEmployeesAsync(CancellationToken ct, IProgress<double> progress)
        {
            employees.Clear();
            var task = Task.Run(() => {
                var xDoc = XDocument.Load("Company.xml");

                var Res = (from emp in xDoc.Descendants("Employee")
                           select new Employee()
                           {
                               EmpNo = Convert.ToInt32(emp.Descendants("EmpNo").First().Value),
                               EmpName = emp.Descendants("EmpName").First().Value.ToString(),
                               Salary = Convert.ToInt32(emp.Descendants("Salary").First().Value)
                           }).ToList();


                int recCount = 0;

                foreach (var item in Res)
                {
                    Thread.Sleep(100);
                    ct.ThrowIfCancellationRequested();
                    employees.Add(item);
                    ++recCount;
                    progress.Report(recCount * 100.0 / 50);
                }

                return employees;

            });

            return await task;
        }

        private async void btnCalculateTax_Click(object sender, RoutedEventArgs e)
        {
            cancelToken = new CancellationTokenSource();
            btnLoadEmployee.IsEnabled = false;
            btnCalculateTax.IsEnabled = false;
            btnCancel.IsEnabled = true;
            txtStatus.Text = "Calculating.....";
            progressOperation = new Progress<double>(value => progress.Value = value);
            try
            {
                dgEmp.Items.Clear();
                int recCount = 0;

                foreach (Employee Emp in employees)
                {
                    dgEmp.Items.Add(await CalculateTaxPerRecord(Emp, cancelToken.Token, progressOperation));
                    ++recCount;
                    ((IProgress<double>)progressOperation).Report(recCount * 100.0 / 50);
                }

                txtStatus.Text = "Operation Completed";
            }
            catch (OperationCanceledException ex)
            {
                txtStatus.Text = "Operation cancelled" + ex.Message;
            }
            catch (Exception ex)
            {
                txtStatus.Text = "Operation cancelled" + ex.Message;
            }
            finally
            {
                cancelToken.Dispose();
                btnCalculateTax.IsEnabled = true;
                btnCancel.IsEnabled = false;
            }
        }



        // Calcluate the Tax for every Employee Record.
        async Task<Employee> CalculateTaxPerRecord(Employee Emp, CancellationToken ct, IProgress<double> progress)
        {
            var tsk = Task<Employee>.Run(() =>
            {
                Thread.Sleep(100);
                ct.ThrowIfCancellationRequested();
                Emp.Tax = Convert.ToInt32(Emp.Salary * 0.2);
                return Emp;
            });


            return await tsk;
        }



        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelToken.Cancel();
        }
    }
}
