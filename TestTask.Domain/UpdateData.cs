using System.Collections.Generic;

namespace TestTask.Domain
{
    public class UpdateData
    {
        public string NewCompanyName { get; set; }
        public IEnumerable<Genre> NewGenres { get; set; }
    }
}