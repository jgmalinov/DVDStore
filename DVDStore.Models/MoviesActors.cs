using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class MoviesActors
    {
        public int MovieId {  get; set; }
        public int ActorId { get; set; }
    }
}
