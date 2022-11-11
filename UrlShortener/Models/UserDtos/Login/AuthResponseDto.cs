namespace UrlShortener.Models.UserDtos.Login
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public IEnumerable<string>? RegistrationErrors { get; set; }
    }
}
