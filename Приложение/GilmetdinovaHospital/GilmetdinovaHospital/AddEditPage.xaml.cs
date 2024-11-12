using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace GilmetdinovaHospital
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {

        public DOCTOR _currentService = new DOCTOR();
       
        public AddEditPage(DOCTOR SelectedService)
        {
            InitializeComponent();


            if (SelectedService != null)
            {
                _currentService = SelectedService;
                ComboType.SelectedIndex = _currentService.DOCTOR_POST_ID - 1;

            }

            if (SelectedService != null)
                _currentService = SelectedService;
            //При инициализации установим DataContext страницы - этот созданный объект
            DataContext = _currentService;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder err = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentService.DOCTTOR_NAME))
                err.AppendLine("Укажите имя врача");
            if (string.IsNullOrWhiteSpace(_currentService.DOCTOR_SURNAME))
                err.AppendLine("Укажите фамилию врача");
            if (string.IsNullOrWhiteSpace(_currentService.DOCTOR_PATRONYMIC))
                err.AppendLine("Укажите отчество врача");

            //if (ComboType.SelectedItem == null)
            //    err.AppendLine("Укажите отделение ");
            if (ComboType.SelectedItem == null)
                err.AppendLine("Укажите отделение ");
            else
            {

                _currentService.DOCTOR_POST_ID = ComboType.SelectedIndex + 1;
            }

            if (string.IsNullOrWhiteSpace(_currentService.DOCTOR_SCIENTIFIC))
                err.AppendLine("Укажите образование врача");

            if (string.IsNullOrWhiteSpace(_currentService.DOCTOR_ADRESS))
                err.AppendLine("Укажите адрес врача");

            if (err.Length > 0)
            {
                MessageBox.Show(err.ToString());
                return;
            }


            if (_currentService.DOCTOR_ID == 0)
                GilmetdinovaHospitalEntities.GetContext().DOCTOR.Add(_currentService);

            try
            {
                GilmetdinovaHospitalEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void UploadPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if (myOpenFileDialog.ShowDialog() == true)
            {

                FileInfo fileInfo = new FileInfo(myOpenFileDialog.FileName);


                if (fileInfo.Length < 2 * 1024 * 1024)
                {
                    _currentService.MainImagePath = myOpenFileDialog.FileName;
                    _photo.Source = new BitmapImage(new Uri(myOpenFileDialog.FileName));
                }
                else
                {

                    MessageBox.Show("Размер файла превышает 2 мегабайта. Выберите другой файл.");
                }
            }
        }
    }
}
