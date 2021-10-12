using System.Collections.Generic;
using TestTask.Domain;

namespace TestTask.WebApi.Controllers
{
    public class UpdateVideoGameRequest
    {
        public string NewCompanyName { get; set; }
        public IEnumerable<Genre> NewGenres { get; set; }
    }
}