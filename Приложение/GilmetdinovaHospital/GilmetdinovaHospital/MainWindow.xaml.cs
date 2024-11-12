using System;
using System.Windows;


namespace GilmetdinovaHospital
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new DoctorPage());
            Manager.MainFrame = MainFrame;
        }

        private void BntBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BntBack.Visibility = Visibility.Visible;
            }
            else
            {
                BntBack.Visibility = Visibility.Hidden;
            }
        }
    }
}
