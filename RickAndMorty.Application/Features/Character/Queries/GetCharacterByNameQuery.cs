using MediatR;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Dtos;
using RickAndMorty.Application.Mapping;

namespace RickAndMorty.Application.Features.Character.Queries;


public record GetCharacterByNameQuery(string Name) : IRequest<CharacterDto>;

public class GetCharacterByNameQueryHandler(ICharacterRepository characterRepository)
    : IRequestHandler<GetCharacterByNameQuery, CharacterDto>
{
    public async Task<CharacterDto> Handle(GetCharacterByNameQuery request, CancellationToken cancellationToken)
    {
        var character = await characterRepository.GetByNameAsync(request.Name, cancellationToken);
        return character!.MapToDtoDetails();
    }
}