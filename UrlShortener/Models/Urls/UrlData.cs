using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.Models;

public class UrlData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UrlId { get; set; }

    [Required(ErrorMessage = "This field is required")]
    public string? OriginalUrl { get; set; }

    [Required(ErrorMessage = "This field is required")]
    public string? ShortUrl { get; set; }

    [Required]
    public string? CreatedOn { get; set; }
    public string? Uid { get; set; }

}
