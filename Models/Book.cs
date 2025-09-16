using System.ComponentModel.DataAnnotations;

public class Book
{
    [Key]
    [Required]
    public string ISBN { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string AuthorName { get; set; }

    public string Description { get; set; }
}
