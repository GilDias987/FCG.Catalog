using FCG.Catalog.Domain.Documents;

namespace FCG.Catalog.Application.Interface.Service
{
    public interface IGameSearchService
    {
        Task CreateIndexAsync();

        Task IndexAsync(GameDocument document);

        Task UpdateAsync(GameDocument document);

        Task DeleteAsync(string Id);

        Task<GameDocument?> GetByIdAsync(string Id);
        Task<IReadOnlyCollection<GameDocument>> GetAllAsync();
        Task<IReadOnlyCollection<GameDocument>> SearchAsync(string? term, int? genderId, int? plataformId);
    }
}
