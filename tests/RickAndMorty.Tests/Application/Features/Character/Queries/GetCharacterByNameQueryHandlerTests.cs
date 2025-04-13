using FluentAssertions;
using NSubstitute;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Features.Character.Queries;

namespace RickAndMorty.Tests.Application.Features.Character.Queries;

public class GetCharacterByNameQueryHandlerTests
{
    [Fact]
    public async Task HandleShouldReturnCharacterWhenCharacterExists()
    {
        // Arrange
        var characterRepository = Substitute.For<ICharacterRepository>();
        var handler = new GetCharacterByNameQueryHandler(characterRepository);
        var cancellationToken = CancellationToken.None;

        var character = new Data.Entities.Character { Name = "Rick Sanchez" };
        characterRepository.GetByNameAsync("Rick Sanchez", cancellationToken).Returns(character);

        var query = new GetCharacterByNameQuery("Rick Sanchez");

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Rick Sanchez");
    }
}