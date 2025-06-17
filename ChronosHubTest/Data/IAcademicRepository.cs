using ChronosHubTest.Models;

namespace ChronosHubTest.Data
{
    public interface IAcademicRepository
    {
        Task<Article?> GetArticleByIdAsync(int id);
        Task<IEnumerable<Article>> GetArticlesByAuthorAsync(int authorId);
        Task<IEnumerable<Article>> GetArticlesByJournalAsync(int journalId);
    }

}
