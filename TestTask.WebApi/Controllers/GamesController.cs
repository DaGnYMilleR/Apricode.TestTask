using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTask.Domain;

namespace TestTask.WebApi.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GamesController : Controller
    {
        private readonly IGamesService gamesService;

        public GamesController(IGamesService gamesService) //add mapper
        {
            this.gamesService = gamesService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateAlgorithmAsync([FromBody] CreateVideoGameRequest request)
        {
            var game = new VideoGame
                {Name = request.Name, DevelopersStudio = request.DevelopersStudio, Genres = request.Genres};
            await gamesService.CreateGameAsync(game);
            return Ok();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<VideoGame>> GetGameAsync([FromRoute] string name)
        {
            var game = await gamesService.GetGameAsync(name);
            return Ok(game);
        }

        [HttpPost("update/{name}")]
        public async Task<ActionResult> UpdateGameAsync([FromRoute] string name, [FromBody] UpdateVideoGameRequest request)
        {
            var updateData = new UpdateData {NewCompanyName = request.NewCompanyName, NewGenres = request.NewGenres};
            await gamesService.UpdateGameAsync(name, updateData);
            return Ok();
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> DeleteGameAsync([FromRoute] string name)
        {
            await gamesService.DeleteGameAsync(name);
            return Ok();
        }

        [HttpGet("genres/{genre}")]
        public async Task<ActionResult<List<VideoGame>>> GetGamesOfGenreAsync([FromRoute] Genre genre)
        {
            var games = await gamesService.GetAllGamesOfGenre(genre);
            return Ok(games);
        }
        
        [HttpGet("genres/{genre}")]
        public async Task<ActionResult<List<VideoGame>>> GetGamesOfGenreAsync([FromRoute] string genre)
        {
            if (!Enum.TryParse<Genre>(genre, true, out var res))
                return BadRequest($"No such genre: {genre}");
            var games = await gamesService.GetAllGamesOfGenre(res);
            return Ok(games);
        }
    }

    public class UpdateVideoGameRequest
    {
        public string NewCompanyName { get; set; }
        public IEnumerable<Genre> NewGenres { get; set; }
    }

    public class CreateVideoGameRequest
    {
        public string Name { get; set; }
        public string DevelopersStudio { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}