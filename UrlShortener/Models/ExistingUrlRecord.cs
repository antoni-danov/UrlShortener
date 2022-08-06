using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class ExistingUrlRecord
    {
        [Required(ErrorMessage = "The field is required")]
        public string OriginalUrl { get; set; }

        [Required(ErrorMessage = "The field is required")]
        public string ShortUrl { get; set; }
    }
}
