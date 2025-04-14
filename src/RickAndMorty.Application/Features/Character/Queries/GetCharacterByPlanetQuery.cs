using MediatR;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Dtos;
using RickAndMorty.Application.Mapping;

namespace RickAndMorty.Application.Features.Character.Queries;

public record GetCharacterByPlanetQuery(int Page, int PageSize, string PlanetName) : IRequest<List<CharacterDto>>;

public class GetCharacterByPlanetQueryHandler(ICharacterRepository characterRepository)
    : IRequestHandler<GetCharacterByPlanetQuery, List<CharacterDto>>
{
    public async Task<List<CharacterDto>> Handle(GetCharacterByPlanetQuery request, CancellationToken cancellationToken)
    {
        var characters = 
            await characterRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken, request.PlanetName);
        return characters.Select(x => x.MapToDto()).ToList();
    }
}
