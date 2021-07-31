using System.ComponentModel.DataAnnotations;

namespace Kalum2021.ModelView
{
    public class ApellidoAttribute : ValidationAttribute
    {
         protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("El Aepllido es requerido");
            }            
            return ValidationResult.Success;
        }
    }
}