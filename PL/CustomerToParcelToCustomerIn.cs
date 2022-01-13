using BO;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PL
{
    public class CustomerToWelcomeMessageConvertor : IValueConverter
    {
        public object Convert(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
            Customer customer = (Customer)value;
            if (customer != null)
            {
                return $"Log in as {customer.Name}";
            }
            else
            {
                return "Log in as Admin";
            }
        }

        public object ConvertBack(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
