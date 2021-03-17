using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace WpfTest.UI.Validations
{
    public class StringFieldValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string field = value.ToString();

            if (string.IsNullOrEmpty(field))
            {
                return new ValidationResult(false, "field can not be empty");
            }

            return ValidationResult.ValidResult;
        }
    }
}
