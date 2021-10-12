using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestTask.Domain;

namespace TestTask.DataAccess
{
    public class DataBaseVideoGame
    {
        [Key] public string Name { get; set; }
        public string DevelopersStudio { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}