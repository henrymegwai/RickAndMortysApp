using MediatR;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Common.Enums;
using RickAndMorty.Application.Dtos;
using RickAndMorty.Application.Mapping;

namespace RickAndMorty.Application.Features.Character.Queries;

public record GetCharacterByStatusFromServiceQuery(CharacterStatus CharacterStatus) : IRequest<List<CharacterDto>>
{
}

public class GetCharacterByStatusQueryHandler(IRickAndMortyService rickAndMortyService)
    : IRequestHandler<GetCharacterByStatusFromServiceQuery, List<CharacterDto>>
{
    public async Task<List<CharacterDto>> Handle(GetCharacterByStatusFromServiceQuery request, CancellationToken cancellationToken)
    {
        var characters = 
            await rickAndMortyService.GetAllCharactersByStatusAsync(request.CharacterStatus, cancellationToken);
        return characters.Select(x => x.MapToDto()).ToList();
    }
}