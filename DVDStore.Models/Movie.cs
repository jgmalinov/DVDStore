using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title {  get; set; }
        [Required]
        [Display(Name = "Release Date")]
        public DateOnly ReleaseDate {  get; set; }
        public string Summary {  get; set; }
        [Required]
        [Range(1, 100)]
        [Display(Name = "Price for 1-5")]
        public double Price {  get; set; }
        [Required]
        [Range(1, 100)]
        [Display(Name = "Price for 5-10")]
        public double Price5 {  get; set; }
        [Required]
        [Range(1, 100)]
        [Display(Name = "Price for 5-10")]
        public double Price10 {  get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId {  get; set; }
        public Category Category { get; set; }
        [ForeignKey(nameof(Director))]
        public int DirectorId {  get; set; }
        public Person Director { get; set; }
        public List<Person> Writers { get; set; }
        public List<Person> Actors {  get; set; }
    }
}
