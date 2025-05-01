using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace URL_Shortener.Models
{
    public class Url
    {
        [Key]
        public int id{ get; set; }
        public required string url{ get; set; }
        public required string shorturl { get; set; }
        public DateTime DateTime{ get; set; } = DateTime.Now;
    }
}
