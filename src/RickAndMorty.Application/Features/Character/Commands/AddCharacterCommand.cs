using MediatR;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Mapping;
using RickAndMorty.Application.Models;

namespace RickAndMorty.Application.Features.Character.Commands;

public record AddCharacterCommand(CharacterModel CharacterModel) : IRequest<Unit>;

public class AddCharacterCommandHandler(ICharacterRepository characterRepository)
    : IRequestHandler<AddCharacterCommand, Unit>
{
    public async Task<Unit> Handle(AddCharacterCommand request, CancellationToken cancellationToken)
    {
        return await HandleCreationOfCharacter(request.CharacterModel, cancellationToken);
    }
    
    private async Task<Unit> HandleCreationOfCharacter(
        CharacterModel characterModel, 
        CancellationToken cancellationToken)
    {
        var character = characterModel.MapToEntity();
        await characterRepository.AddAsync(character, cancellationToken);
        return Unit.Value;
    }
}