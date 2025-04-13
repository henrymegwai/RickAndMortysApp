using MediatR;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Dtos;
using RickAndMorty.Application.Mapping;

namespace RickAndMorty.Application.Features.Character.Queries;

public record GetCharacterFromDbQuery(int Page, int PageSize) : IRequest<List<CharacterDto>>;

public class GetCharacterFromDbQueryHandler(ICharacterRepository characterRepository)
    : IRequestHandler<GetCharacterFromDbQuery, List<CharacterDto>>
{
    public async Task<List<CharacterDto>> Handle(GetCharacterFromDbQuery request, CancellationToken cancellationToken)
    {
        var characters = 
            await characterRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);
        
        return characters.Select(x => x.MapToDto()).ToList();
    }
}