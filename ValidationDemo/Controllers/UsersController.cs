using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationDemo.Models;

namespace ValidationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> Users = new List<User>();

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.Id = Users.Count + 1;
            Users.Add(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById([FromRoute] int id)
        {
            var user = Users.FirstOrDefault(x => x.Id == id);

            if(user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(Users);
        }
    }
}
