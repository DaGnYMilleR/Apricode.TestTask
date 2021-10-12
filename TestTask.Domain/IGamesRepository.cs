using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTask.Domain
{
    public interface IGamesRepository
    {
        Task AddAsync(VideoGame game);
        Task<VideoGame> GetAsync(string name);
        Task<IEnumerable<VideoGame>> GetGamesOfGenreAsync(Genre genre);
        Task<VideoGame> UpdateAsync(string name, UpdateData newData);
        Task DeleteAsync(string name);
    }
}