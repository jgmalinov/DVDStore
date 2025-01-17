using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Background { get; set; }
        [Display(Name = "Movies Directed")]
        public IEnumerable<Movie> MoviesDirected {  get; set; }
        [Display(Name = "Filmography")]
        public IEnumerable<Movie> MoviesStarredIn { get; set; }
        [Display(Name = "Writer Credits")]
        public IEnumerable<Movie> MoviesWrittenFor { get; set; }
    }
}
