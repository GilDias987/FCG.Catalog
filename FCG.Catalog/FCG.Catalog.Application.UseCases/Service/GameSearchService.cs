using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using FCG.Catalog.Application.Interface.Service;
using FCG.Catalog.Domain.Documents;

namespace FCG.Catalog.Application.UseCases.Service
{
    public class GameSearchService : IGameSearchService
    {
        private readonly ElasticsearchClient _client;

        private const string IndexName = "games";

        public GameSearchService(
            ElasticsearchClient client
        )
        {
            _client = client;
        }

        public async Task CreateIndexAsync()
        {
            var exists = await _client.Indices.ExistsAsync(IndexName);

            if (exists.Exists)
                return;

            await _client.Indices.CreateAsync<GameDocument>("games", c => c
                    .Mappings(m => m
                        .Properties(p => p
                            .Keyword(x => x.Id)
                            .Text(x => x.Title)
                            .Text(x => x.Description)
                            .DoubleNumber(x => x.Price)
                            .DoubleNumber(x => x.Discount)
                            .IntegerNumber(x => x.GenderId)
                            .IntegerNumber(x => x.PlataformId)
                        )
                    )
                );
        }

        public async Task IndexAsync(GameDocument document)
        {
            await _client.IndexAsync(
                document,
                idx => idx
                    .Index(IndexName)
                    .Id(document.Id)
            );
        }

        public async Task UpdateAsync(GameDocument document)
        {
            await _client.UpdateAsync<GameDocument, GameDocument>(
                IndexName,
                document.Id,
                u => u.Doc(document)
            );
        }

        public async Task DeleteAsync(string Id)
        {
            await _client.DeleteAsync<GameDocument>(
                Id,
                d => d.Index(IndexName)
            );
        }

        public async Task<GameDocument?> GetByIdAsync(string Id)
        {
            var response = await _client.GetAsync<GameDocument>(
                IndexName,
                Id
            );

            if (!response.Found)
                return null;

            return response.Source;
        }

        public async Task<IReadOnlyCollection<GameDocument>> GetAllAsync()
        {
            var response = await _client.SearchAsync<GameDocument>(IndexName, s => s
                .Query(q => q.MatchAll())
                .Size(1000) // limite padrão
            );

            return response.Documents;
        }

        public async Task<IReadOnlyCollection<GameDocument>> SearchAsync(string? term, int? genderId, int? plataformId)
        {
            var mustQueries = new List<Query>();
            var filterQueries = new List<Query>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                mustQueries.Add(new MultiMatchQuery
                {
                    Query = term,
                    Fields = new[] { "title", "description" },
                    Fuzziness = "AUTO"
                });
            }

            if (genderId.HasValue)
            {
                filterQueries.Add(new TermQuery
                {
                    Field = Infer.Field<GameDocument>(x => x.GenderId),
                    Value = genderId.Value
                });
            }

            if (plataformId.HasValue)
            {
                filterQueries.Add(new TermQuery
                {
                    Field = Infer.Field<GameDocument>(x => x.PlataformId),
                    Value = plataformId.Value
                });
            }

            var response = await _client.SearchAsync<GameDocument>(
                                            IndexName,
                                            s => s
                                                .Query(q => q
                                                    .Bool(b => b
                                                        .Must(mustQueries)
                                                        .Filter(filterQueries)
                                                    )
                                                )
                                        );

            return response.Documents;
        }
    }
}
