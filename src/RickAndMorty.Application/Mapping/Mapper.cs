using System.Text.Json;
using RickAndMorty.Application.Dtos;
using RickAndMorty.Application.Models;
using RickAndMorty.Data.Entities;

namespace RickAndMorty.Application.Mapping;

public static class Mapper
{
    public static CharacterDto MapToDto(this Character character)
    {
        return new CharacterDto
        {
            Id = character.Id,
            Name = character.Name,
            Status = character.Status,
            Species = character.Species,
            Type = character.Type,
            Gender = character.Gender,
            Image = character.Image,
            Episodes = character.Episodes as string[],
            Url = character.Url,
            Created = character.Created
        };
    }
    
    public static CharacterDto MapToDtoDetails(this Character character)
    {
        return new CharacterDto
        {
            Id = character.Id,
            Name = character.Name,
            Status = character.Status,
            Species = character.Species,
            Type = character.Type,
            Gender = character.Gender,
            OriginName = character?.OriginName,
            OriginUrl = character?.OriginUrl,
            LocationName = character?.LocationName,
            LocationUrl = character?.LocationUrl,
            Image = character?.Image,
            Episodes = MapToDtos<EpisodeDtos>(character?.Episodes)?.Select(e => e.ToString()).ToArray(),
            Url = character!.Url,
            Created = character.Created
        };
    }
    
    private static string[]? MapToDtos<T>(object? source) where T : class
    {
        if (source is null)
        {
            return null;
        }

        var json = source.ToString();
        if (string.IsNullOrEmpty(json))
        {
            return null;
        }
        
        var dtos =  JsonSerializer.Deserialize<string[]>(json);

        return dtos;
    }
    
    public static Character MapToEntity(this CharacterModel characterModel)
    {
        return new Character
        {
            Name = characterModel.Name,
            Status = characterModel.Status,
            Species = characterModel.Species,
            Type = characterModel.Type,
            Gender = characterModel.Gender,
            OriginName = characterModel.OriginName,
            OriginUrl = characterModel.OriginUrl,
            LocationName = characterModel.LocationName,
            LocationUrl = characterModel.LocationUrl,
            Image = characterModel.ImageUrl,
            Episodes = string.IsNullOrEmpty(characterModel.Episodes)
                ? null
                : JsonSerializer.Serialize(characterModel.Episodes),
            Url = characterModel.Url,
            Created = DateTime.UtcNow
        };
    }
}