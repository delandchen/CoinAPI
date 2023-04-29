using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoinAPI.Models;

namespace CoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CoinAPIDBContext _context;

        public UserController(CoinAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<Response>> GetUsers()
        {
            if (_context.Users == null)
            {
                return new Response
                {
                    statusCode = 500,
                    statusDescription = "Internal Server Error"

                };
            }

            var users = await _context.Users.ToListAsync();

            if (users == null)
            {
                return new Response
                {
                    statusCode = 404,
                    statusDescription = "Users not found"
                };
            }

            List<User> userList = users.Select(user => 
            new User
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailId = user.EmailId,
                EmailAddress = user.EmailAddress
            }).ToList();

            return new Response
            {
                statusCode = 200,
                statusDescription = "OK",
                users = userList
            };
        }
        
        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetUser(int? id)
        {
          if (_context.Users == null)
          {
                return new Response
                {
                    statusCode = 500,
                    statusDescription = "Internal Server Error"
                };
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return new Response
                {
                    statusCode = 404,
                    statusDescription = "User Not Found"
                };
            }

            var response = new Response
            {
                statusCode = 200,
                statusDescription = "OK",
                user = new User
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailId = user.EmailId,
                    EmailAddress = user.EmailAddress
                }
            };

            return response;
        }


        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostUser(User user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'MyFirstAPIDBContext.Users' is null.");
            }

            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.EmailAddress))
            {
                return BadRequest(new Response { statusCode = 400, statusDescription = "Bad Request: Missing required field(s)" });
            }

            var existingEmail = await _context.Emails.FirstOrDefaultAsync(e => e.Address == user.EmailAddress);
            if (existingEmail == null)
            {
                existingEmail = new Email { Address = user.EmailAddress };
                _context.Emails.Add(existingEmail);
                await _context.SaveChangesAsync();
            }

            user.EmailId = existingEmail.EmailId;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var response = new Response
            {
                statusCode = 201,
                statusDescription = "User created",
                user = user
            };

            return response;
        }

        // DELETE: api/user/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return new Response
                {
                    statusCode = 404,
                    statusDescription = "User not found"
                };
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new Response
                {
                    statusCode = 404,
                    statusDescription = "User not found"
                };
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new Response
            {
                statusCode = 200,
                statusDescription = "User deleted"
            };
        }

        private bool UserExists(int? id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
