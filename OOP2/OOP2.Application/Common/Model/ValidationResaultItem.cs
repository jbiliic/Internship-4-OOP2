using OOP2.Domain.Common.Validation;

namespace OOP2.Application.Common.Model
{
    public class ValidationResaultItem
    {
        public ValidationSeverity Severity { get; init; }
        public ValidationType Type { get; init; }
        public string Code { get; init; }
        public string Message { get; init; }

        public static ValidationResaultItem FromValidationItem(ValidationItem item)
        {
            return new ValidationResaultItem
            {
                Severity = item.Severity,
                Type = item.Type,
                Code = item.Code,
                Message = item.Message
            };
        }
    }
}
