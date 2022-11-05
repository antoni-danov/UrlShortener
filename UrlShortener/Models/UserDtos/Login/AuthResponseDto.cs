namespace UrlShortener.Models.UserDtos.Login
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public string? Email { get; set; }
        public string? Uid { get; set; }
    }
}
