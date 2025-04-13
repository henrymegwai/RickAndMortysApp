using FluentAssertions;
using NSubstitute;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Features.Character.Queries;

namespace RickAndMorty.Tests.Application.Features.Character.Queries;

public class GetCharacterByPlanetQueryHandlerTests
{
    [Fact]
    public async Task HandleShouldReturnCharactersWhenCharactersExistOnPlanet()
    {
        // Arrange
        var characterRepository = Substitute.For<ICharacterRepository>();
        var handler = new GetCharacterByPlanetQueryHandler(characterRepository);
        var cancellationToken = CancellationToken.None;

        var characters = new List<Data.Entities.Character>
        {
            new Data.Entities.Character { Name = "Rick Sanchez", OriginName = "Earth" },
            new Data.Entities.Character { Name = "Morty Smith", OriginName = "Earth" }
        };
        characterRepository.GetAllAsync(1, 10, cancellationToken, "Earth").Returns(characters);

        var query = new GetCharacterByPlanetQuery( 1, 10,  "Earth");

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(c => c.Name == "Rick Sanchez");
        result.Should().Contain(c => c.Name == "Morty Smith");
    }
}