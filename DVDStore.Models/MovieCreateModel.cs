using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class MovieCreateModel
    {
        public string Title { get; set; }
        [Required]
        [Display(Name = "Release Date")]
        public DateOnly ReleaseDate { get; set; }
        public string Summary { get; set; }
        [Required]
        [Range(1, 100)]
        [Display(Name = "Price for 1-5")]
        public double Price { get; set; }
        [Required]
        [Range(1, 100)]
        [Display(Name = "Price for 5-10")]
        public double Price5 { get; set; }
        [Required]
        [Range(1, 100)]
        [Display(Name = "Price for 5-10")]
        public double Price10 { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public int DirectorId { get; set; }
        public List<PersonCheckModel> Actors { get; set; } = new List<PersonCheckModel>();
        public List<PersonCheckModel> Writers { get; set; } = new List<PersonCheckModel>();
    }
}
