namespace BlogBackend.Application.DTOs.Requests
{
    [Serializable]
    public class RegisterRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
