using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Steam_HW1.Models;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Steam_HW1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<Userr> Get()
        {
            return Userr.Read();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("usersInfo")]
        public Object GetUsersInfo()
        {
            return Userr.GetUsersInfo();
        }

        // POST api/<UsersController>
        [HttpPost]
        public int Post([FromBody] Userr user)
        {
            return Userr.Insert(user);
        }

        [HttpPost("Register")]
        public int Register([FromBody] Userr user)
        {
            return Userr.Insert(user);
        }
        [HttpPost("Login")]
        public int login([FromBody] Userr user)
        {
            return Userr.Login(user.Email,user.Password);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public int Put( [FromBody] Userr user)
        {
            return Userr.Update(user);
        }

        [HttpPut("{id}/{isActive}")]
        public int isActivePut(int id, bool isActive)
        {
            return Userr.updateIsActive(id,isActive);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
