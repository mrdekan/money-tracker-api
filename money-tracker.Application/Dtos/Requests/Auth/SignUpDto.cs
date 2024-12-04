namespace money_tracker.Application.Dtos.Requests.Auth
{
    public class SignUpDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
    }
}
