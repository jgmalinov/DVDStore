﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DVDStore_RazorPages.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be within the range 1-100.")]
        public int DisplayOrder { get; set; }
    }
}
