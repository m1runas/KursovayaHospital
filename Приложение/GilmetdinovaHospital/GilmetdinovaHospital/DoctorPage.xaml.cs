using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace GilmetdinovaHospital
{
    /// <summary>
    /// Логика взаимодействия для DoctorPage.xaml
    /// </summary>
    public partial class DoctorPage : Page
    {
        public DoctorPage()
        {
            InitializeComponent();
            var currentServices = GilmetdinovaHospitalEntities.GetContext().DOCTOR.ToList();
 


            ServiceListView.ItemsSource = currentServices;
           

            int ProductCount = 0;
            foreach (DOCTOR product in currentServices)
            {
                ProductCount++;
            }
            ProductMaxCountTextBlock.Text = ProductCount.ToString();




        }


        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }
        private void UpdateServices()
        {
            var currentServices = GilmetdinovaHospitalEntities.GetContext().DOCTOR.ToList();
            if (ComboType.SelectedIndex == 0)
            {
                currentServices = currentServices.Where(p => (p.DOCTOR_POST_ID == 1 || p.DOCTOR_POST_ID == 2 || p.DOCTOR_POST_ID == 3 || p.DOCTOR_POST_ID == 4 || p.DOCTOR_POST_ID == 5 || p.DOCTOR_POST_ID == 6
                || p.DOCTOR_POST_ID == 7 || p.DOCTOR_POST_ID == 8 || p.DOCTOR_POST_ID == 9 || p.DOCTOR_POST_ID == 10)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentServices = currentServices.Where(p => (p.DOCTOR_POST_ID ==1)).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentServices = currentServices.Where(p => (p.DOCTOR_POST_ID ==2)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentServices = currentServices.Where(p => (p.DOCTOR_POST_ID == 3)).ToList();
            }
            if (ComboType.SelectedIndex == 4)
            {
                currentServices = currentServices.Where(p => (p.DOCTOR_POST_ID == 4)).ToList();
            }
            if (ComboType.SelectedIndex == 5)
            {
                currentServices = currentServices.Where(p => (p.DOCTOR_POST_ID == 5)).ToList();
            }
            if (ComboType.SelectedIndex == 6)
            {
                currentServices = currentServices.Where(p => (p.DOCTOR_POST_ID == 6)).ToList();
            }
            if (ComboType.SelectedIndex == 7)
            {
                currentServices = currentServices.Where(p => (p.DOCTOR_POST_ID == 7)).ToList();
            }
            if (ComboType.SelectedIndex == 8)
            {
                currentServices = currentServices.Where(p => (p.DOCTOR_POST_ID == 8)).ToList();
            }
            if (ComboType.SelectedIndex == 9)
            {
                currentServices = currentServices.Where(p => (p.DOCTOR_POST_ID == 9)).ToList();
            }
            if (ComboType.SelectedIndex == 10)
            {
                currentServices = currentServices.Where(p => (p.DOCTOR_POST_ID == 10)).ToList();
            }
          


            currentServices = currentServices.Where(p => p.DOCTOR_SURNAME.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            if (RButtonDown.IsChecked.Value)
            {
                currentServices = currentServices.OrderByDescending(p => p.DOCTOR_SURNAME).ToList();
            }
            if (RButtonUp.IsChecked.Value)
            {
                currentServices = currentServices.OrderBy(p => p.DOCTOR_SURNAME).ToList();
            }

            ServiceListView.ItemsSource = currentServices;

            int ProductCount = 0;
            foreach (DOCTOR product in currentServices)
            {
                ProductCount++;
            }
            ProductCountTextBlock.Text = ProductCount.ToString();
            ServiceListView.ItemsSource = currentServices;
            
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
            UpdateServices();
            ServiceListView.Items.Refresh();
        }

        private void ServiceListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
            ServiceListView.Items.Refresh();
        }

        private void ServiceListView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateServices();
        }

        private void editDoctor_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as DOCTOR));
            UpdateServices();
            ServiceListView.Items.Refresh();
        }

        private void deleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            var currentDoctor = (sender as Button).DataContext as DOCTOR;
            var currentDoctorHistory = GilmetdinovaHospitalEntities.GetContext().OPERATION.ToList();
            currentDoctorHistory = currentDoctorHistory.Where(p => p.OPERATION_DOCTOR_ID == currentDoctor.DOCTOR_ID).ToList();

            if (currentDoctorHistory.Count != 0)
            {
                MessageBox.Show("Невозможно выполнить удаление, так как врача запланирована операция!");



            }
            else
            {
                if (MessageBox.Show("Вы точно хотите удалить?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    GilmetdinovaHospitalEntities.GetContext().DOCTOR.Remove(currentDoctor);
                    GilmetdinovaHospitalEntities.GetContext().SaveChanges();
                    ServiceListView.ItemsSource = GilmetdinovaHospitalEntities.GetContext().DOCTOR.ToList();
                    UpdateServices();
                }

            }


        }
    }
}
