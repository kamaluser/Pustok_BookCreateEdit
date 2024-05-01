using System.ComponentModel.DataAnnotations.Schema;

namespace Pustok_MVC.Models
{
    public class Book:AuditEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        [Column(TypeName = "money")]
        public double CostPrice { get; set; }
        [Column(TypeName = "money")]
        public double SalePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double DiscountPercent { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public bool StockStatus { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public List<BookImage> BookImages { get; set; }
        public Author? Author { get; set; }
        public Genre? Genre { get; set; }
        public List<BookTag> BookTags { get; set; }
    }
}
