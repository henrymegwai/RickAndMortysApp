using FluentAssertions;
using NSubstitute;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Common.Enums;
using RickAndMorty.Application.Features.Character.Queries;

namespace RickAndMorty.Tests.Application.Features.Character.Queries;

public class GetCharacterByStatusQueryHandlerTests
{
    [Fact]
    public async Task HandleShouldReturnCharactersWhenCharactersExistWithStatus()
    {
        // Arrange
        var rickAndMortyService = Substitute.For<IRickAndMortyService>();
        var handler = new GetCharacterByStatusQueryHandler(rickAndMortyService);
        var cancellationToken = CancellationToken.None;

        var characters = new List<Data.Entities.Character>
        {
            new Data.Entities.Character { Name = "Rick Sanchez", Status = "Alive" },
            new Data.Entities.Character { Name = "Morty Smith", Status = "Alive" }
        };
        rickAndMortyService.GetAllCharactersByStatusAsync(CharacterStatus.Alive, cancellationToken).Returns(characters);

        var query = new GetCharacterByStatusFromServiceQuery(CharacterStatus.Alive);

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(c => c.Name == "Rick Sanchez");
        result.Should().Contain(c => c.Name == "Morty Smith");
    }
}