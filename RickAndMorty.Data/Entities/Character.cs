using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RickAndMorty.Data.Entities
{
    public class Character
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Species { get; set; } = null!;
        public string? Type { get; set; }
        public string Gender { get; set; } = null!;
        
        [Column("OriginName")]
        public string? OriginName { get; set; }
        [Column("OriginUrl")]
        public string? OriginUrl { get; set; }
        [Column("LocationName")]
        public string? LocationName { get; set; }
        [Column("LocationUrl")]
        public string? LocationUrl { get; set; }
        public string? Image { get; set; }
        public object? Episodes { get; set; }
        public string Url { get; set; } = null!;
        public DateTime Created { get; set; }
    }
}
