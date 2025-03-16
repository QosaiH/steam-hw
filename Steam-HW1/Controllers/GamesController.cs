using Microsoft.AspNetCore.Mvc;
using Steam_HW1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Steam_HW1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        // GET: api/<GamesController>
        [HttpGet]
        public IEnumerable<Game> Get()
        {
            return Game.Read();
        }

        // GET api/<GamesController>/5
        [HttpGet("{id}")]
        public Game Get(int id)
        {
            return Game.GetById(id);
        }

        [HttpGet("gamesInfo")]
        public Object GetUsersInfo()
        {
            return Game.GetGamesInfo();
        }

        [HttpGet("SearchByPrice")]
        public IEnumerable<Game> GetByPrice(double price, int id)
        {
            return Game.GetByPrice(price,id);

        }

        [HttpGet("searchByRank/rank/{rank}")] // this uses resource routing
        public IEnumerable<Game> GetByRank(int rank, int id)
        {
            return Game.GetByRank(rank, id);

        }

        [HttpDelete("delete/{appID}")]
        public IEnumerable<Game> DeleteById(int appID)
        {
            Game game = new Game();
            return game.DeleteById(appID);

        }
        /*
        // POST api/<GamesController>
        [HttpPost]
        public bool Post([FromBody] Game game)
        {
            return Game.Insert(game);
           
        }
        */

        [HttpPost]
        public void Post()
        {

        }


        // PUT api/<GamesController>/5
        [HttpPut("{id}")]
        public void Put(int id)
        {
        }

        // DELETE api/<GamesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
