namespace RickAndMorty.Application.Dtos;

public record LocationDto
{
    public string? name { get; set; } = null!;
    public string? url { get; set; } = null!;
}