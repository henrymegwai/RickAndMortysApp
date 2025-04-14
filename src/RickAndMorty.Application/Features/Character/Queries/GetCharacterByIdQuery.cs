using MediatR;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Dtos;
using RickAndMorty.Application.Mapping;

namespace RickAndMorty.Application.Features.Character.Queries;

public record GetCharacterByIdQuery(int Id) : IRequest<CharacterDto>;

public class GetCharacterByIdQueryHandler(ICharacterRepository characterRepository)
    : IRequestHandler<GetCharacterByIdQuery, CharacterDto>
{
    public async Task<CharacterDto> Handle(GetCharacterByIdQuery request, CancellationToken cancellationToken)
    {
        var character = await characterRepository.GetByIdAsync(request.Id, cancellationToken);
        return character!.MapToDtoDetails();
    }
}