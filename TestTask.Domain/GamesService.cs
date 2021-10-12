using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTask.Domain
{
    public class GamesService : IGamesService
    {
        private readonly IGamesRepository repository;

        public GamesService(IGamesRepository repository)
        {
            this.repository = repository;
        }
        
        public async Task CreateGameAsync(VideoGame game)//TODO add errors handling
        {
            await repository.AddAsync(game);
        }

        public async Task<VideoGame> GetGameAsync(string name)//TODO add errors handling
        {
            return await repository.GetAsync(name);
        }

        public async Task<VideoGame> UpdateGameAsync(string name, UpdateData newData)//TODO add errors handling
        {
            return await repository.UpdateAsync(name, newData);
        }

        public async Task<bool> DeleteGameAsync(string name)
        {
            try
            {
                await repository.DeleteAsync(name);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public async Task<IEnumerable<VideoGame>> GetAllGamesOfGenre(Genre genre)
        {
            return await repository.GetGamesOfGenreAsync(genre);
        }

        /*public async Task<IEnumerable<VideoGame>> GetAllGames()
        {
            return await repository.GetGamesAsync(g => true);
        }*/
    }
}