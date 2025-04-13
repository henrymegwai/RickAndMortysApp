using RickAndMorty.Data.Entities;

namespace RickAndMorty.Application.Abstractions;

public interface ICharacterRepository
{
    Task<List<Character>> GetAllAsync(
        int page, 
        int pageSize, 
        CancellationToken cancellationToken,
        string? planetName = null);
    Task<List<string?>> GetAllDistinctLocationsAsync(CancellationToken cancellationToken);
    Task<Character?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Character?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task AddAsync(Character character, CancellationToken cancellationToken);
    Task<List<Character>> AddCollectionAsync(List<Character> listOfItems, CancellationToken cancellationToken);
}