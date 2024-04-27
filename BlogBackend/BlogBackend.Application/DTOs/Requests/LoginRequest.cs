namespace BlogBackend.Application.DTOs.Requests
{
    [Serializable]
    public class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
