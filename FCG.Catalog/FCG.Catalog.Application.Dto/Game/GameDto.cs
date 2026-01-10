using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.Dto.Game
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public int GenderId { get; set; }
        public int PlataformId { get; set; }
        public string PriceDiscount { get; set; }
    }
}
