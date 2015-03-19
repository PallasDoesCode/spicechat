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
    public class ConversationsController : ApiController
    {
        private SpiceChatContext db = new SpiceChatContext();

        // GET: api/Conversations
        public IQueryable<ConversationDTO> GetConversations()
        {
            var conversations = from b in db.Conversations
                                select new ConversationDTO()
                                {
                                    Id = b.Id,
                                    CreatedAt = b.CreatedAt,
                                    CreatedBy = b.CreatedBy

                                };

            return conversations;
        }

        // GET: api/Conversations/5
        [ResponseType(typeof(ConversationDetailDTO))]
        public async Task<IHttpActionResult> GetConversation(int id)
        {
            var conversation = await db.Conversations.Select(b =>
                new ConversationDetailDTO()
                {
                    Id = b.Id,
                    CreatedAt = b.CreatedAt,
                    CreatedBy = b.CreatedBy,
                    Summary = b.Summary,
                    Description = b.Description,
                    UpdatedAt = b.UpdatedAt,
                    ClosedAt = b.ClosedAt
                }).SingleOrDefaultAsync(b => b.Id == id);

            if (conversation == null)
            {
                return NotFound();
            }

            return Ok(conversation);
        }

        // PUT: api/Conversations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutConversation(int id, Conversation conversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != conversation.Id)
            {
                return BadRequest();
            }

            db.Entry(conversation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConversationExists(id))
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

        // POST: api/Conversations
        [ResponseType(typeof(Conversation))]
        public async Task<IHttpActionResult> PostConversation(Conversation conversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Conversations.Add(conversation);
            await db.SaveChangesAsync();

            var dto = new ConversationDTO()
            {
                Id = conversation.Id,
                CreatedAt = conversation.CreatedAt,
                CreatedBy = conversation.CreatedBy
            };

            return CreatedAtRoute("DefaultApi", new { id = conversation.Id }, dto);
        }

        // DELETE: api/Conversations/5
        [ResponseType(typeof(Conversation))]
        public async Task<IHttpActionResult> DeleteConversation(int id)
        {
            Conversation conversation = await db.Conversations.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }

            db.Conversations.Remove(conversation);
            await db.SaveChangesAsync();

            return Ok(conversation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConversationExists(int id)
        {
            return db.Conversations.Count(e => e.Id == id) > 0;
        }
    }
}