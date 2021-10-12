using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTask.Domain
{
    public interface IGamesService
    {
        Task CreateGameAsync(VideoGame game);
        Task<VideoGame> GetGameAsync(string name);
        Task<VideoGame> UpdateGameAsync(string name, UpdateData newData);
        Task DeleteGameAsync(string name);
        Task<IEnumerable<VideoGame>> GetAllGamesOfGenre(Genre genre);
    }
}