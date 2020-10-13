using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfTestData.ValidationRules
{
    public class EmailRule : ValidationRule
    {
        private string email = String.Empty;
        public string Email { get => email; set => email = value; }

        private string customMessage = String.Empty;
        public string CustomMessage { get => customMessage; set => customMessage = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
           {
                if (!String.IsNullOrEmpty(value.ToString())) email = value.ToString();
                else
                    return new ValidationResult(false, "Адрес электронной почты не задан!");
                if (email.Contains("@") && email.Contains("."))
                    return new ValidationResult(true, null);
                else
                    return new ValidationResult(false, "Ошибка ввода. Шаблон адреса: adress@mymail.com");
            }
            catch (Exception)
            {
                return new ValidationResult(false, String.Format("{0}", CustomMessage));
            }
        }
    }
}
