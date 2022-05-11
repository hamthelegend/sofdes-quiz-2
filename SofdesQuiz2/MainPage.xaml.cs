using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SofdesQuiz2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public ObservableCollection<Employee> Employees = new ObservableCollection<Employee>();

        public MainPage()
        {
            InitializeComponent();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var id = EmployeeIdInput.Text;
            var firstName = FirstNameInput.Text;
            var lastName = LastNameInput.Text;
            var profession = ProfessionInput.Text;
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(profession))
            {
                Notification.Show("None of the fields can be empty", 2000);
                return;
            }
            foreach (var employee in Employees)
            {
                if (employee.Id == id)
                {
                    Notification.Show("Employee ID already exists", 2000);
                    return;
                }
            }
            var newEmployee = new Employee(id, firstName, lastName, profession);
            Employees.Add(newEmployee);
            Clear(sender, e);
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = EmployeesGrid.SelectedItem as Employee;
            if (selectedEmployee == null)
            {
                Notification.Show("Select an employee to delete from the list first.", 2000);
                return;
            }
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Delete employee",
                Content = "Are you sure you want to delete this employee?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
            
            if (result == ContentDialogResult.Primary)
            {
                Employees.Remove(selectedEmployee);
                EmployeesGrid.SelectedIndex = -1;
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            EmployeeIdInput.Text = string.Empty;
            FirstNameInput.Text = string.Empty;
            LastNameInput.Text = string.Empty;
            ProfessionInput.Text = string.Empty;
            EmployeesGrid.SelectedIndex = -1;
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            var id = EmployeeIdInput.Text;
            var firstName = FirstNameInput.Text;
            var lastName = LastNameInput.Text;
            var profession = ProfessionInput.Text;
            var selectedEmployee = EmployeesGrid.SelectedItem as Employee;
            foreach (var employee in Employees)
            {
                if (employee.Id == id && employee != selectedEmployee)
                {
                    Notification.Show("Employee ID already exists", 2000);
                    return;
                }
            }
            if (selectedEmployee == null)
            {
                Notification.Show("Select an employee to update from the list first.", 2000);
                return;
            }
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(profession))
            {
                Notification.Show("None of the fields can be empty", 2000);
                return;
            }
            var selectedEmployeeIndex = Employees.IndexOf(selectedEmployee);
            var newEmployee = new Employee(id, firstName, lastName, profession);
            Employees.Remove(selectedEmployee);
            Employees.Insert(selectedEmployeeIndex, newEmployee);
            Clear(sender, e);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void EmployeeEdit(object sender, DoubleTappedRoutedEventArgs e)
        {
            var employee = EmployeesGrid.SelectedItem as Employee;
            if (employee != null)
            {
                EmployeeIdInput.Text = employee.Id;
                FirstNameInput.Text = employee.FirstName;
                LastNameInput.Text = employee.LastName;
                ProfessionInput.Text = employee.Profession;
            }
        }
    }
}
