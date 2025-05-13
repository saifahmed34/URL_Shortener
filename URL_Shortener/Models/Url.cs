using System;
using System.ComponentModel.DataAnnotations;

namespace URL_Shortener.Models
{
    public class Url
    {
        public int Id { get; set; }

        [Required]
        public string OriginalUrl { get; set; }

        [Required]
        public string ShortCode { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ClickCount { get; set; }
    }
}