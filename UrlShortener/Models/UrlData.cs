using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class UrlData
    {
        
        [Key]
        public Guid UrlId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string OriginalUrl { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string ShortUrl { get; set; }

        [Required]
        public string CreatedOn { get; set; }
       
    }
}
