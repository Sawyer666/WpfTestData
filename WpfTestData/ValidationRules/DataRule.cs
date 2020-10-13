using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfTestData.ValidationRules
{
    public class DataRule : ValidationRule
    {
        private string data = String.Empty;
        public string Data { get => data; set => data = value; }

        private string customMessage = String.Empty;
        public string CustomMessage { get => customMessage; set => customMessage = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (!String.IsNullOrEmpty(value.ToString())) data = value.ToString();
                else
                    return new ValidationResult(false, "Строка не может быть пустой!");
                if (!Regex.IsMatch(value.ToString(), @"^[\p{L}]+$"))
                    return new ValidationResult(false, "Ввод цифр недопустим");
                else
                    return new ValidationResult(true, null);
            }
            catch (Exception)
            {
                return new ValidationResult(false, String.Format("{0}", CustomMessage));
            }
        }
    }
}
