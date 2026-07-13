using System.ComponentModel.DataAnnotations;

namespace CerGlazerAPI.Models.Validations
{
    public class Glazes_EnsureIdealConeCategoryAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                return ValidationResult.Success;
            }

            var glaze = (Glaze)validationContext.ObjectInstance;

            if (glaze != null && !string.IsNullOrEmpty(glaze.IdealConeCategory))
            {
                var idealConeCategory = glaze.IdealConeCategory.ToLower();

                if (idealConeCategory != "low" && idealConeCategory != "medium" && idealConeCategory != "high")
                {
                    return new ValidationResult("Ideal Cone Category must be 'Low', 'Medium', or 'High'.");
                }
            }
            else if (glaze == null)
            {
                return new ValidationResult("One or more properties missing from glaze object model.");
            }
            else if (string.IsNullOrEmpty(glaze.IdealConeCategory))
            {
                return new ValidationResult("Ideal cone category must be specified.");
            }

            return ValidationResult.Success;

            }
        }
    }
