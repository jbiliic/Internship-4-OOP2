using System.Text.RegularExpressions;
using OOP2.Domain.Common.Entity;
using OOP2.Domain.Common.Model;
using OOP2.Domain.Common.Validation;
using OOP2.Domain.Common.Validation.ValidationItems;
using OOP2.Domain.Repository.User;
using OOP2.Domain.Services.Helper;

namespace OOP2.Domain.Entities.User
{
    public class User : BaseEntity
    {
        public const int MaxNameLength = 100;
        public const int URLMaxLength = 200;
        public const string RegexMailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public const string RegexWebUrlPattern = @"^https?:\/\/[^\s/$.?#].[^\s]*$";

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Website { get; set; }
        public string? AdressCity { get; set; }
        public string? AdressStreet { get; set; }
        public DateTime? BirthDate { get; set; }
        public float CoordinateLng { get; set; }
        public float CoordinateLat { get; set; }
        public bool IsActive { get; set; } = true;

        public async Task<ValidationResault> ValidateBasic()
        {
            var validationResault = new ValidationResault();

            if (FirstName.Length > MaxNameLength || string.IsNullOrWhiteSpace(FirstName))
                validationResault.AddValidationItem(
                    ValidationItems.User.FirstNameLen
                );

            if (LastName.Length > MaxNameLength || string.IsNullOrWhiteSpace(LastName))
                validationResault.AddValidationItem(
                    ValidationItems.User.LastNameLen
                );

            if (!Regex.IsMatch(Email, RegexMailPattern))
                validationResault.AddValidationItem(
                    ValidationItems.User.EmailInvalidFormat
                );

            if (BirthDate != null)
            {
                var today = DateTime.UtcNow;

                if (BirthDate > today || BirthDate < today.AddYears(-100))
                    validationResault.AddValidationItem(
                        ValidationItems.User.BirthDateInvalid
                    );
            }


            if (CoordinateLat < -90 || CoordinateLat > 90)
                validationResault.AddValidationItem(
                    ValidationItems.User.GeoLatError
                );


            if (CoordinateLng < -180 || CoordinateLng > 180)
                validationResault.AddValidationItem(
                    ValidationItems.User.GeoLngError
                );

            if (!string.IsNullOrWhiteSpace(AdressCity))
                if (AdressCity.Length > MaxNameLength)
                    validationResault.AddValidationItem(
                        ValidationItems.User.CityAdressInvalid
                    );

            if (!string.IsNullOrWhiteSpace(AdressStreet))
                if (AdressStreet.Length > MaxNameLength)
                    validationResault.AddValidationItem(
                        ValidationItems.User.StreetAdressInvalid
                    );

            if (!HelperUser.IsWithin3KmOfSplit(CoordinateLat, CoordinateLng))
                validationResault.AddValidationItem(
                 ValidationItems.User.NotWithin3KmOfSplit
                  );

            if (Website != null)
                if (Website.Length > URLMaxLength || !Regex.IsMatch(Website, RegexWebUrlPattern))
                    validationResault.AddValidationItem(
                        ValidationItems.User.WebsiteInvalidFormat
                    );
            return validationResault;
        }


    }
}
