using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models;

public class DeletedItem
{
    [Key]
    public int Id { get; set; }
   
    [Required]
    public int UrlId { get; set; }
    public UrlData? UrlData { get; set; }

    [Required]
    public string? CreatedOn { get; set; }

    [Required]
    public string? DeletedOn { get; set; }

}
