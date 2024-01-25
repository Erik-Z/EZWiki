using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;
using NodaTime.Extensions;

namespace EZWiki.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public required string Topic { get; set; }

        public string Slug { get; set; }

        [NotMapped]
        public Instant Published { get; set; }

        [Obsolete("Exists for entity framework serialization")]
        [DataType(DataType.DateTime)]
        [Column("Published")]
        public DateTime PublishedDateTime { 
            get => Published.ToDateTimeUtc(); 
            set => Published = DateTime.SpecifyKind(value, DateTimeKind.Utc).ToInstant();
        }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
