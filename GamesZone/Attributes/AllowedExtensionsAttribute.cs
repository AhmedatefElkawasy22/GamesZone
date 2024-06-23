using System.ComponentModel.DataAnnotations;

namespace GamesZone.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string _allowedExtensions;

        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                bool IsAllowd = _allowedExtensions.Split(',').Contains(extension,StringComparer.OrdinalIgnoreCase);
                if (!IsAllowd)
                {
                    return new ValidationResult($"This Extension Not Allowed please enter one of {_allowedExtensions}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
