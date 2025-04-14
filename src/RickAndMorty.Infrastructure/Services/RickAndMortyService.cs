using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Common.Enums;
using RickAndMorty.Infrastructure.Services.Response;
using Character = RickAndMorty.Data.Entities.Character;

namespace RickAndMorty.Infrastructure.Services;
public class RickAndMortyService : IRickAndMortyService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RickAndMortyService> _logger;
    private readonly ICharacterRepository _characterRepository;

    public RickAndMortyService(
        HttpClient httpClient, 
        ICharacterRepository characterRepository, 
        ILogger<RickAndMortyService> logger)
    {
        _httpClient = httpClient;
        _characterRepository = characterRepository;
        _logger = logger;
       _httpClient.BaseAddress = new Uri("https://rickandmortyapi.com/api/");
    }

    public async Task<List<Character>> GetAllCharactersByStatusAsync(
        CharacterStatus characterStatus, 
        CancellationToken cancellationToken)
    {
        return characterStatus switch
        {
            CharacterStatus.Alive => await GetCharactersByStatusAsync(characterStatus, cancellationToken),
            _ => throw new ArgumentOutOfRangeException(nameof(characterStatus), characterStatus, null)
        };
    }
    
    private async Task<List<Character>> GetCharactersByStatusAsync(
        CharacterStatus characterStatus, 
        CancellationToken cancellationToken)
    {
            var listOfCharacters = new List<Character>();
            var nextPageUrl = "character/";

            try
            {
                while (!string.IsNullOrEmpty(nextPageUrl))
                {
                    _logger.LogInformation("Fetching characters from URL: {Url}", nextPageUrl);
                    var response = 
                        await _httpClient.GetFromJsonAsync<CharacterResponse>(nextPageUrl, cancellationToken);

                    var characters =
                        response?.Results.Where(c => 
                            c.Status.Equals($"{characterStatus}", StringComparison.OrdinalIgnoreCase));
 
                    if (characters != null)
                    {
                        listOfCharacters.AddRange(characters.Select(x => new Character
                        {
                            Name = x.Name,
                            Gender = x.Gender,
                            Species = x.Species,
                            Type = x.Type,
                            OriginName = x.Origin.Name,
                            OriginUrl = x.Origin.Url,
                            LocationName = x.Location.Name,
                            LocationUrl = x.Location.Url,
                            Image = x.Image,
                            Episodes = x.Episode,
                            Url = x.Url,
                            Created = x.Created, 
                            Status = x.Status
                        }));
                    }
                    
                    nextPageUrl = response!.Info.Next;
                }

                await _characterRepository.AddCollectionAsync(listOfCharacters, cancellationToken);

                return listOfCharacters;
            }
            catch (Exception e)
            {
                _logger.LogError("Error fetching characters from API: {Message}", e.Message);
                return [];
            }
    }
}