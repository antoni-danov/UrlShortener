namespace UrlShortener.Models.UserDtos.Login
{
    public class ExternalProviderDto
    {
        public string? Provider { get; set; }
        public string? IdToken { get; set; }
    }
}
