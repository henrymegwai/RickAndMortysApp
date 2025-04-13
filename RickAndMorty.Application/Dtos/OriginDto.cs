namespace RickAndMorty.Application.Dtos;

public record OriginDto
{
    public string? name { get; set; } = null!;
    public string? url { get; set; } = null!;
}