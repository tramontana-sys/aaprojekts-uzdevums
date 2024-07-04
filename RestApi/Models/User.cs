using System.ComponentModel.DataAnnotations;

namespace RestApi.Models;

public class User
{
    public int Id { get; init; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    public bool Verified { get; set; }
}