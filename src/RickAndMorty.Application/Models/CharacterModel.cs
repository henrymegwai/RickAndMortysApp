using System.ComponentModel.DataAnnotations;
namespace RickAndMorty.Application.Models;

public class CharacterModel
{
    [Required (ErrorMessage = "Character Status e.g. 'Alive' or 'Dead' is required here.")]
    public string Status { get; set; } = null!;

    [StringLength(50, ErrorMessage = "Character Name cannot be longer than 50 characters.")]
    [Required(ErrorMessage = "Character Name is required here.")]
    public string Name { get; set; } = null!;
    
    [StringLength(50, ErrorMessage = "Character Species cannot be longer than 50 characters.")]
    [Required]
    public string Species { get; set; } = null!;
    
    [StringLength(10, ErrorMessage = "Character Type cannot be longer than 10 characters.")]
    public string? Type { get; set; }
    
    [StringLength(10, ErrorMessage = "Character Gender cannot be longer than 10 characters.")]
    [Required]
    public string Gender { get; set; } = null!; 
    
    [StringLength(50, ErrorMessage = "Character OriginName cannot be longer than 50 characters.")]
    public string OriginName { get; set; }  = null!;
    public string OriginUrl { get; set; } = null!;
    
    [Required] 
    public string LocationName { get; set; } = null!;
    
    [Required]
    public string LocationUrl { get; set; } = null!;
    
    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required(ErrorMessage = "Character Url is required here.")]
    public string Url { get; set; } = null!;

    [Required] public string Episodes { get; set; } = null!;
}