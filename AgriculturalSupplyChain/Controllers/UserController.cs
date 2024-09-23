using AgriculturalSupplyChain.Data;
using AgriculturalSupplyChain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AgriculturalSupplyChain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AgriculturalSupplyChainDbContext _context;

        public UserController(AgriculturalSupplyChainDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public IActionResult PatchUser(int id, [FromBody] User updatedUser)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound();

            // Update only specific fields
            if (!string.IsNullOrEmpty(updatedUser.UserName))
                user.UserName = updatedUser.UserName;

            if (!string.IsNullOrEmpty(updatedUser.Email))
                user.Email = updatedUser.Email;

            if (updatedUser.Role != null)
                user.Role = updatedUser.Role;

            _context.SaveChanges();
            return NoContent();
        }


        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
