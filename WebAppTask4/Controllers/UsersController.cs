using Microsoft.AspNetCore.Mvc;
using WebAppTask4.Areas.Identity.Data;
using WebAppTask4.Data;

namespace Task4App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            this._context = context;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<AppUser> Get()
        {
            return _context.Users;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public AppUser Get(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
                return user;

            return null;
        }

        // POST api/<UsersController>
        [HttpPost]
        public int[] Post([FromBody] int[] value)
        {
            return value;
        }

        [HttpDelete]
        public async Task Delete([FromBody] Guid[] guids)
        {
            foreach (var guid in guids)
            {
                var deleteUser = await _context.Users.FindAsync(guid);

                if (deleteUser != null)
                {
                    _context.Users.Remove(deleteUser);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
