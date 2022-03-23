using System.ComponentModel.DataAnnotations;
namespace playlistimport_blazor.Data;

public class User
{
    [Required]
    [StringLength(24, MinimumLength = 3, ErrorMessage = "User name must be between 3 and 24 characters!")]
    public string? UserName { get; set; }
    
    [Required]
    [StringLength(48, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters (max 48)!")]
    public string? Password { get; set; }
    
    [Required]
    [EmailAddress]
    public string? EmailAddress { get; set; }
}