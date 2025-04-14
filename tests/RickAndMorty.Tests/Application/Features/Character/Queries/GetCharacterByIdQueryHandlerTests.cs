using FluentAssertions;
using NSubstitute;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Features.Character.Queries;
using RickAndMorty.Tests.Application.Features.Character.Command;
using ICharacterRepository = RickAndMorty.Application.Abstractions.ICharacterRepository;

namespace RickAndMorty.Tests.Application.Features.Character.Queries;

public class GetCharacterByIdQueryHandlerTests
{
    [Fact]
    public async Task HandleShouldReturnCharacterWhenCharacterExists()
    {
        // Arrange
        var characterRepository = Substitute.For<ICharacterRepository>();
        var handler = new GetCharacterByIdQueryHandler(characterRepository);
        var cancellationToken = CancellationToken.None;

        var character = new Data.Entities.Character { Id = 1, Name = "Rick Sanchez" };
        characterRepository.GetByIdAsync(1, cancellationToken).Returns(character);

        var query = new GetCharacterByIdQuery(1);

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Rick Sanchez");
    }
}