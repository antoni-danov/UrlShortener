using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class UrlData
    {
        private const bool _isActive = true;
        
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string OriginalUrl { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string ShortUrl { get; set; }

        [Required]
        public string CreatedOn { get; set; }
        [Required]
        [DefaultValue(_isActive)]
        public bool isActive { get; set; } = _isActive;
        public User User { get; set; }
    }
}
