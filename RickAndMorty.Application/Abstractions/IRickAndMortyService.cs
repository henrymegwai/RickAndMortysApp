using RickAndMorty.Application.Common.Enums;
using RickAndMorty.Data.Entities;

namespace RickAndMorty.Application.Abstractions;

public interface IRickAndMortyService
{
    Task<List<Character>> GetAllCharactersByStatusAsync(
        CharacterStatus characterStatus, 
        CancellationToken cancellationToken);
}
