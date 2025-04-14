using System.ComponentModel.DataAnnotations;

namespace RickAndMorty.Data.Entities;

public class Location
{ 
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
}