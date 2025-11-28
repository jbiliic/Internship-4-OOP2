using OOP2.Domain.Common.Validation;

namespace OOP2.Application.Common.Model
{
    public class Resault<TValue> where TValue : class
    {
        private List<ValidationResaultItem> _info = new List<ValidationResaultItem>();
        private List<ValidationResaultItem> _warnings = new List<ValidationResaultItem>();
        private List<ValidationResaultItem> _errors = new List<ValidationResaultItem>();

        public TValue? Value { get; set; }
        public Guid ReqId { get; init; } = Guid.NewGuid();

        public IReadOnlyList<ValidationResaultItem> Infos
        {
            get => _info.AsReadOnly();
            init => _info.AddRange(value);
        }
        public IReadOnlyList<ValidationResaultItem> Warnings
        {
            get => _warnings.AsReadOnly();
            init => _warnings.AddRange(value);
        }
        public IReadOnlyList<ValidationResaultItem> Errors
        {
            get => _errors.AsReadOnly();
            init => _errors.AddRange(value);

        }
        public bool hasErrors => _errors.Any();
        public bool hasWarnings => _warnings.Any();
        public bool hasInfos => _info.Any();

        public void setValue(TValue value)
        {
            Value = value;
        }
        public void setValidationResault(ValidationResault validationResault)
        {
            _info.AddRange(validationResault.ValidationItems
                .Where(vi => vi.Severity == ValidationSeverity.Info)
                .Select(ValidationResaultItem.FromValidationItem));
            _warnings.AddRange(validationResault.ValidationItems
                .Where(vi => vi.Severity == ValidationSeverity.Warning)
                .Select(ValidationResaultItem.FromValidationItem));
            _errors.AddRange(validationResault.ValidationItems
                .Where(vi => vi.Severity == ValidationSeverity.Error)
                .Select(ValidationResaultItem.FromValidationItem));
        }
    }
}
