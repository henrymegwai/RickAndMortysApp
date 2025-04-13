namespace RickAndMorty.WebApp.ViewModels;

public class AddCharacterViewModel
{
    public string Status { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Species { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public object? Origin { get; set; } = null!;
    public object? Location { get; set; }
    public string Image { get; set; } = null!;
    public string Url { get; set; } = null!;
    public object? Episodes { get; set; } = null!;
    public DateTime Created { get; set; }
}