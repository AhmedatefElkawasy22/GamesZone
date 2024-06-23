using System.ComponentModel.DataAnnotations;

namespace GamesZone.Attributes
{
    public class MaxFileSize : ValidationAttribute
    {
        private readonly int _maxSize;

        public MaxFileSize(int maxSize)
        {
            _maxSize = maxSize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
			if (file == null)
			{
				return ValidationResult.Success;
			}
			if (file.Length > _maxSize)
            {
                return new ValidationResult($"max size file must be {_maxSize} Bytes");
            }
            return ValidationResult.Success;
        }
    }
}
