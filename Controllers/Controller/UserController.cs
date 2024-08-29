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
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            var userResponses = users.Select(u => new UserResponse(u.Id, u.Name, u.Email)).ToList();

            return Ok(userResponses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetById(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);

            if (user == null)
                return NotFound("User not found");

            return Ok(new UserResponse(user.Id, user.Name, user.Email));
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> Save(UserRequest request)
        {
            if (await _context.Users.AnyAsync(x => x.Email == request.Email))
                return Conflict("User already exists");

            var newUser = new User(request.Name, request.Email, request.Password);
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok(new UserResponse(newUser.Id, newUser.Name, newUser.Email));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> Update(Guid id, UserRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound("User not found");

            user.Name = request.Name;
            user.ChangeEmail(request.Email);
            user.ChangePassword(request.Password);

            await _context.SaveChangesAsync();

            return Ok(new UserResponse(user.Id, user.Name, user.Email));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
