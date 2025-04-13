using MediatR;
using RickAndMorty.Application.Abstractions;

namespace RickAndMorty.Application.Features.Character.Queries;

public record GetCharacterLocationsQuery : IRequest<List<string?>>;

public class GetCharacterLocationsQueryHandler(ICharacterRepository characterRepository)
    : IRequestHandler<GetCharacterLocationsQuery, List<string?>>
{
    public async Task<List<string?>> Handle(GetCharacterLocationsQuery request, CancellationToken cancellationToken)
    {
        var locations = await characterRepository.GetAllDistinctLocationsAsync(cancellationToken);
        return locations;
    }
}