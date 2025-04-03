using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/")]
    public class EventsController : ControllerBase
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        [HttpGet("events/")]
        public int GetEventsCount()
        {
            return _context.Events.Count();
        }

        [HttpGet("events/min_date")]
        public DateTime GetMinDate()
        {
            return (DateTime)_context.Events.Min(e => e.EventDateTime);
        }

        [HttpGet("events/future_events")]
        public List<Event> GetFutureEvents()
        {
            return _context.Events
                .Where(e => e.EventDateTime.Value.Year > 2025 && e.Title.Contains("konferencia"))
                .ToList();
        }
        [HttpGet("events/titles_sorted")]
        public Dictionary<string, DateTime> GetSortedTitles()
        {
            return _context.Events
                .Where(e => e.EventDateTime.HasValue
                            && e.Title != null)
                .OrderBy(e => e.Title)
                .ToDictionary(e => e.Title!, e => e.EventDateTime!.Value);
        }

        [HttpGet("events/by_author/{author_id}")]
        public List<Event> GetFutureEvents(int author_id)
        {
            return _context.Events
                .Where(e => e.AuthorId == author_id)
                .ToList();
        }

        [HttpGet("events/update_title")]
        public async Task GetFutureEvents(int id, string newTitle)
        {
            var ev = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (ev != null)
            {
                ev.Title = newTitle;
                _context.Events.Update(ev);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Event not found");
            }
        }

        [HttpGet("/authors/event_count")]
        public Dictionary<string, int>GetAuthorsCount()
        {
            var e = _context.Events.GroupBy(e=> e.AuthorId).ToDictionary(g => g.Key.ToString(), g => g.Count());

            return e;
        }

        [HttpGet("events/delete/{id}")]
        public async Task DeleteEvent(int id)
        {
            var ev = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (ev != null)
            {
                _context.Events.Remove(ev);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Event not found");
            }
        }

        [HttpGet("api/events_with_authors")]
        public ActionResult<object> GetEventsWithAuthors()
        {
            var result = (from e in _context.Events
                          join a in _context.Authors on e.AuthorId equals a.Id
                          select new EventWithAuthor
                          {
                              Id = e.Id,
                              Title = e.Title ?? "",
                              Author = a.Name
                          }).ToList();

            return Ok(new { events = result });
        }


    }
}
