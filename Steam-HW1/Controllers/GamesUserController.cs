using Microsoft.AspNetCore.Mvc;
using Steam_HW1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Steam_HW1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesUserController : ControllerBase
    {
        // GET: api/<GamesUserController>
        [HttpGet]
        public void Get()
        {
            return;
        }

       
        // GET api/<GamesUserController>/5
        [HttpGet("{id}")]
        public IEnumerable<GamesUser> Get(int id)
        {
            return GamesUser.Read(id);
        }
       
        // POST api/<GamesUserController>
        [HttpPost]
        public bool Post([FromBody] GamesUser gamesUser)
        {
            return GamesUser.Insert(gamesUser);
        }

        // PUT api/<GamesUserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("delete")]
        public int Delete(GamesUser gamesUser)
        {
            return GamesUser.DeleteGamesUser(gamesUser);

        }

        // DELETE api/<GamesUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
