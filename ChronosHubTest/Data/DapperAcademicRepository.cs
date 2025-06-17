// Data/DapperAcademicRepository.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ChronosHubTest.Models;

namespace ChronosHubTest.Data
{
    public class DapperAcademicRepository : IAcademicRepository
    {
        private readonly IDbConnection _db;
        public DapperAcademicRepository(IDbConnection db) => _db = db;

        public async Task<Article?> GetArticleByIdAsync(int id)
        {
            const string sqlArticle = @"
                SELECT Id, Title, Abstract, PublicationDate, JournalId
                  FROM Articles
                 WHERE Id = @id";
            var art = await _db.QuerySingleOrDefaultAsync<ArticleRow>(sqlArticle, new { id });
            if (art == null) return null;

            const string sqlJournal = @"
                SELECT Id, Name, Issn, Publisher
                  FROM Journals
                 WHERE Id = @JournalId";
            var journal = await _db.QuerySingleAsync<Journal>(
                sqlJournal, new { art.JournalId });

            const string sqlAuthors = @"
                SELECT a.Id, a.FirstName, a.LastName, a.Affiliation
                  FROM Authors a
                  JOIN ArticleAuthors aa ON a.Id = aa.AuthorId
                 WHERE aa.ArticleId = @id";
            var authors = (await _db.QueryAsync<Author>(sqlAuthors, new { id })).ToList();

            return new Article
            {
                Id = art.Id,
                Title = art.Title,
                Abstract = art.Abstract,
                PublicationDate = art.PublicationDate,
                Journal = journal,
                Authors = authors
            };
        }

        public async Task<IEnumerable<Article>> GetArticlesByAuthorAsync(int authorId)
        {
            const string sqlIds = @"
                SELECT ArticleId
                  FROM ArticleAuthors
                 WHERE AuthorId = @authorId";
            var ids = await _db.QueryAsync<int>(sqlIds, new { authorId });
            var list = new List<Article>();
            foreach (var aid in ids)
            {
                var article = await GetArticleByIdAsync(aid);
                if (article != null) list.Add(article);
            }
            return list;
        }

        public async Task<IEnumerable<Article>> GetArticlesByJournalAsync(int journalId)
        {
            const string sqlIds = @"
                SELECT Id
                  FROM Articles
                 WHERE JournalId = @journalId";
            var ids = await _db.QueryAsync<int>(sqlIds, new { journalId });
            var list = new List<Article>();
            foreach (var id in ids)
            {
                var article = await GetArticleByIdAsync(id);
                if (article != null) list.Add(article);
            }
            return list;
        }

        // Internal DTO for initial article row
        private class ArticleRow
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Abstract { get; set; }
            public DateTime PublicationDate { get; set; }
            public int JournalId { get; set; }
        }
    }
}
