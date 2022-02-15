namespace E_proc.Models
{
    public class UserSignup
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string PasswordC { get; set; }
        public string Type { get; set; }

    }
}
