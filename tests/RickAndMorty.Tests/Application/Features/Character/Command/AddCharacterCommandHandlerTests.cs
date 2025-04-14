
using FluentAssertions;
using MediatR;
using NSubstitute;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Features.Character.Commands;
using RickAndMorty.Application.Models;

namespace RickAndMorty.Tests.Application.Features.Character.Command;

public class AddCharacterCommandHandlerTests
{
    [Fact]
    public async Task HandleShouldAddNewCharacter()
    {
        // Arrange
        var characterRepository = Substitute.For<ICharacterRepository>();
        var handler = new AddCharacterCommandHandler(characterRepository);
        var cancellationToken = CancellationToken.None;

        var characterModel = new CharacterModel
        {
            Name = "Rick Sanchez",
            Status = "Alive",
            Species = "Human",
            Gender = "Male",
            OriginName = "Earth",
            OriginUrl = "https://example.com/origin",
            LocationName = "Earth",
            LocationUrl = "https://example.com/location",
            Url = "https://example.com/character",
            ImageUrl = "https://example.com/image"
        };

        var command = new AddCharacterCommand(characterModel);

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        result.Should().Be(Unit.Value);
        
        await characterRepository.Received(1).AddAsync(Arg.Is<Data.Entities.Character>(c =>
            c.Name == characterModel.Name &&
            c.Status == characterModel.Status &&
            c.Species == characterModel.Species &&
            c.Gender == characterModel.Gender && 
            c.OriginName == characterModel.OriginName && 
            c.OriginUrl == characterModel.OriginUrl &&
            c.LocationName ==  characterModel.LocationName && 
            c.LocationUrl == characterModel.LocationUrl && 
            c.Url == characterModel.Url && 
            c.Image == characterModel.ImageUrl
        ), cancellationToken);
    }
}