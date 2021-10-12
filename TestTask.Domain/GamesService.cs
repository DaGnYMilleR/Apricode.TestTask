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
            if(!await repository.IsGameExistsAsync(game.Name))
                await repository.AddAsync(game);
            else
                throw new ArgumentException($"game with name \"{game.Name}\" already exists");
        }

        public async Task<VideoGame> GetGameAsync(string name)//TODO add errors handling
        {
            if(await repository.IsGameExistsAsync(name))
                return await repository.GetAsync(name);
            throw new ArgumentException($"game with name \"{name}\" doesn't exists");
        }

        public async Task<VideoGame> UpdateGameAsync(string name, UpdateData newData)//TODO add errors handling
        {
            if(await repository.IsGameExistsAsync(name))
                return await repository.UpdateAsync(name, newData);
            throw new ArgumentException($"game with name \"{name}\" doesn't exists");
        }

        public async Task DeleteGameAsync(string name)
        {
            if (await repository.IsGameExistsAsync(name))
                await repository.DeleteAsync(name);
            throw new ArgumentException($"game with name \"{name}\" doesn't exists");
        }

        public async Task<IEnumerable<VideoGame>> GetAllGamesOfGenre(Genre genre) 
            => await repository.GetGamesOfGenreAsync(genre);
    }
}