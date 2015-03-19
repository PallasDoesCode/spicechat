using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SpiceChat.Models;

namespace SpiceChat.Controllers
{
    public class UsersController : ApiController
    {
        private SpiceChatContext db = new SpiceChatContext();

        // GET: api/Users
        public IQueryable<UserDTO> GetUsers()
        {
            var users = from b in db.Users
                        select new UserDTO()
                        {
                            Id = b.Id,
                            UserID = b.UserID,
                            FirstName = b.FirstName,
                            LastName = b.LastName,
                            Role = b.Role
                        };

            return users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(UserDetailDTO))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            var user = await db.Users.Select(b =>
                new UserDetailDTO()
                {
                    Id = b.Id,
                    UserID = b.UserID,
                    FirstName = b.FirstName,
                    LastName = b.LastName,
                    DisplayName = b.DisplayName,
                    Role = b.Role
                }).SingleOrDefaultAsync(b => b.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            var dto = new UserDTO()
            {
                Id = user.Id,
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, dto);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}