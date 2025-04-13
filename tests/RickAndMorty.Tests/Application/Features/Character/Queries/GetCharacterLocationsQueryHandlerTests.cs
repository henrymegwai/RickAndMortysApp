using FluentAssertions;
using NSubstitute;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Features.Character.Queries;

namespace RickAndMorty.Tests.Application.Features.Character.Queries;

public class GetCharacterLocationsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnLocations_WhenLocationsExist()
    {
        // Arrange
        var characterRepository = Substitute.For<ICharacterRepository>();
        var handler = new GetCharacterLocationsQueryHandler(characterRepository);
        var cancellationToken = CancellationToken.None;

        var locations = new List<string?> { "Earth", "Mars" };
        characterRepository.GetAllDistinctLocationsAsync(cancellationToken).Returns(locations);

        var query = new GetCharacterLocationsQuery();

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain("Earth");
        result.Should().Contain("Mars");
    }
}