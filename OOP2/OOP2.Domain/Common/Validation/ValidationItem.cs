namespace OOP2.Domain.Common.Validation
{
    public class ValidationItem
    {
        public ValidationSeverity Severity { get; init; }
        public ValidationType Type { get; init; }
        public string Code { get; init; }
        public string Message { get; init; }
    }
}
