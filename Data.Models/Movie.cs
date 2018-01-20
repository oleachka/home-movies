using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MoviesApp.Data.Models
{

    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }        
        public int Genre { get; set; }
        public string GenreName { get; set; }
        public float Rating { get; set; }
        [JsonIgnoreAttribute]
        public IEnumerable<Actor> Actors { get; set;}

        public string ActorList
        {
            set
            {
                Actors = value
                    .Split(',')
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => new Actor { ActorName = s });
            }
            get
            {
                if (Actors?.Any() == false)
                    return "";
                else
                    return Actors.Select(actor => $"{actor.ActorName}")
                        .Aggregate((current, next) => $"{current}, {next}");
            }
        }
        
    }    
}
