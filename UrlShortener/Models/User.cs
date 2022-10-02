using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.Models
{
    public class User
    {
        public User()
        {
            this.UsersUrls = new HashSet<UrlData>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string Uid { get; set; }

        public ICollection<UrlData> UsersUrls { get; set; }
    }
}
