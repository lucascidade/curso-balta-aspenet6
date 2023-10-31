using Blog.Data;
using Blog.Models;
using Blog.Models.ViewModels;
using Blog.Models.ViewModels.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet("v1/posts")]

        public async Task<IActionResult> GetAsync(
            [FromServices] BlogDataContext context,
            [FromQuery] int page,
            [FromQuery] int pageSize
            )
        {
           
            try
            {
                var posts = await context.
      Posts
      .AsNoTracking()
      .Include(x => x.Category)
      .Include(x => x.Author)
      .Select(x =>

      new ListPostViewModel
      {
          Id = x.Id,
          Title = x.Title,
          Slug = x.Slug,
          LastUpdate = x.LastUpdateDate,
          Category = x.Category.Name,
          Author = $"{x.Author} ({x.Author.Email})"
      })
      .Skip(page * pageSize)
      .Take(pageSize)
      .OrderByDescending(x => x.LastUpdate)
      .ToListAsync();
                return Ok(new ResultViewModel<dynamic>(
                    new
                    {
                        page,
                        pageSize,
                        posts
                    }));
            } catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Post>("FALHA INTERNA DO SERVIODR"));
            }
  
        }

        [HttpGet("v1/posts/category/{category}")]
        public async Task<IActionResult> GetPostByCategory(
            [FromServices] BlogDataContext context,
            [FromRoute] string category,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25
            )
        {
            try
            {
                var count = await context.Posts.AsNoTracking().CountAsync();
                var posts = await context.Posts
                    .AsNoTracking()
                    .Include(x => x.Author)
                    .Include(x => x.Category)
                    .Where(x => x.Category.Slug == category)
                    .Select(x =>
                     new ListPostViewModel {
                         Id = x.Id,
                         Title = x.Title,
                         Slug = x.Slug,
                         LastUpdate = x.LastUpdateDate,
                         Category = x.Category.Name

                     })
                     .Skip(page * pageSize)
                     .Take(pageSize)
                     .OrderByDescending(x => x.LastUpdate)
                    .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(
                    new
                    {
                        total = count,
                        page,
                        pageSize,
                        posts
                    }));
            }
            catch
            {
                return BadRequest(new ResultViewModel<List<Post>>("Erro interno servidor"));
            }
        }
    }
}
