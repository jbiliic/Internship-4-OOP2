namespace OOP2.Domain.Common.Validation.ValidationItems
{
    public partial class ValidationItems
    {
        public static class Company
        {
            public static string CodePrefix { get; set; } = nameof(Company);

            public static readonly ValidationItem CompanyNameLen = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = $"Naziv tvrtke ne smije biti prazan ili duzi od 200 znakova",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem CompanyNameUnique = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = $"Naziv tvrtke vec postoji",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
        }
    }
}
