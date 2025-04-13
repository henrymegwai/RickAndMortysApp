using MediatR;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Mapping;
using RickAndMorty.Application.Models;
using RickAndMorty.Data.Entities;

namespace RickAndMorty.Application.Features.Character.Commands;

public record AddCharacterCommand(CharacterModel CharacterModel) : IRequest<Unit>;

public class AddCharacterCommandHandler : IRequestHandler<AddCharacterCommand, Unit>
{
    private readonly ICharacterRepository _characterRepository;

    public AddCharacterCommandHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }

    public async Task<Unit> Handle(AddCharacterCommand request, CancellationToken cancellationToken)
    {
        return await HandleCreationOfCharacter(request.CharacterModel, cancellationToken);
    }
    
    private async Task<Unit> HandleCreationOfCharacter(
        CharacterModel characterModel, 
        CancellationToken cancellationToken)
    {
        var character = characterModel.MapToEntity();
        await _characterRepository.AddAsync(character, cancellationToken);
        return Unit.Value;
    }
}