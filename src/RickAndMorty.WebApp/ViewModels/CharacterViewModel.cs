using RickAndMorty.Application.Dtos;

namespace RickAndMorty.WebApp.ViewModels;

public class CharacterViewModel
{
    public List<CharacterDto>? Characters { get; set; } = new();
    public bool FromCache { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
}