namespace OOP2.Domain.Common.Validation
{
    public class ValidationResault
    {
        private List<ValidationItem> _validationItems = new List<ValidationItem>();
        public IReadOnlyList<ValidationItem> ValidationItems => _validationItems.AsReadOnly();

        public bool HasErrors => _validationItems.Any(vi => vi.Severity == ValidationSeverity.Error);
        public bool HasWarnings => _validationItems.Any(vi => vi.Severity == ValidationSeverity.Warning);
        public bool HasInfos => _validationItems.Any(vi => vi.Severity == ValidationSeverity.Info);

        public void AddValidationItem(ValidationItem item)
        {
            _validationItems.Add(item);
        }
    }
}
