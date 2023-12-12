using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;
using NodaTime.Extensions;

namespace EZWiki.Models
{
    public class Article
    {
        [Required, Key, MaxLength(100)]
        public string Topic { get; set; }

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
