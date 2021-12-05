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
using System.Windows.Shapes;
using System.Globalization;

namespace PL
{
    /// <summary>
    /// Interaction logic for ReleaseDroneDialog.xaml
    /// </summary>
    public partial class ReleaseDroneDialog : Window
    {
        TimeSpan result;
        public ReleaseDroneDialog()
        {
            InitializeComponent();
        }
        public static TimeSpan? GetResult()
        {
            ReleaseDroneDialog inst = new();
            inst.ShowDialog();
            if (inst.DialogResult==true)
                return inst.result;
            return null;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (TimeSpan.TryParseExact(ResponseTextBox.Text, "h\\:mm", CultureInfo.CurrentCulture, out result))
            {
                DialogResult = true;
                Close();
            }
            ResponseTextBox.BorderBrush = Brushes.Red;
            ErrorText.Text = "Not vaild TimeSpan!";
        }
    }
}
