using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAuthentication.Data;
using WebApiAuthentication.Models;

namespace WebApiAuthentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogsController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        //GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blogs>>> GetBlogs()
        {
            return await _context.Blogs.ToListAsync();
        }

        //GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blogs>> GetBlogs(int id)
        {
            var blogs = await _context.Blogs.FindAsync(id);

            if (blogs == null) return NotFound();

            return blogs;
        }

        //POST: api/Blogs
        [HttpPost]
        public async Task<ActionResult<Blogs>> PostBlogs(Blogs blogs)
        {
            _context.Blogs.Add(blogs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogs", new { id = blogs.BlogId }, blogs);
        }

        //DELETE: api/Blogs/6
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBlogs(int id)
        {
            if (!BlogsExists(id)) return NotFound();

            var blogToDelete = await _context.Blogs.FindAsync(id);

            _context.Blogs.Remove(blogToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //PUT: api/Blogs/8
        [HttpPut("{id}")]
        public async Task<ActionResult> PutBlogs(int id, Blogs blogs)
        {
            if (id != blogs.BlogId) return BadRequest();

            _context.Entry(blogs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return NoContent();
        }

        private bool BlogsExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }
    }
}
