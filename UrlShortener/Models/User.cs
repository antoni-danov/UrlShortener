using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class User
    {
        public User()
        {
            this.Urls = new List<UrlData>();
        }
        [Key]
        public Guid UserId { get; set; }
       
        [Required]
        public string Email { get; set; }

        public ICollection<UrlData> Urls { get; set; }
    }
}
