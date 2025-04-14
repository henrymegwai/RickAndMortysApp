using System.Text.Json;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Application.Common.Enums;
using RickAndMorty.Data.Entities;
using RickAndMorty.Infrastructure.Services;
using RickAndMorty.Infrastructure.Services.Response;
using RickAndMorty.Tests.Application.Features.Character.Command;
using Character = RickAndMorty.Data.Entities.Character;
using ICharacterRepository = RickAndMorty.Application.Abstractions.ICharacterRepository;
using Location = RickAndMorty.Infrastructure.Services.Response.Location;

namespace RickAndMorty.Tests.Infrastructure.Services;

public class RickAndMortyServiceTests
{
    private readonly RickAndMortyService _sut;
    private readonly ICharacterRepository _characterRepository;

    public RickAndMortyServiceTests()
    {
        var httpClient = new HttpClient(new FakeHttpMessageHandler());
        _characterRepository = Substitute.For<ICharacterRepository>();
        var logger = Substitute.For<ILogger<RickAndMortyService>>();
        _sut = new RickAndMortyService(httpClient, _characterRepository, logger);
    }
    [Fact]
    public async Task GetAllCharactersByStatusAsyncShouldReturnCharactersWithStatusAlive()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await _sut.GetAllCharactersByStatusAsync(CharacterStatus.Alive, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(c => c.Name == "Rick Sanchez" && c.Status == "Alive");
        result.Should().Contain(c => c.Name == "Morty Smith" && c.Status == "Alive");

        await _characterRepository.Received(1).AddCollectionAsync(Arg.Any<List<Character>>(), cancellationToken);
    }

    [Fact]
    public async Task GetAllCharactersByStatusAsyncShouldThrowArgumentOutOfRangeExceptionWhenStatusIsInvalid()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        // Act
        Func<Task> act = async () => await _sut.GetAllCharactersByStatusAsync((CharacterStatus)999, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }
}

// Fake HttpMessageHandler to mock API responses
public class FakeHttpMessageHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = new CharacterResponse
        {
            Info = new Info { Next = null!, Count = 2, Pages = 1 },
            Results =
            [
                new()
                {
                    Name = "Rick Sanchez",
                    Status = "Alive",
                    Gender = "Male",
                    Species = "Human",
                    Origin = new Origin { Name = "Earth", Url = "https://example.com/origin" },
                    Location = new Location { Name = "Earth", Url = "https://example.com/location" },
                    Image = "https://example.com/image",
                    Episode = ["https://example.com/episode/1"],
                    Url = "https://example.com/character/1",
                    Created = DateTime.UtcNow
                },
                new()
                {
                    Name = "Morty Smith",
                    Status = "Alive",
                    Gender = "Male",
                    Species = "Human",
                    Origin = new Origin { Name = "Earth", Url = "https://example.com/origin" },
                    Location = new Location { Name = "Earth", Url = "https://example.com/location" },
                    Image = "https://example.com/image",
                    Episode = ["https://example.com/episode/2"],
                    Url = "https://example.com/character/2",
                    Created = DateTime.UtcNow
                }
            ]
        };

        var jsonResponse = JsonSerializer.Serialize(response);
        var httpResponse = new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent(jsonResponse)
        };
        
        httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        return Task.FromResult(httpResponse);
    }
}