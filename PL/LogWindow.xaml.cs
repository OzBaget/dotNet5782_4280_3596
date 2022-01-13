using BlApi;
using System.Media;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        private static IBL db;
        private bool exit = false;
        public LogWindow()
        {
            InitializeComponent();
            db = BlFactory.GetBl();


        }
        private void Register(object sender, RoutedEventArgs e)
        {
            new ViewCustomer().ShowDialog();
        }

        private void logIn(object sender, RoutedEventArgs e)
        {
            int id;
            if (!int.TryParse(logInBox.Text, out id))
            {
                errorBox.Text = "Id not valid, try again";
                return;
            }
            try
            {
                new ViewParcelList(db.GetCustomer(id)).Show();
                errorBox.Text = "";
            }
            catch (IdNotFoundException)
            {
                errorBox.Text = "ID not found in the system, try again";
                return;
            }



        }

        private void loginAdmin(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            Close();
        }
        private void CloseWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!exit)
            {
                e.Cancel = true;
                SystemSounds.Beep.Play();
            }
        }
    }
}
