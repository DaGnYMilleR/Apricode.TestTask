using System.Collections.Generic;
using TestTask.Domain;

namespace TestTask.WebApi.Controllers
{
    public class CreateVideoGameRequest
    {
        public string Name { get; set; }
        public string DevelopersStudio { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}