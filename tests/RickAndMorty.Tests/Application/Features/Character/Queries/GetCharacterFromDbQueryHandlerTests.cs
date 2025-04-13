using FluentAssertions;
using NSubstitute;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Features.Character.Queries;

namespace RickAndMorty.Tests.Application.Features.Character.Queries;

public class GetCharacterFromDbQueryHandlerTests
{
    [Fact]
    public async Task HandleShouldReturnCharactersWhenCharactersExistInDb()
    {
        // Arrange
        var characterRepository = Substitute.For<ICharacterRepository>();
        var handler = new GetCharacterFromDbQueryHandler(characterRepository);
        var cancellationToken = CancellationToken.None;

        var characters = new List<Data.Entities.Character>
        {
            new Data.Entities.Character { Name = "Rick Sanchez" },
            new Data.Entities.Character { Name = "Morty Smith" }
        };
        characterRepository.GetAllAsync(1, 10, cancellationToken).Returns(characters);

        var query = new GetCharacterFromDbQuery(1, 10);

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(c => c.Name == "Rick Sanchez");
        result.Should().Contain(c => c.Name == "Morty Smith");
    }
}