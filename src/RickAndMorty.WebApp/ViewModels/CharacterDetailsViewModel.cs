namespace RickAndMorty.WebApp.ViewModels;

public class CharacterDetailsViewModel
{
    public string Name { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Species { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string? OriginName { get; set; } = null!;
    public string? OriginUrl { get; set; } = null!;
    public string? LocationName { get; set; } = null!;
    public string? LocationUrl { get; set; } = null!;
    public string? Image { get; set; }
    public string? Url { get; set; } = null!;
    public DateTime Created { get; set; }
}