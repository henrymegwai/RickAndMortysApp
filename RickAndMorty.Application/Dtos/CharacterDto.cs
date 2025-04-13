namespace RickAndMorty.Application.Dtos;

public class CharacterDto
{
    public int Id { get; set; }
    public string Status { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Species { get; set; } = null!;
    public string? Type { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string? OriginName { get; set; } = null!;
    public string? OriginUrl { get; set; } = null!;
    public string? LocationName { get; set; } = null!;
    public string? LocationUrl { get; set; } = null!;
    public string? Image { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string?[]? Episodes { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.UtcNow;
}