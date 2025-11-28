
namespace OOP2.Application.Users.User
{
    public class CreateUserRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Website { get; set; }
        public string? AdressCity { get; set; }
        public string? AdressStreet { get; set; }
        public DateTime? BirthDate { get; set; }
        public float CoordinateLng { get; set; }
        public float CoordinateLat { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
