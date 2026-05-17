namespace FCG.Catalog.Domain.Documents
{
    public class GameDocument
    {
        public string Id { get; set; }

        public string Title { get; set; } = default!;

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public decimal? Discount { get; set; }

        public int GenderId { get; set; }

        public int PlataformId { get; set; }
    }
}
