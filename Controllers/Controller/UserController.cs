using Microsoft.AspNetCore.Mvc;
using Controllers.Data;
using Microsoft.EntityFrameworkCore;
using Controllers.Models;

namespace Controllers.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly EntityFrameworkContext _context;
        public UserController(EntityFrameworkContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var users = await _context.Users.ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Save(User user)
        {
            var userAlreadyExist = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

            if (userAlreadyExist != null)
                return Conflict("User already exist");

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok("User Created");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, User user)
        {
            var findUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (findUser == null)
                return NotFound("User not found");

            findUser.Name = user.Name;
            findUser.ChangeEmail(user.Email);
            findUser.ChangePassword(user.Password);

            await _context.SaveChangesAsync();

            return Ok("User Updated!");
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound("User not found");

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return Ok("User Deleted");
        }
    }
}