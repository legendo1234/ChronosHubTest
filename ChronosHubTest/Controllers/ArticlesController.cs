// Controllers/ArticlesController.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChronosHubTest.Data;
using ChronosHubTest.Models;

namespace ChronosHubTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IAcademicRepository _repo;

        public ArticlesController(IAcademicRepository repo)
        {
            _repo = repo;
        }

        // GET api/articles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetById(int id)
        {
            var article = await _repo.GetArticleByIdAsync(id);
            if (article == null) return NotFound();
            return Ok(article);
        }

        // GET api/articles/byAuthor/{authorId}
        [HttpGet("byAuthor/{authorId}")]
        public async Task<ActionResult<IEnumerable<Article>>> GetByAuthor(int authorId)
        {
            var list = await _repo.GetArticlesByAuthorAsync(authorId);
            return Ok(list);
        }

        // GET api/articles/byJournal/{journalId}
        [HttpGet("byJournal/{journalId}")]
        public async Task<ActionResult<IEnumerable<Article>>> GetByJournal(int journalId)
        {
            var list = await _repo.GetArticlesByJournalAsync(journalId);
            return Ok(list);
        }
    }
}
