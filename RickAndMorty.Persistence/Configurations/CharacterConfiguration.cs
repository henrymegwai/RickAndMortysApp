using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RickAndMorty.Data.Entities;

namespace RickAndMorty.Persistence.Configurations;

public class CharacterConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Name).HasMaxLength(50);

        builder.Property(c => c.Status).IsRequired();

        builder.Property(c => c.Species).IsRequired();
        builder.Property(c => c.Species).HasMaxLength(50);

        builder.Property(c => c.Type).IsRequired(false);
        builder.Property(c => c.Type).HasMaxLength(50);

        builder.Property(c => c.Url).IsRequired();
        builder.Property(c => c.Url).HasMaxLength(50);

        builder.Property(c => c.OriginName).IsRequired(false);        
        builder.Property(c => c.OriginUrl).IsRequired(false);

        builder.Property(c => c.LocationName).IsRequired(false);
        builder.Property(c => c.LocationUrl).IsRequired(false);

        builder.Property(c => c.Image).IsRequired(false);

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        
        builder.Property(c => c.Episodes)
            .HasConversion(
                v => JsonSerializer.Serialize(v, jsonSerializerOptions),
                v => JsonSerializer.Deserialize<object>(v, jsonSerializerOptions)
            );
    }
}