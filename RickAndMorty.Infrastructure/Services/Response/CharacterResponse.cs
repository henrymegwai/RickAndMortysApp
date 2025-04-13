using RickAndMorty.Data.Entities;

namespace RickAndMorty.Infrastructure.Services.Response
{
    public class CharacterResponse
    {
        public Info Info { get; set; } = null!;
        public List<CharacterFromService> Results { get; set; } = null!;
    }

    public class Info
    {
        public int Count { get; set; }
        public int Pages { get; set; }
        public string Next { get; set; } = null!;
        public string Prev { get; set; } = null!;
    }

    public class CharacterFromService
    {
        public string Name { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Species { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public Origin Origin { get; set; } = null!;
        public Location Location { get; set; } = null!;
        public string Image { get; set; } = null!;
        public List<string> Episode { get; set; } = null!;
        public string Url { get; set; } = null!;
        public DateTime Created { get; set; }
    }

    public class Location
    {
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}