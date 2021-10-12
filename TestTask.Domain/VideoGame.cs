using System.Collections.Generic;

namespace TestTask.Domain
{
    public class VideoGame
    {
        public string Name { get; set; }
        public string DevelopersStudio { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}