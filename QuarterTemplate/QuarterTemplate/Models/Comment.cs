using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required, StringLength(500)]

        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAccepted { get; set; }
        public int Rate { get; set; }
        public string Description { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }

    }
}
