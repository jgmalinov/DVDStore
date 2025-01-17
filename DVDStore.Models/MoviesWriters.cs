using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class MoviesWriters
    {
        public int MovieId {  get; set; }
        public int WriterId {  get; set; }
    }
}
