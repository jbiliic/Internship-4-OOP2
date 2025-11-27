using OOP2.Domain.Entities.User;

namespace OOP2.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class User {

            public static string CodePrefix { get; set; } = nameof(User);

            public static readonly ValidationItem FirstNameLen = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = $"Ime nesmije biti duze od {Entities.User.User.MaxNameLength} znakova",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };

            public static readonly ValidationItem LastNameLen = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = $"Prezime nesmije biti duze od {Entities.User.User.MaxNameLength} znakova",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };

            public static readonly ValidationItem EmailExists = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = $"Email vec postoji u bazi podataka",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem EmailInvalidFormat = new ValidationItem
            {
                Code = $"{CodePrefix}4",
                Message = $"Email nije validnog formata",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem UserNameExists = new ValidationItem
            {
                Code = $"{CodePrefix}5",
                Message = $"Korisnicko ime je zauzeto",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem BirthDateInvalid = new ValidationItem
            {
                Code = $"{CodePrefix}6",
                Message = $"Uneseni datum rodenja nije validan",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem CityAdressInvalid = new ValidationItem
            {
                Code = $"{CodePrefix}7",
                Message = $"Uneseni grad nije valjan",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem StreetAdressInvalid = new ValidationItem
            {
                Code = $"{CodePrefix}8",
                Message = $"Unesena ulica nije valjana",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem GeoLatError = new ValidationItem
            {
                Code = $"{CodePrefix}9",
                Message = $"Unesena Lat koordinata nije valjana",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem GeoLngError = new ValidationItem
            {
                Code = $"{CodePrefix}10",
                Message = $"Unesena Lng koordinata nije valjana",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem NotWithin3KmOfSplit = new ValidationItem
            {
                Code = $"{CodePrefix}11",
                Message = $"Korisnik je udaljeniji od 3km ",
                Severity = ValidationSeverity.Warning,
                Type = ValidationType.Security
            };
            public static readonly ValidationItem WebsiteInvalidFormat = new ValidationItem
            {
                Code = $"{CodePrefix}12",
                Message = $"URL nije validan",
                Severity = ValidationSeverity.Warning,
                Type = ValidationType.Performance
            };
        }
    }
}
