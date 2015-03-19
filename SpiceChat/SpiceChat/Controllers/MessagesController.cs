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
    public class MessagesController : ApiController
    {
        private SpiceChatContext db = new SpiceChatContext();

        // GET: api/Messages
        public IQueryable<MessageDTO> GetMessages()
        {
            var messages = from b in db.Messages
                          select new MessageDTO()
                          {
                              Id = b.Id,
                              Body = b.Body,
                              CreatedAt = b.CreatedAt,
                              ConversationID = b.ConversationID,
                              CreatedBy = b.CreatedBy
                          };

            return messages;
        }

        // GET: api/Messages/5
        [ResponseType(typeof(MessageDetailDTO))]
        public async Task<IHttpActionResult> GetMessage(int id)
        {
            var message = await db.Messages.Select(b =>
                new MessageDetailDTO()
                {
                    Id = b.Id,
                    Body = b.Body,
                    CreatedAt = b.CreatedAt,
                    AttachmentLocation = b.AttachmentLocation,
                    AttachmentContentType = b.AttachmentContentType,
                    AttachmentName = b.AttachmentName,
                    ConversationID = b.ConversationID,
                    CreatedBy = b.CreatedBy
                }).SingleOrDefaultAsync(b => b.Id == id);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // PUT: api/Messages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMessage(int id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.Id)
            {
                return BadRequest();
            }

            db.Entry(message).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Messages.Add(message);
            await db.SaveChangesAsync();

            var dto = new MessageDTO()
            {
                Id = message.Id,
                Body = message.Body,
                CreatedAt = message.CreatedAt,
                ConversationID = message.ConversationID,
                CreatedBy = message.CreatedBy
            };

            return CreatedAtRoute("DefaultApi", new { id = message.Id }, dto);
        }

        // DELETE: api/Messages/5
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> DeleteMessage(int id)
        {
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
            await db.SaveChangesAsync();

            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(int id)
        {
            return db.Messages.Count(e => e.Id == id) > 0;
        }
    }
}